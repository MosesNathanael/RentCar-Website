using System.ComponentModel.DataAnnotations;

namespace RentCar_MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required and must be filled")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email {get; set;}

        [Required(ErrorMessage = "Password can't be empty")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 character")]
        public required string Password {get; set;}

        [Required(ErrorMessage = "Name must be filled")]
        public required string Name {get; set;}
        public string Phone_number {get; set;} = string.Empty;
        public string Address {get; set; }= string.Empty;

        [Required(ErrorMessage = "License number is required")]
        public string Driver_license_number {get; set;} = string.Empty;
        
    }
}