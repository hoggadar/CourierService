using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourierService.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Desc { get; set; } = string.Empty;

        [Required]
        public int Price { get; set; }

        [Required]
        public int OverPrice { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey("CourierId")]
        public int CourierId { get; set; }
        public CourierModel? Courier { get; set; }
    }
}
