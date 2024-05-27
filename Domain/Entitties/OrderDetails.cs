using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties
{
    public class OrderDetails : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderDetails(int productId, int quantity, decimal price) : base()
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }
    }
}
