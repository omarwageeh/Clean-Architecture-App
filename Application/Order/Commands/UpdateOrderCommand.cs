using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class UpdateOrderCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;
        [Required]
        public CustomerDetailsDto CustomerDetails { get; set; }
        [Required]
        public ICollection<OrderDetailsDto> OrderDetails { get; set; } = [];
        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
