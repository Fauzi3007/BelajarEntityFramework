using BelajarEntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BelajarEntityFramework.ViewModels
{
    public class EmployeeEditViewModel : EmployeeCreateViewModel
    {
        public int EmployeeId { get; set; }
        public IEnumerable<SelectListItem>? States { get; set; }
        public IEnumerable<SelectListItem>? Cities { get; set; }
    }
}