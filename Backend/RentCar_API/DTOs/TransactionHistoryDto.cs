namespace RentCar_API.DTOs
{
    public class TransactionHistoryDto
    {
        public DateTime RentalDate {get; set;}
        public DateTime ReturnDate {get; set;}
        public string CarName {get; set;} = string.Empty;
        public decimal Price {get; set;}
        public int Total_Days {get; set;}
        public decimal Total_Price {get; set;}
        public bool status {get; set;}
    }
}