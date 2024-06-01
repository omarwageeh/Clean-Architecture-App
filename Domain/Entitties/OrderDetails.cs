using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public required Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
