using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BelajarEntityFramework.Models
{
    public class Department
    {
        public int DepartmentId {  get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public List<Employee> Employees { get; set; }
    }
}
