using BelajarEntityFramework.Models;
using BelajarEntityFramework.Repository;
using BelajarEntityFramework.UOW;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRUDinCoreMVC.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public EFDbContext Context = null;

        //The following varibale will hold the Transaction Instance
        private IDbContextTransaction? _objTran = null;

        public EmployeeRepository Employees { get; private set; }

        public DepartmentRepository Departments { get; private set; }

        public UnitOfWork(EFDbContext _Context)
        {
            Context = _Context;
            Employees = new EmployeeRepository(Context);
            Departments = new DepartmentRepository(Context);
        }

   
        public void CreateTransaction()
        {
            _objTran = Context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _objTran?.Commit();
        }

        public void Rollback()
        {
            _objTran?.Rollback();

            _objTran?.Dispose();
        }
        public async Task Save()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}