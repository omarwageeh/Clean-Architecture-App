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
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<GetOrderByIdQueryHandler> _logger;

        public GetOrderByIdQueryHandler(IUnitOfWork uow, IMapper mapper, ILogger<GetOrderByIdQueryHandler> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetOrderByIdQuery");

            try
            {
                var order = await _uow.OrderRepo.GetById(request.Id);
                _logger.LogInformation($"Retrieved order by ID: {order?.Id}");
                return _mapper.Map<GetOrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling GetOrderByIdQuery");
                throw;
            }
        }
    }
}
