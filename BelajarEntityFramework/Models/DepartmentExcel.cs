using System.ComponentModel.DataAnnotations;

namespace BelajarEntityFramework.Models
{
    public class DepartmentExcel
    {
        [Key]
        public int DepartmentId { get; set; }    
        public string Name { get; set; }              
        public ICollection<EmployeeExcel> Employees { get; set; }
    }
}
