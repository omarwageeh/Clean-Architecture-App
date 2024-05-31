using Domain.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Update
{
    public class UpdateOrderDto
    {
        public Guid Id { get; set; }
        public CustomerDetails? CustomerDetails { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public DateTime DeliveryTime { get; set; }
        public IEnumerable<UpdateOrderDetailsDto> OrderDetails { get; set; } = [];
    }
}
