using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModels;

namespace BelajarEntityFramework.ViewModels
{
    public class ProductExcelListViewModel
    {
        public IEnumerable<ProductExcel> Products { get; set; }
        public string? SearchName { get; set; }
        public int? SelectedCategoryId { get; set; }
        public int? SelectedSupplierId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}