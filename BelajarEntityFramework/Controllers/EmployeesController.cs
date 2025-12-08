using BelajarEntityFramework.GenericRepository;
using BelajarEntityFramework.Models;
using BelajarEntityFramework.Repository;
using BelajarEntityFramework.UOW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CRUDinCoreMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EFDbContext _context;

        private readonly IGenericRepository<Employee> _repository;

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork,IGenericRepository<Employee> repository, IEmployeeRepository employeeRepository, EFDbContext context)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(Convert.ToInt32(id));
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Begin The Tranaction
                    _unitOfWork.CreateTransaction();
                    //Use Generic Reposiory to Insert a new employee
                    await _unitOfWork.Employees.InsertAsync(employee);
                    //Call SaveAsync to Insert the data into the database
                    //await _repository.SaveAsync();
                    //Save Changes to database
                    await _unitOfWork.Save();
                    //Commit the Changes to database
                    _unitOfWork.Commit();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    //Rollback Transaction
                    _unitOfWork.Rollback();
                    //Log The Exception
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _repository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //Begin The Tranaction
                    _unitOfWork.CreateTransaction();
                    //Use Generic Reposiory to Insert a new employee
                    await _unitOfWork.Employees.UpdateAsync(employee);
                    //Save Changes to database
                    await _unitOfWork.Save();
                    //Commit the Changes to database
                    _unitOfWork.Commit();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Rollback Transaction
                    _unitOfWork.Rollback();
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "DepartmentId", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(Convert.ToInt32(id));

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            if (employee != null)
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}