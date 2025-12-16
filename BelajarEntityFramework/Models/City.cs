using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}
