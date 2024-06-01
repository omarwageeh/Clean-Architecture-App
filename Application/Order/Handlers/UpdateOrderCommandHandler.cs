using Application.Commands;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Updating order with ID: {request.Id}");

                var order = await _uow.OrderRepo.GetById(request.Id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {request.Id} not found");
                    return -1;
                }

                foreach (var orderDetail in request.OrderDetails)
                {
                    if (!order.OrderDetails.Any(od => od.Id == orderDetail.Id))
                    {
                        _logger.LogWarning($"Order detail with ID {orderDetail.Id} not found in order with ID {request.Id}");
                        return -2;
                    }
                }

                _mapper.Map(request, order);

                await _uow.OrderRepo.Update(order);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Order with ID {request.Id} updated successfully");

                return await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the order with ID {request.Id}");
                throw;
            }
        }
    }
}
