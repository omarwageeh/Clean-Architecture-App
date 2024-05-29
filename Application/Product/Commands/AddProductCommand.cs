using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitties;
using Application.Dtos;

namespace Application.Commands
{
    public class AddProductCommand : IRequest<ProductDto>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Merchant { get; set; }

    }
}
