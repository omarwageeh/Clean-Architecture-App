using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Create
{
    public class CreateOrderDetailsDto
    {
        [Required]
        public required Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
