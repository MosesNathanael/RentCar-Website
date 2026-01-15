using System.ComponentModel.DataAnnotations;

namespace RentCar_API.DTOs
{
   public class CarSearchViewModel
    {   
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int? YearFilter { get; set; }
        public string SortOrder { get; set; } = string.Empty;
        public List<CarDisplayDto>? Cars { get; set; }
    }

    public class CarDisplayDto
    {
        [Required]
        public required string Id { get; set; }
        public string Name { get; set; }= string.Empty;
        public int Year { get; set; }
        public decimal PricePerDay { get; set; }
        public string Image { get; set; } = string.Empty;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalPrice { get; set; }
    }

}

