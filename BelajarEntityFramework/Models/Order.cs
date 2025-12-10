using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required, StringLength(50)]
        public string OrderNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
     
        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalTax { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotal { get; set; }
    }
}
