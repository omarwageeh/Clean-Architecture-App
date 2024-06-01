using Domain.Common;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        public byte[]? Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public required string Merchant { get; set; }

        public Product(string name, string description, decimal price, string merchant, byte[]? image = null) : base()
        {
            Name = name.ToLower();
            Description = description;
            Price = price;
            Merchant = merchant;
            Image = image;
        }
    }
}
