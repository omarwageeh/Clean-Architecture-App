using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Common;

namespace Application.Dtos.Create
{
    public class CreateProductDto : ProductDto
    {
        public ProductDto? Product { get; set; }
        public bool Exists { get; set; }
        public CreateProductDto(ProductDto? product, bool exists)
        {
            Product = product;
            Exists = exists;
        }
    }
}
