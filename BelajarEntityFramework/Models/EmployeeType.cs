using BelajarEntityFramework.Models;

namespace BelajarEntityFramework.Models
{
    public class EmployeeType
    {
        public int EmployeeTypeId { get; set; }       
        public string TypeName { get; set; }        
        public ICollection<Employee> Employees { get; set; }
    }
}