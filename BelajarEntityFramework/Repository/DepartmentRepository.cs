using BelajarEntityFramework.GenericRepository;
using BelajarEntityFramework.Models;

namespace BelajarEntityFramework.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EFDbContext context) : base(context) { }
    }
    //public class DepartmentRepository : IDepartmentRepository
    //{
    //    private readonly EFDbContext _context;

    //    public DepartmentRepository(EFDbContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IEnumerable<Department>> GetAllAsync()
    //    {
    //        return await _context.Departments.ToListAsync();
    //    }

    //    public async Task<Department?> GetByIdAsync(int DepartmentID)
    //    {
    //        var Department = await _context.Departments
    //                       .FirstOrDefaultAsync(d => d.DepartmentId == DepartmentID);

    //        return Department;
    //    }

    //    public async Task InsertAsync(Department Department)
    //    {
    //        await _context.Departments.AddAsync(Department);
    //    }

    //    public async Task UpdateAsync(Department Department)
    //    {
    //        _context.Departments.Update(Department);
    //    }

    //    public async Task DeleteAsync(int DepartmentId)
    //    {
    //        var Department = await _context.Departments.FindAsync(DepartmentId);
    //        if (Department != null)
    //        {
    //            _context.Departments.Remove(Department);
    //        }
    //    }
    //    public async Task SaveAsync()
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //}
}