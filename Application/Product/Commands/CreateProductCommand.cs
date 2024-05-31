using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entitties;
using Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Merchant { get; set; } = string.Empty;

    }
}
