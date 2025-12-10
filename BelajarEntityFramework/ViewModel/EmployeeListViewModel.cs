using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModels;

namespace BelajarEntityFramework.ViewModels
{
    public class EmployeeListViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? SelectedDepartmentId { get; set; }
        public int? SelectedEmployeeTypeId { get; set; }
        public bool? SelectedStatus { get; set; }

        public IEnumerable<DepartmentExcel> DepartmentsExcel { get; set; }
        public IEnumerable<EmployeeType> EmployeeTypes { get; set; }
    }
}