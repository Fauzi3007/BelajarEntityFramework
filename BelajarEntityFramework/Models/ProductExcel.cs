using BelajarEntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class ProductExcel
    {
        [Key]
        public int ProductId { get; set; }  

        [Required]
        [StringLength(100)]
        public string Name { get; set; }    

        public string? Description { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }  

        [Required]
        [Range(0, 100000, ErrorMessage = "Quantity cannot be Negative Number")]
        public int Quantity { get; set; }  

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }  

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100.")]
        public decimal DiscountPercentage { get; set; }  

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }  

        public bool IsActive { get; set; } 

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}