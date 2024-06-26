﻿using Application.Dtos.Common;
using Application.Dtos.Get;
using Application.Queries;
using AutoMapper;
using Domain.Contracts.Repositories;
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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsQueryHandler> _logger;

        public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetProductsQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetProductsQuery");

                (var productList, var totalPages) = await _uow.ProductRepo.GetAll(request.Page, request.PageSize, request.Filter, request.FilterBy, request.SortBy, request.Descending);
                var productDtoList = _mapper.Map<IEnumerable<ProductDto>>(productList);

                _logger.LogInformation($"GetProductsQuery handled successfully, returned product count : {productDtoList.Count()}");

                return new GetProductsDto(productDtoList, totalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling GetProductsQuery");
                throw;
            }
        }
    }
}
