namespace RentCar_API.DTOs
{
    public class RentalResponseDto
    {
        public string RentalId { get; set; } = string.Empty;
        public string CarId { get; set; } = string.Empty;
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool PaymentStatus { get; set; }
    }

}

