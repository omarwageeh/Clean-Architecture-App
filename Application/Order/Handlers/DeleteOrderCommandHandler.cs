using Application.Commands;
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
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IUnitOfWork uow, ILogger<DeleteOrderCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Deleting order with ID: {request.Id}");

                var order = await _uow.OrderRepo.GetById(request.Id);
                if (order == null)
                {
                    _logger.LogWarning($"Order with ID {request.Id} not found");
                    return -1;
                }

                if (order.DeliveryTime < DateTime.Now)
                {
                    _logger.LogWarning($"Order with ID {request.Id} cannot be deleted as it has already been delivered");
                    return -2;
                }

                await _uow.OrderRepo.Delete(request.Id);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Order with ID {request.Id} deleted successfully");

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting order with ID {request.Id}");
                throw;
            }
        }
    }
}
