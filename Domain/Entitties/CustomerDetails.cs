using Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entitties
{
    public class CustomerDetails : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }

        public CustomerDetails(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
        }
    }
}