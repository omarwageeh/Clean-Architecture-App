using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        public OrderRepo(IAppDbContext context) : base(context)
        {
        }
    }
}
