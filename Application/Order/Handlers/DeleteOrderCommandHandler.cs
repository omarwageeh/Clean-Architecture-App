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
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IUnitOfWork _uow;
        public DeleteOrderCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepo.GetById(request.Id);
            if(order == null)
            {
                return -1;
            }
            await _uow.OrderRepo.Delete(request.Id);
            return await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
