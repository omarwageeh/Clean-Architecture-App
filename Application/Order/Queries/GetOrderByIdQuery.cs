using Application.Dtos;
using Application.Dtos.Get;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetOrderByIdQuery : IRequest<GetOrderDto>
    {
        public Guid Id { get; set; }

        public GetOrderByIdQuery(Guid id) => Id = id;
    }
    
}
