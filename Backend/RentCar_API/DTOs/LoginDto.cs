using System.ComponentModel.DataAnnotations;

namespace RentCar_API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email must be filled")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email {get; set;}

        [Required(ErrorMessage = "Password must be filled")]
        public required string Password {get; set;}
    }
}