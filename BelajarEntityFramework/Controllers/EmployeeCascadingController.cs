using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BelajarEntityFramework.Controllers
{
    public class EmployeeCascadingController : Controller
    {
        private readonly EFDbContext _context;

        public EmployeeCascadingController(EFDbContext context)
        {
            _context = context;
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            var viewModel = new EmployeeCreateViewModel
            {
                Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName")
            };

            return View(viewModel);
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Map the view model to the domain model
                var employee = new EmployeeCascading
                {
                    FullName = viewModel.FullName,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    Department = viewModel.Department,
                    CountryId = Convert.ToInt32(viewModel.CountryId),
                    StateId = Convert.ToInt32(viewModel.StateId),
                    CityId = Convert.ToInt32(viewModel.CityId)
                };

                _context.EmployeesCascading.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate the Countries dropdown on error
            viewModel.Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName", viewModel.CountryId);
            return View(viewModel);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.EmployeesCascading.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            // Map domain model to view model and preload dropdowns
            var viewModel = new EmployeeEditViewModel
            {
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Email = employee.Email,
                Phone = employee.Phone,
                Department = employee.Department,
                CountryId = employee.CountryId,
                StateId = employee.StateId,
                CityId = employee.CityId,
                Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName", employee.CountryId)
                // States and Cities will be loaded via AJAX in the view for prepopulation
            };

            return View(viewModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.EmployeesCascading.FindAsync(viewModel.EmployeeId);
                if (employee == null)
                {
                    return NotFound();
                }

                // Map view model values to the domain model
                employee.FullName = viewModel.FullName;
                employee.Email = viewModel.Email;
                employee.Phone = viewModel.Phone;
                employee.Department = viewModel.Department;
                employee.CountryId = Convert.ToInt32(viewModel.CountryId);
                employee.StateId = Convert.ToInt32(viewModel.StateId);
                employee.CityId = Convert.ToInt32(viewModel.CityId);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopulate Countries on error
            viewModel.Countries = new SelectList(_context.Countries.AsNoTracking().ToList(), "CountryId", "CountryName", viewModel.CountryId);
            return View(viewModel);
        }

        // GET: Employees/Index
        public async Task<IActionResult> Index()
        {
            var employees = await _context.EmployeesCascading
                .Include(e => e.Country)
                .Include(e => e.State)
                .Include(e => e.City)
                .AsNoTracking()
                .ToListAsync();
            return View(employees);
        }

        // JSON endpoint: Get States for a Country
        [HttpGet]
        public IActionResult GetStates(int countryId)
        {
            var states = _context.States.AsNoTracking().Where(s => s.CountryId == countryId).ToList();
            return Json(new SelectList(states, "StateId", "StateName"));
        }

        // JSON endpoint: Get Cities for a State
        [HttpGet]
        public IActionResult GetCities(int stateId)
        {
            var cities = _context.Cities.AsNoTracking().Where(c => c.StateId == stateId).ToList();
            return Json(new SelectList(cities, "CityId", "CityName"));
        }
    }
}