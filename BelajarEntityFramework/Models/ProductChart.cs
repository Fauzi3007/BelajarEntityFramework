using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class ProductChart
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // Alternatively, you could create a dedicated Category entity/table.
        // But for simplicity, we will store category as a string.
        [Required, StringLength(100)]
        public string Category { get; set; }
    }
}