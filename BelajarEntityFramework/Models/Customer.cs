using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.Models
{
    public class Customer
    {
     
            public int CustomerId { get; set; }
            [Required, StringLength(100)]
            public string Name { get; set; }
            [Required, EmailAddress, StringLength(100)]
            public string Email { get; set; }
            [StringLength(250)]
            public string Address { get; set; }
            [StringLength(15)]
            public string Phone { get; set; }
            // Navigation property: one customer can have many orders.
            public ICollection<Order> Orders { get; set; }
    }
}

