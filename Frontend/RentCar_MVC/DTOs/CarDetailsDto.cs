namespace RentCar_MVC
{
    public class CarDetailsDto
    {
        public string CarId {get; set;} = string.Empty;
        public string CarName {get; set;} = string.Empty;
        public List<string> CarImages {get; set;} = new List<string>();
        public string Transmission {get; set;} = string.Empty;
        public decimal Price {get; set;}
        public decimal Total_Price {get; set;}


        public DateTime RentalDate {get; set;}
        
        public DateTime ReturnDate {get; set;}
    }
}