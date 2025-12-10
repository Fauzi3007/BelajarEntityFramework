using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class ProductPdf
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        // Navigation property: one product can be part of many order items.
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
