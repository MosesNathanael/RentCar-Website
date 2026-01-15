using System.ComponentModel.DataAnnotations;
namespace RentCar_MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email must be filled")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email {get; set;}

        [Required(ErrorMessage = "Password must be filled")]
        public required string Password {get; set;}
    }
}