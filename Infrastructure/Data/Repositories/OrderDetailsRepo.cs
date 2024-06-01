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
    public class OrderDetailsRepo : GenericRepo<OrderDetails> ,IOrderDetailsRepo
    {
        public OrderDetailsRepo(IAppDbContext context, ILogger<OrderDetailsRepo> logger) : base(context, logger)
        {
        }

    }
}
