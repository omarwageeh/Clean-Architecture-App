﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Common;
using MediatR;

namespace Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public GetProductByIdQuery(Guid id) => Id = id;
    }
}
