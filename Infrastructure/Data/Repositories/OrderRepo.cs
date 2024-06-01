using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<OrderRepo> _logger;

        public OrderRepo(IAppDbContext context, ILogger<OrderRepo> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        override public Task<Order?> GetById(Guid id)
        {
            _logger.LogInformation("Getting order by ID: {OrderId}", id);

            var order = _context.Orders
                        .AsNoTracking()
                        .Include(o => o.OrderDetails)
                            .ThenInclude(od => od.Product)
                        .Include(o => o.CustomerDetails)
                        .Where(o => o.Id == id)
                        .FirstOrDefault();

            _logger.LogInformation("Retrieved order by ID: {OrderId}", id);

            return Task.FromResult(order);
        }

        public async Task<Tuple<IEnumerable<Order>, int>> GetAll(int page, int pageSize, string? filter, OrderFilter? filterBy, OrderSort? sortBy, bool descending = false)
        {
            _logger.LogInformation($"Getting all orders with page: {page}, pageSize: {pageSize}, filter: {filter}, filterBy: {filterBy}, sortBy: {sortBy}, descending: {descending}");

            var query = _context.Orders
                        .AsNoTracking()
                        .Include(o => o.OrderDetails)
                            .ThenInclude(od => od.Product)
                        .Include(o => o.CustomerDetails)
                        .AsQueryable();

            if (filterBy != null && filter != null)
            {
                switch (filterBy)
                {
                    case OrderFilter.CustomerName:
                        query = query.Where(o => o.CustomerDetails.Name.Contains(filter));
                        break;
                    case OrderFilter.ProductName:
                        query = query.Where(o => o.OrderDetails.Any(od => od.Product.Name.Contains(filter)));
                        break;
                    case OrderFilter.CustomerEmail:
                        query = query.Where(o => o.CustomerDetails.Email.Contains(filter));
                        break;
                    case OrderFilter.Phone:
                        query = query.Where(o => o.CustomerDetails.Phone.Contains(filter));
                        break;
                    default:
                        break;
                }
            }

            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case OrderSort.OrderDeliveryTime:
                        query = descending ? query.OrderByDescending(o => o.DeliveryTime) : query.OrderBy(o => o.DeliveryTime);
                        break;
                    case OrderSort.TotalCost:
                        query = descending ? query.OrderByDescending(o => o.TotalCost) : query.OrderBy(o => o.TotalCost);
                        break;
                    default:
                        break;
                }
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var orders = await query.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            _logger.LogInformation("Retrieved all orders");

            var combinedRet = new Tuple<IEnumerable<Order>, int>(orders, totalPages);

            return combinedRet;
        }
    }
}
