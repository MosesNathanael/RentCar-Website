using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class MsCar
    {
        [Key]
        [MaxLength(36)]
        public string Car_id {get; set;}= Guid.NewGuid().ToString();

        [Required, MaxLength(200)]
        public string Name {get; set;} = string.Empty;

        [Required, MaxLength(100)]
        public string Model {get; set;} = string.Empty;

        [Required]
        public int Year {get; set;}

        [Required, MaxLength(50)]
        public string License_plate {get; set;} = string.Empty;

        [Required]
        public int Number_of_car_seats {get; set;}

        [Required, MaxLength(100)]
        public string Transmission {get; set;} = string.Empty;

        [Required]
        public decimal Price_per_day {get; set;}

        [Required]
        public bool Status {get; set;} = true;

        public ICollection<TrRental>? Rentals {get; set;}

        public ICollection<TrMaintenance>? Maintenances {get; set;}

        public ICollection<MsCarImages> CarImages {get; set;} = new List<MsCarImages>();

    }
}