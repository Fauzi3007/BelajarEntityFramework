using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModels;
namespace BelajarEntityFramework.Models
{
    public class SalesReportViewModel
    {
        public SalesReportFilterViewModel Filter { get; set; }

        // Summary values
        public int TotalOrders { get; set; }
        public int TotalPending { get; set; }
        public int TotalCompleted { get; set; }
        public int TotalCanceled { get; set; }
        public decimal TotalSales { get; set; }

        // Orders for the current page
        public List<OrderChart> Orders { get; set; }

        // Bar Chart Data (JSON)
        public string ChartDataJson { get; set; }

        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}