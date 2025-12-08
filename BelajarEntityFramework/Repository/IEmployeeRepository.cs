using BelajarEntityFramework.GenericRepository;
using BelajarEntityFramework.Models;


namespace BelajarEntityFramework.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
     
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task<Employee?> GetEmployeeByIdAsync(int EmployeeID);

        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int Departmentid);
    }
}