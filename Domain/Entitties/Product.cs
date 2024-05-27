using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitties
{
    public class Product : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
        public Blob? Imaage { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public required string Merchant { get; set; }

        public Product(string name, string description, decimal price, string merchant, Blob? image = null) : base()
        {
            Name = name;
            Description = description;
            Price = price;
            Merchant = merchant;
            Imaage = image;
        }
    }
}
