using Application.Commands;
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<UpdateProductCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _uow.ProductRepo.GetById(request.Id);
                if (product == null)
                {
                    _logger.LogWarning($"Product not found with ID: {request.Id}");
                    return -1;
                }
                _mapper.Map(request, product);
                await _uow.ProductRepo.Update(product);
                return await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the product with ID: {request.Id}");
                throw;
            }
        }
    }
}
