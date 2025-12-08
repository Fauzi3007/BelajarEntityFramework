using BelajarEntityFramework.GenericRepository;
using BelajarEntityFramework.Models;
using BelajarEntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace BelajarEntityFramework.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EFDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Include(e => e.Department).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int EmployeeID)
        {
            var employee = await _context.Employees
               .Include(e => e.Department)
               .FirstOrDefaultAsync(m => m.EmployeeId == EmployeeID);

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int DepartmentId)
        {
            return await _context.Employees
                .Where(emp => emp.DepartmentId == DepartmentId)
                .Include(e => e.Department).ToListAsync();
        }
    }
}