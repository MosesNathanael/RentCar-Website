using System.ComponentModel.DataAnnotations;

namespace RentCar_API.Models
{
    public class MsEmployee
    {
        [Key]
        [MaxLength(36)]
        public string Employee_id {get; set;} = Guid.NewGuid().ToString();

        [Required,MaxLength(200)]
        public string Name {get; set;} = string.Empty;

        [Required, MaxLength(4000)]
        public string Position {get; set;} = string.Empty;

        [Required, MaxLength(100)]
        public string Email {get; set;} = string.Empty;

        [Required, MaxLength(50)]
        public string Phone_number {get; set;} = string.Empty;

        public ICollection<TrMaintenance>? Maintenances {get; set;}
    }
}