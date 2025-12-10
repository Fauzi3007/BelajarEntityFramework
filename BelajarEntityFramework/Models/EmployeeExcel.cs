using BelajarEntityFramework.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BelajarEntityFramework.Models
{
    public class EmployeeExcel
    {
        [Key]
        public int EmployeeId { get; set; }         
        public string FirstName { get; set; }         
        public string LastName { get; set; }         
        public string Email { get; set; }            

        public int DepartmentId { get; set; }
        public DepartmentExcel Department { get; set; }

        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public DateTime JoinDate { get; set; }        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }            
        public bool IsActive { get; set; }             

        public ICollection<Attendance> Attendances { get; set; }
    }
}