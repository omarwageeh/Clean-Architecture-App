using Application.Commands;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UpdateOrderCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepo.GetById(request.Id);
            if(order == null)
            {
                return -1;
            }
            foreach(var orderDetail in request.OrderDetails)
            {
                if(!order.OrderDetails.Any(od => od.Id == orderDetail.Id))
                {
                    return -2;
                }
            }
            _mapper.Map(request, order);
            await _uow.OrderRepo.Update(order);
            return await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
