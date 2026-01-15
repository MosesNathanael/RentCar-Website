namespace RentCar_API.DTOs
{
    public class PaymentResponse
    {
        public string PaymentId { get; set; }  = string.Empty;
        public decimal Amount { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public string PaymentMethod { get; set; } = string.Empty;
        public string RentalId { get; set; } = string.Empty;
    }
}