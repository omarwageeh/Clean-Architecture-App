using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Common;

namespace Application.Dtos.Get
{
    public class GetProductsDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public int TotalPages { get; set; }

        public GetProductsDto(IEnumerable<ProductDto> products, int totalPages)
        {
            Products = products;
            TotalPages = totalPages;
        }
    }
}
