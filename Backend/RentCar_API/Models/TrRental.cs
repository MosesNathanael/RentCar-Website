using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class TrRental
    {
        [Key]
        [MaxLength(36)]
        public string Rental_id {get; set;} = Guid.NewGuid().ToString();

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

        public required MsCustomer Customer {get; set;}
        public required MsCar Car {get; set;}

        public LtPayment? Payment {get; set;}
        
    }
}