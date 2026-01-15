using System.ComponentModel.DataAnnotations;

namespace RentCar_MVC.Models
{
    public class RentalViewModel
    {
        [Key]
        [MaxLength(36)]
        public string Rental_id {get; set;} = string.Empty;

        [Required]
        public DateTime Rental_date {get; set;}

        [Required]
        public DateTime Return_date {get; set;}

        [Required]
        public decimal Total_price {get; set;}

        [Required]
        public bool Payment_status {get; set;} = false;

        [Required, MaxLength(36)]
        public required string Customer_id {get; set;}

        [Required, MaxLength(36)]
        public required string Car_id {get; set;}
        
    }
}