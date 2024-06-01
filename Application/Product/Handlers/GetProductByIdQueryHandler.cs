using Application.Dtos.Common;
using Application.Queries;
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
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetProductByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetProductByIdQuery with ID: {Id}", request.Id);

                var product = await _uow.ProductRepo.GetById(request.Id);

                _logger.LogInformation("Successfully retrieved product with ID: {Id}", request.Id);

                return _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the product by ID");
                throw;
            }
        }
    }
}
