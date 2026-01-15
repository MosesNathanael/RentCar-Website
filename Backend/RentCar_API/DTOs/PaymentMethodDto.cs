using System.ComponentModel.DataAnnotations;

namespace RentCar_API.DTOs
{
    public class PaymentMethod
    {
        [Required]
        public string paymentMethod {get; set; }= string.Empty;

    }
}