using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CountryId { get; set; }
        public ICollection<City> Citites { get; set; }
    }
}
