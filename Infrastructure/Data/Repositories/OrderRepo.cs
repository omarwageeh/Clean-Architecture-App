using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
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
        new public Task<Order?> GetById(Guid id)
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
    }
}
