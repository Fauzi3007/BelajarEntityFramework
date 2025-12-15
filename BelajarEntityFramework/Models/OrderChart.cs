using BelajarEntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class OrderChart
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, StringLength(20)]
        public string OrderNumber { get; set; }

        // Foreign Key to Customer
        public int CustomerId { get; set; }
        public CustomerChart Customer { get; set; }

        // Navigation
        public List<OrderItemChart> OrderItems { get; set; }

        // Optional convenience property to sum order details
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }
}