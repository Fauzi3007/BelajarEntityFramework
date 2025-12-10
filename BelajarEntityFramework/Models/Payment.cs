using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        public DateTime? PaymentDate { get; set; }
        [StringLength(50)]
        public string PaymentStatus { get; set; }  
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentAmount { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
