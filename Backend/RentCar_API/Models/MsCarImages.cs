using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class MsCarImages
    {
        [Key]
        [MaxLength(36)]
        public string Image_car_id {get; set;}= Guid.NewGuid().ToString();

        [Required, MaxLength(36)]
        public string Car_id {get; set;} = string.Empty;

        [Required, MaxLength(2000)]
        public string Image_link {get; set;} = string.Empty;

        [Required]
        public bool FrontImage {get; set;} = false;

        public required MsCar Car {get; set;}
    }
}