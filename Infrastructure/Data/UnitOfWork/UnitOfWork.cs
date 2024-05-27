using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.UnitofWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly IAppDbContext _context;

        public ICustomerDetailsRepo CustomerDetailsRepo {get;}

        public IOrderDetailsRepo OrderDetailsRepo { get; }

        public IOrderRepo OrderRepo { get; }

        public IProductRepo ProductRepo { get; }

        public UnitOfWork(IAppDbContext context, IProductRepo productRepo, ICustomerDetailsRepo customerDetailsRepo, IOrderRepo orderRepo, IOrderDetailsRepo orderDetailsRepo)
        {
            _context = context;
            ProductRepo = productRepo;
            CustomerDetailsRepo = customerDetailsRepo;
            OrderRepo = orderRepo;
            OrderDetailsRepo = orderDetailsRepo;
        }

        public  int SaveChanges()
        {
            return   _context.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
