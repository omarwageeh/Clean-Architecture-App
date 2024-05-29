﻿using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Merchant { get; set; } = String.Empty;
    }
}
