using Application.Dtos.Common;
using Application.Dtos.Get;
using Application.Queries;
using AutoMapper;
using Domain.Contracts.Repositories;
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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GetProductsDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            (var productList, var totalPages) = await _uow.ProductRepo.GetAll(request.Page, request.PageSize, request.Filter, request.FilterBy, request.SortBy, request.Descending);
             var productDtoList= _mapper.Map<IEnumerable<ProductDto>>(productList);
            
            return new GetProductsDto(productDtoList, totalPages);
        }
    }
}
