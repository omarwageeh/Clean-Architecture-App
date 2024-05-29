using Application.Commands;
using AutoMapper;
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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IUnitOfWork uow, IMapper mapper) 
        { 
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepo.GetById(request.Id);
            if (product == null) 
            {
                return -1;
            }
            _mapper.Map(request, product);
            await _uow.ProductRepo.Update(product);
            return await _uow.SaveChangesAsync(cancellationToken);
        }
    }
}
