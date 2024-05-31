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
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetOrdersQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<GetOrdersDto> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            (var orderList, var totalPages) = await _uow.OrderRepo.GetAll(request.Page, request.PageSize, request.Filter, request.FilterBy, request.SortBy, request.Desending);
            var orderDtoList = _mapper.Map <IEnumerable<GetOrderDto>>(orderList);

            return new GetOrdersDto(orderDtoList, totalPages);
        }
    }
}
