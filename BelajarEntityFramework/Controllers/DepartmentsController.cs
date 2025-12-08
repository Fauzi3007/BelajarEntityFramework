using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BelajarEntityFramework.Models;
using BelajarEntityFramework.Repository;
using BelajarEntityFramework.GenericRepository;

namespace BelajarEntityFramework.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly EFDbContext _context;
        private readonly IGenericRepository<Department> _repository;

        public DepartmentsController(EFDbContext context, IGenericRepository<Department> repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var employees = await _repository.GetAllAsync();
            return View(employees);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                await _repository.InsertAsync(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.UpdateAsync(department);
                    await _repository.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var emp = await _repository.GetByIdAsync(department.DepartmentId);
                    if (emp == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _repository.GetByIdAsync(Convert.ToInt32(id));
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _repository.GetByIdAsync(id);
            if (department != null)
            {
                await _repository.DeleteAsync(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
