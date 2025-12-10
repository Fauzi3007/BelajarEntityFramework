using BelajarEntityFramework.ViewModels;

namespace BelajarEntityFramework.ViewModels
{
    public class ProductImportViewModel
    {
        public IFormFile FileUpload { get; set; }

        public string Message { get; set; }

        public List<ProductImportResult> ImportResults { get; set; } = new List<ProductImportResult>();
    }
}