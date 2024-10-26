using CourierService.Enums;
using System.ComponentModel.DataAnnotations;

namespace CourierService.Models
{
    public class CourierModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public int Dist { get; set; }

        [Required]
        public TypeEnum Type { get; set; }

        public ICollection<OrderModel>? Orders { get; set; }
    }
}
