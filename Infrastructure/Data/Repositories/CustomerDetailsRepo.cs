using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CustomerDetailsRepo : GenericRepo<CustomerDetails>, ICustomerDetailsRepo
    {
        public CustomerDetailsRepo(IAppDbContext context, ILogger<CustomerDetailsRepo> logger) : base(context, logger)
        { 
        }
    }
}
