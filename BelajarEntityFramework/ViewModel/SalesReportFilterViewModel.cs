namespace BelajarEntityFramework.ViewModels
{
    public class SalesReportFilterViewModel
    {
        public string DateRangeOption { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Category { get; set; }
        public string OrderStatus { get; set; }

        public List<string> Categories { get; set; }
        public List<string> OrderStatuses { get; set; }

        public List<string> DateRangeOptions { get; set; } = new List<string>
        {
            "All",
            "Today",
            "Yesterday",
            "Last 7 Days",
            "This Month",
            "Last Month",
            "Last 3 Months",
            "Last 6 Months",
            "Last 1 Year"
        };
    }
}