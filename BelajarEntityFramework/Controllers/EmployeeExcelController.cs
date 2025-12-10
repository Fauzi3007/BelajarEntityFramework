using BelajarEntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using BelajarEntityFramework.Models;

namespace HRInternalApp.Controllers
{
    public class EmployeeExcelController : Controller
    {
        private readonly EFDbContext _context;
        // Define page size for pagination
        private const int PageSize = 7;

        public EmployeeExcelController(EFDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        // This action displays the list of employees with filtering and pagination.
        public async Task<IActionResult> Index(int? departmentId, int? employeeTypeId, bool? isActive, int page = 1)
        {
            // Retrieve filter dropdown data
            var departments = await _context.DepartmentsExcel.AsNoTracking().ToListAsync();
            var employeeTypes = await _context.EmployeeTypes.AsNoTracking().ToListAsync();

            var query = _context.EmployeesExcel
                .AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.EmployeeType)
                .Include(e => e.Attendances)
                .AsQueryable();

            // Apply filters if parameters are provided
            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }
            if (employeeTypeId.HasValue)
            {
                query = query.Where(e => e.EmployeeTypeId == employeeTypeId.Value);
            }
            if (isActive.HasValue)
            {
                query = query.Where(e => e.IsActive == isActive.Value);
            }

            // Get total record count for pagination
            int totalRecords = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalRecords / (double)PageSize);

            // Retrieve the paginated data
            var employees = await query
                .OrderBy(e => e.EmployeeId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            // Map the data to the view model
            var employeeViewModels = employees.Select(e => new EmployeeViewModel
            {
                EmployeeId = e.EmployeeId,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                DepartmentName = e.Department.Name,
                JoinDate = e.JoinDate,
                Salary = e.Salary,
                IsActive = e.IsActive,
                EmployeeTypeName = e.EmployeeType.TypeName,
                TotalAttendance = e.Attendances.Count,
                PresentDays = e.Attendances.Count(a => a.IsPresent),
                AbsentDays = e.Attendances.Count(a => !a.IsPresent)
            }).ToList();

            // Build the complete view model
            var model = new EmployeeListViewModel
            {
                Employees = employeeViewModels,
                CurrentPage = page,
                TotalPages = totalPages,
                SelectedDepartmentId = departmentId,
                SelectedEmployeeTypeId = employeeTypeId,
                SelectedStatus = isActive,
                DepartmentsExcel = departments,
                EmployeeTypes = employeeTypes
            };

            return View(model);
        }

        // GET: Employees/ExportToExcel
        // This action exports the filtered employee data to an Excel file.
        public async Task<IActionResult> ExportToExcel(int? departmentId, int? employeeTypeId, bool? isActive)
        {
            // Build the same query as in Index (without pagination)
            var query = _context.EmployeesExcel.AsNoTracking()
                .Include(e => e.Department)
                .Include(e => e.EmployeeType)
                .Include(e => e.Attendances)
                .AsQueryable();

            if (departmentId.HasValue)
            {
                query = query.Where(e => e.DepartmentId == departmentId.Value);
            }
            if (employeeTypeId.HasValue)
            {
                query = query.Where(e => e.EmployeeTypeId == employeeTypeId.Value);
            }
            if (isActive.HasValue)
            {
                query = query.Where(e => e.IsActive == isActive.Value);
            }

            var employees = await query.OrderBy(e => e.EmployeeId).ToListAsync();

            // Use EPPlus to generate an Excel file.
            // IMPORTANT: Set the EPPlus license context to non-commercial.
            ExcelPackage.License.SetNonCommercialPersonal("Yozora");

            // Create the Excel package and worksheet
            using (var package = new ExcelPackage())
            {
                // Create a worksheet.
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Set up header row.
                worksheet.Cells[1, 1].Value = "Employee ID";
                worksheet.Cells[1, 2].Value = "Full Name";
                worksheet.Cells[1, 3].Value = "Email";
                worksheet.Cells[1, 4].Value = "Department";
                worksheet.Cells[1, 5].Value = "Join Date";
                worksheet.Cells[1, 6].Value = "Salary";
                worksheet.Cells[1, 7].Value = "Is Active";
                worksheet.Cells[1, 8].Value = "Employee Type";
                worksheet.Cells[1, 9].Value = "Total Attendance";
                worksheet.Cells[1, 10].Value = "Present Days";
                worksheet.Cells[1, 11].Value = "Absent Days";

                // Fill in the employee data rows
                int row = 2;
                foreach (var e in employees)
                {
                    worksheet.Cells[row, 1].Value = e.EmployeeId;
                    worksheet.Cells[row, 2].Value = $"{e.FirstName} {e.LastName}";
                    worksheet.Cells[row, 3].Value = e.Email;
                    worksheet.Cells[row, 4].Value = e.Department.Name;
                    worksheet.Cells[row, 5].Value = e.JoinDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 6].Value = e.Salary;
                    worksheet.Cells[row, 7].Value = e.IsActive ? "Yes" : "No";
                    worksheet.Cells[row, 8].Value = e.EmployeeType.TypeName;
                    worksheet.Cells[row, 9].Value = e.Attendances.Count;

                    // Calculate attendance summary: count the number of days the employee was present and absent.
                    worksheet.Cells[row, 10].Value = e.Attendances.Count(a => a.IsPresent);
                    worksheet.Cells[row, 11].Value = e.Attendances.Count(a => !a.IsPresent);
                    row++;
                }

                // Auto-fit columns for better readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convert the package to a byte array
                var fileBytes = package.GetAsByteArray();

                // Return the Excel file for download
                // 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' is the MIME type for Excel files
                return File(
                    fileBytes, //Excel File data in Byte Array
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", //Excel Sheet Mime Type
                    "Employees.xlsx" //File Name
                );
            }
        }
    }
}