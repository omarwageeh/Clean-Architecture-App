using Application.Commands;
using Application.Dtos.Create;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderDto?>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> _logger;

        public CreateOrderCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateOrderDto?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling CreateOrderCommand");

                var order = _mapper.Map<Order>(request);
                foreach (var orderDetails in order.OrderDetails)
                {
                    var product = await _uow.ProductRepo.GetById(orderDetails.ProductId);
                    if (product == null)
                    {
                        _logger.LogError($"Product with ID {orderDetails.ProductId} not found");
                        return null;
                    }
                    orderDetails.Product = product;
                    orderDetails.Price = product.Price;
                }

                await _uow.OrderRepo.Add(order);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Order created successfully");

                return _mapper.Map<CreateOrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling CreateOrderCommand");
                throw;
            }
        }
    }
}
