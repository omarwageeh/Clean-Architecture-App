using Application.Commands;
using Domain.Contracts.UnitofWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IUnitOfWork _uow;
        public DeleteProductCommandHandler(IUnitOfWork uow) { 
            _uow = uow;
        }
        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product  = await _uow.ProductRepo.GetById(request.Id);
            if (product == null)
            {
                return -1;
            }
            await _uow.ProductRepo.Delete(request.Id);
            return await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
