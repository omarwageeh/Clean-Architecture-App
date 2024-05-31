using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string DeliveryAddress { get; set; }
        [Required]
        public required CustomerDetailsDto CustomerDetails { get; set; }
        [Required]
        public ICollection<OrderDetailsDto> OrderDetails { get; set; } = [];
        public decimal TotalCost { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
