using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties
{
    public class Order : BaseEntity
    {
        [Required]
        public required string DeliveryAddress { get; set; }
        [Required]
        public required CustomerDetails CustomerDetails { get; set; }
        [Required]
        public ICollection<OrderDetails> OrderDetails { get; set; } = [];
        public decimal TotalCost => OrderDetails.Sum(x => x.Price * x.Quantity);
        [Required] 
        public required DateTime DeliveryTime { get; set; }
    }
}
