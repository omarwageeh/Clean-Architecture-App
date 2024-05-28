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
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Merchant { get; set; }
        public AddProductCommand()
        {
        }

    }
}
