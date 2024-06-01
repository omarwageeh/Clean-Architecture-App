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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IUnitOfWork uow, ILogger<DeleteProductCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Deleting product with ID: {request.Id}");

                var product = await _uow.ProductRepo.GetById(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {request.Id} not found");
                    return -1;
                }

                if (await _uow.OrderDetailsRepo.Any(od => od.ProductId == request.Id))
                {
                    _logger.LogWarning($"Product with ID {request.Id} is associated with orders");
                    return -2;
                }

                await _uow.ProductRepo.Delete(request.Id);
                await _uow.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"Product with ID {request.Id} deleted successfully");

                return 1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting product with ID {request.Id}");
                throw;
            }
        }
    }
}
