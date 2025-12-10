using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public ProductPdf Product { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal TaxPercent { get; set; }
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;
        [NotMapped]
        public decimal TotalTaxAmount => (TotalPrice * TaxPercent) / 100;
        [NotMapped]
        public decimal TotalPriceWithTax => TotalPrice + TotalTaxAmount;
    }
}
