using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Get
{
    public class GetOrderDetailsDto
    {
        public Guid Id { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
    }
}
