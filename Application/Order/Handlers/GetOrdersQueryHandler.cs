using Application.Dtos.Create;
using Application.Dtos.Get;
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
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrdersQueryHandler> _logger;

        public GetOrdersQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetOrdersQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrdersDto> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Handling GetOrdersQuery");

                (var orderList, var totalPages) = await _uow.OrderRepo.GetAll(request.Page, request.PageSize, request.Filter, request.FilterBy, request.SortBy, request.Desending);
                var orderDtoList = _mapper.Map<IEnumerable<GetOrderDto>>(orderList);

                _logger.LogInformation($"GetOrdersQuery handled successfully returned Order Count: {orderDtoList.Count()}" );

                return new GetOrdersDto(orderDtoList, totalPages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling GetOrdersQuery");
                throw;
            }
        }
    }
}
