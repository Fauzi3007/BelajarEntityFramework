using BelajarEntityFramework.Models;
using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.Models
{
    public class CustomerChart
    {
        [Key]
        public int CustomerId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        // Navigation
        public List<OrderChart> Orders { get; set; }
    }
}