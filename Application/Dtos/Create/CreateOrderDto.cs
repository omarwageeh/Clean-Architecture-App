using Application.Dtos.Common;
using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Create
{
    public class CreateOrderDto
    {
        public Guid Id { get; set; }
        [Required]
        public string DeliveryAddress { get; set; } = string.Empty;
        [Required]
        public CustomerDetailsDto? CustomerDetails { get; set; }
        [Required]
        public IEnumerable<CreateOrderDetailsDto> OrderDetails { get; set; } = [];
        public decimal TotalCost { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
    }
}
