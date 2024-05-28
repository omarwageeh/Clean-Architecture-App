using Application.Commands;
using MediatR;
using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using Application.Dtos;

namespace Application.Handlers
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productToAdd = _mapper.Map<Product>(request);
            await _uow.ProductRepo.Add(productToAdd);
            await _uow.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(productToAdd);
        }
    }
}
