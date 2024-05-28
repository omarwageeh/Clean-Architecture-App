using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Entitties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        public ProductRepo(IAppDbContext context) : base(context)
        {
        }
    }
}
