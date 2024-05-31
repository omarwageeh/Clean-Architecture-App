using Application.Commands;
using Application.Dtos;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using Domain.Entitties;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<OrderDto?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            foreach(var orderDetails in order.OrderDetails)
            {
                var product = await _uow.ProductRepo.GetById(orderDetails.ProductId);
                if (product == null)
                    return null;
                orderDetails.Product = product;
                orderDetails.Price = product.Price;
            }
            await _uow.OrderRepo.Add(order);
            await _uow.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderDto>(order);
        }
    }
}
