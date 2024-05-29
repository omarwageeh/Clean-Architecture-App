using Application.Dtos;
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
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetProductsQueryHandler(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productList = await _uow.ProductRepo.GetAll();
            return _mapper.Map<IEnumerable<Product>, IEnumerable < ProductDto >> (productList);
        }
    }
}
