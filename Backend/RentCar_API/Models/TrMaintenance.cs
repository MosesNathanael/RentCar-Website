using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class TrMaintenance
    {
        [Key]
        [MaxLength(36)]
        public string Maintenance_id {get; set;} = Guid.NewGuid().ToString();

        [Required]
        public DateTime Maintenance_date {get; set;}

        [Required, MaxLength(4000)]
        public string Description {get; set;} = string.Empty;

        [Required]
        public decimal Cost {get; set;}

        [Required, MaxLength(36)]
        public required string Car_id {get; set;}
        
        [Required, MaxLength(36)]
        public required string Employee_id {get; set;}

        public required MsCar Car {get; set;}
        public required MsEmployee Employee {get; set;}
        
    }
}