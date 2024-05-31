using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Common
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Merchant { get; set; } = string.Empty;
    }
}
