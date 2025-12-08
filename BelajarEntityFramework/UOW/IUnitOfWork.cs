using BelajarEntityFramework.Repository;
namespace BelajarEntityFramework.UOW
{
    public interface IUnitOfWork
    {
        EmployeeRepository Employees { get; }
        DepartmentRepository Departments { get; }

        void CreateTransaction();

        void Commit();

        void Rollback();

        Task Save();
    }
}