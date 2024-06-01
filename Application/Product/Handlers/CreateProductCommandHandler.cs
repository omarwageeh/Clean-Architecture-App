using Application.Commands;
using MediatR;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using Application.Dtos.Create;
using Application.Dtos.Get;
using Application.Dtos.Common;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IUnitOfWork uow, IMapper mapper, ILogger<CreateProductCommandHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreateProductCommand");

            var product = await _uow.ProductRepo.GetProductByName(request.Name);
            if (product != null)
            {
                _logger.LogWarning($"Product with name {request.Name} already exists");
                return new CreateProductDto(null, true);
            }

            var productToAdd = _mapper.Map<Product>(request);
            await _uow.ProductRepo.Add(productToAdd);
            await _uow.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Product with name {request.Name} created successfully");

            return new CreateProductDto(_mapper.Map<ProductDto>(productToAdd), false);
        }
    }
}
