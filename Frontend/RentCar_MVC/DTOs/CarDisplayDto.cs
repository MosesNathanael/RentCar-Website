using System.Text.Json.Serialization;

namespace RentCar_MVC.DTOs
{
    public class CarDisplayDto
    {
        [JsonPropertyName("car_id")]
        public string Id {get; set;} = string.Empty;
        [JsonPropertyName("name")]
        public string Name {get; set;} = string.Empty;
        [JsonPropertyName("price_per_day")]
        public decimal PricePerDay {get; set;} 
        [JsonPropertyName("images")]
        public string Image {get; set;} = string.Empty;
    }
}