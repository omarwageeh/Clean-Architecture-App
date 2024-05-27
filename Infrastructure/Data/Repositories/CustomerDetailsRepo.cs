using Domain.Contracts.Repositories;
using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CustomerDetailsRepo : ICustomerDetailsRepo
    {
        public Task<CustomerDetails> Add(CustomerDetails entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDetails>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDetails> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDetails> Update(CustomerDetails entity)
        {
            throw new NotImplementedException();
        }
    }
}
