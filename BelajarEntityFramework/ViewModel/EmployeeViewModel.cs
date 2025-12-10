namespace BelajarEntityFramework.ViewModels
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public DateTime JoinDate { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string EmployeeTypeName { get; set; }
        public int TotalAttendance { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
    }
}