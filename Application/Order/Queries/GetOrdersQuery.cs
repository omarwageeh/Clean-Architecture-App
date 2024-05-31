using Application.Dtos.Get;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetOrdersQuery : IRequest<GetOrdersDto>
    {
        [Required]
        public int Page { get; set; }
        [Required]
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public OrderFilter? FilterBy { get; set; }
        public OrderSort? SortBy { get; set; }
        public bool Desending { get; set; }
    }
}
