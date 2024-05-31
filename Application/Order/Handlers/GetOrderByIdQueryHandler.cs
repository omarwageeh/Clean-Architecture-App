using Application.Dtos.Create;
using Application.Dtos.Get;
using Application.Queries;
using AutoMapper;
using Domain.Contracts.UnitofWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetOrderByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        
        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _uow.OrderRepo.GetById(request.Id);
            return _mapper.Map<GetOrderDto>(order);
        }
    }
}
