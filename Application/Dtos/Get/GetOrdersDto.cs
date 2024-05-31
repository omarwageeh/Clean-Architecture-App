using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.Create;

namespace Application.Dtos.Get
{
    public class GetOrdersDto
    {
        public IEnumerable<GetOrderDto> Orders { get; set; }
        public int TotalPages { get; set; }

        public GetOrdersDto(IEnumerable<GetOrderDto> orders, int totalPages)
        {
            Orders = orders;
            TotalPages = totalPages;
        }
    }
}
