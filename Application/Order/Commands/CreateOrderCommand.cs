using Application.Dtos.Common;
using Application.Dtos.Create;
using Domain.Entitties;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderDto>
    {
        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;
        [Required]
        public required CustomerDetailsDto CustomerDetails { get; set; }
        [Required]
        public ICollection<CreateOrderDetailsDto> OrderDetails { get; set; } = [];
        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
