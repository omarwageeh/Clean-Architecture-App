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
using Application.Dtos.Create;
using Application.Dtos.Get;
using Application.Dtos.Common;

namespace Application.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IUnitOfWork uow, IMapper mapper) 
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<CreateProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepo.GetProductByName(request.Name);
            if (product != null)
                return new CreateProductDto(null, true);
            var productToAdd = _mapper.Map<Product>(request);
            await _uow.ProductRepo.Add(productToAdd);
            await _uow.SaveChangesAsync(cancellationToken);

            return new CreateProductDto(_mapper.Map<ProductDto>(productToAdd), false);
        }
    }
}
