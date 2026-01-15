using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class MsCustomer
    {
        [Key]
        [MaxLength(36)]
        public string Customer_id {get; set;}= Guid.NewGuid().ToString();

        [Required, MaxLength(100)]
        public string Email {get; set;} = string.Empty;

        [Required, MaxLength(100)]
        public string Password {get; set;} = string.Empty;

        [Required, MaxLength(200)]
        public string Name {get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Phone_number {get; set;} = string.Empty;

        [Required, MaxLength(500)]
        public string Address {get; set;} = string.Empty;

        [Required, MaxLength(100)]
        public string Driver_license_number {get; set;} = string.Empty;


        public ICollection<TrRental>? Rentals {get; set;}
    }
}