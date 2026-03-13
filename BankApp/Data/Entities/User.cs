using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Data.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string Role { get; set; } = "Customer";
        public int? CustomerId { get; set; } // Null acceptans då en admin har ingen CustomerId
        public Customer? Customer { get; set; }

    }
}


