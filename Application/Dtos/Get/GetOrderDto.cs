using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Get
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public CustomerDetailsDto? CustomerDetails { get; set; }
        public IEnumerable<GetOrderDetailsDto> OrderDetails { get; set; } = [];
        public decimal TotalCost { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
