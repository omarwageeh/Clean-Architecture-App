using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public OrderRepo(IAppDbContext context) : base(context)
        {
            _context = context;
        }

        override public Task<Order?> GetById(Guid id)
        {
            var order = _context.Orders
                        .AsNoTracking()
                        .Include(o => o.OrderDetails)
                            .ThenInclude(od => od.Product)
                        .Include(o => o.CustomerDetails)
                        .Where(o => o.Id == id)
                        .FirstOrDefault();

            return Task.FromResult(order);
        }

        public async Task<Tuple<IEnumerable<Order>, int>> GetAll(int page, int pageSize, string? filter, OrderFilter? filterBy, OrderSort? sortBy, bool descending = false)

        {
            var query = _context.Orders
                        .AsNoTracking()
                        .Include(o => o.OrderDetails)
                            .ThenInclude(od => od.Product)
                        .Include(o => o.CustomerDetails)
                        .AsQueryable();

            if (filterBy != null && filter != null)
            {
                switch(filterBy)
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

            var combinedRet = new Tuple<IEnumerable<Order>, int>(orders, totalPages);

            return combinedRet;
        }
    }
}
