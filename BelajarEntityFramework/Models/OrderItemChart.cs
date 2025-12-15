using BelajarEntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class OrderItemChart
    {
        [Key]
        public int OrderItemId { get; set; }

        // Foreign Key to Order
        public int OrderId { get; set; }
        public OrderChart Order { get; set; }

        // Foreign Key to Product
        public int ProductId { get; set; }
        public ProductChart Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
    }
}