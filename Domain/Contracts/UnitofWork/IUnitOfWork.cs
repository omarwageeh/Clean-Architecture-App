using Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.UnitofWork
{
    public interface IUnitOfWork
    {
        ICustomerDetailsRepo CustomerDetailsRepo { get; }
        IOrderDetailsRepo OrderDetailsRepo { get; }
        IOrderRepo OrderRepo { get; }
        IProductRepo ProductRepo { get; }
        int SaveChanges();
    }
}
