using Domain.Contracts.Repositories;
using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class OrderDetailsRepo : IOrderDetailsRepo
    {
        public Task<OrderDetails> Add(OrderDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetails> Update(OrderDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
