using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class CustomerDetails : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public required string Email { get; set; }
    }
}