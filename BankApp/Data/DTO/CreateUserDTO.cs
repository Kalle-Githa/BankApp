using System.ComponentModel.DataAnnotations;

namespace BankApp.Data.DTO
{
    public class CreateUserDTO
    {


        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string TemporaryPassword { get; set; }

    }
}
