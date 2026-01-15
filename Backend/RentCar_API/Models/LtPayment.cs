using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class LtPayment
    {
        [Key]
        [MaxLength(36)]
        public string Payment_id {get; set;}= Guid.NewGuid().ToString();

        [Required]
        public DateTime Payment_date {get; set;}

        [Required]
        public decimal Amount {get; set;}

        [Required]
        public string Payment_method {get; set;} = string.Empty;

        [Required]
        public required string Rental_id {get; set;}

        public TrRental? Rental {get; set;}
    }
}