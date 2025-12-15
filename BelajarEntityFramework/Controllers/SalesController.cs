using BelajarEntityFramework.Models;
using BelajarEntityFramework.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace ECommerceSalesApp.Controllers
{
    public class SalesController : Controller
    {
        private readonly EFDbContext _context;

        // Number of orders to display per page
        private const int PageSize = 5;

        public SalesController(EFDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string dateRangeOption, string category, string orderStatus, int page = 1)
        {
            // Default to "This Month" if no date range option is provided.
            if (string.IsNullOrEmpty(dateRangeOption))
                dateRangeOption = "This Month";

            // Determine the start and end dates based on the selected date range option.
            SetDatesFromOption(dateRangeOption, out DateTime startDate, out DateTime endDate);

            // Build the query for orders including related Customer, OrderItems, and Product data.
            var ordersQuery = _context.OrdersChart
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            // Filter by order status if provided.
            if (!string.IsNullOrEmpty(orderStatus) && Enum.TryParse<OrderStatus>(orderStatus, out var status))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderStatus == status);
            }

            // Filter by product category if provided.
            if (!string.IsNullOrEmpty(category))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderItems.Any(oi => oi.Product.Category == category));
            }

            // Execute query and load orders into memory.
            var allOrders = await ordersQuery.ToListAsync();

            // Calculate summary statistics.
            int totalOrders = allOrders.Count;
            int totalPending = allOrders.Count(o => o.OrderStatus == OrderStatus.Pending);
            int totalCompleted = allOrders.Count(o => o.OrderStatus == OrderStatus.Completed);
            int totalCanceled = allOrders.Count(o => o.OrderStatus == OrderStatus.Cancelled);
            decimal totalSales = allOrders.Sum(o => o.TotalAmount);

            // ---------------------------------------------------------------------
            // BAR CHART DATA PREPARATION SECTION
            // ---------------------------------------------------------------------
            // Group orders by day (using OrderDate.Date) to get daily aggregates.
            var dayGroups = allOrders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    Day = g.Key,
                    Pending = g.Count(x => x.OrderStatus == OrderStatus.Pending),
                    Completed = g.Count(x => x.OrderStatus == OrderStatus.Completed),
                    Cancelled = g.Count(x => x.OrderStatus == OrderStatus.Cancelled),
                    TotalSales = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Day)
                .ToList();

            // Create a list of formatted date labels (e.g., "2025-02-15") for the X-axis.
            var labels = dayGroups.Select(dg => dg.Day.ToString("yyyy-MM-dd")).ToList();

            // Prepare datasets for the stacked bar chart (order counts by status).
            var datasetPending = dayGroups.Select(dg => (double)dg.Pending).ToList();
            var datasetCompleted = dayGroups.Select(dg => (double)dg.Completed).ToList();
            var datasetCancelled = dayGroups.Select(dg => (double)dg.Cancelled).ToList();

            // Prepare dataset for the line chart (total sales per day).
            var datasetSales = dayGroups.Select(dg => (double)dg.TotalSales).ToList();

            // Build the complete chart data object containing:
            // - labels: the dates (x-axis)
            // - datasets: an array with 3 bar datasets (stacked) and 1 line dataset.
            var chartData = new
            {
                labels,
                datasets = new object[]
                {
                    // Bar dataset for Pending orders
                    new
                    {
                        label = "Pending",
                        backgroundColor = "#17a2b8",
                        data = datasetPending,
                        stack = "Orders"
                    },
                    // Bar dataset for Cancelled orders
                    new
                    {
                        label = "Cancelled",
                        backgroundColor = "#dc3545",
                        data = datasetCancelled,
                        stack = "Orders"
                    },
                    // Bar dataset for Completed orders
                    new
                    {
                        label = "Completed",
                        backgroundColor = "#28a745",
                        data = datasetCompleted,
                        stack = "Orders"
                    },
                    // Line dataset for Total Sales
                    new
                    {
                        label = "Total Sales ($)",
                        type = "line",
                        borderColor = "#ffc107",
                        backgroundColor = "#ffc107",
                        data = datasetSales,
                        fill = false,
                        yAxisID = "ySales"
                    }
                }
            };

            // Serialize the chart data object into JSON format for use in JavaScript (Chart.js).
            var chartDataJson = System.Text.Json.JsonSerializer.Serialize(chartData);

            // ---------------------------------------------------------------------
            // PAGINATION: Calculate and apply paging on orders list.
            // ---------------------------------------------------------------------
            int totalPages = (int)Math.Ceiling(totalOrders / (double)PageSize);
            var pagedOrders = allOrders
                .OrderBy(o => o.OrderDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Prepare the filter view model with available filter options.
            var filter = new SalesReportFilterViewModel
            {
                DateRangeOption = dateRangeOption,
                Category = category,
                OrderStatus = orderStatus,
                Categories = await _context.ProductsChart.Select(p => p.Category).Distinct().ToListAsync(),
                OrderStatuses = Enum.GetNames(typeof(OrderStatus)).ToList()
            };

            // Assemble the main view model with summary data, paged orders, and chart JSON.
            var vm = new SalesReportViewModel
            {
                Filter = filter,
                TotalOrders = totalOrders,
                TotalPending = totalPending,
                TotalCompleted = totalCompleted,
                TotalCanceled = totalCanceled,
                TotalSales = totalSales,
                Orders = pagedOrders,
                CurrentPage = page,
                TotalPages = totalPages,
                ChartDataJson = chartDataJson
            };

            // Return the view with the complete view model.
            return View(vm);
        }

        // GET: /Sales/ExportToPdf
        [HttpGet]
        public async Task<IActionResult> ExportToPdf(string dateRangeOption, string category, string orderStatus)
        {
            // Default to "This Month" if no date range option is provided.
            if (string.IsNullOrEmpty(dateRangeOption))
                dateRangeOption = "This Month";

            // Determine the start and end dates for the report based on the selected date range option.
            SetDatesFromOption(dateRangeOption, out DateTime startDate, out DateTime endDate);

            // Build the query for orders with related data based on the selected filters.
            var ordersQuery = _context.OrdersChart
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Product)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            // Filter by order status if provided.
            if (!string.IsNullOrEmpty(orderStatus) && Enum.TryParse<OrderStatus>(orderStatus, out var status))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderStatus == status);
            }

            // Filter by category if provided.
            if (!string.IsNullOrEmpty(category))
            {
                ordersQuery = ordersQuery.Where(o => o.OrderItems.Any(oi => oi.Product.Category == category));
            }

            // Retrieve the filtered orders sorted by date.
            var allOrders = await ordersQuery.OrderBy(o => o.OrderDate).ToListAsync();

            // Calculate summary statistics for the PDF report.
            int totalOrders = allOrders.Count;
            int totalPending = allOrders.Count(o => o.OrderStatus == OrderStatus.Pending);
            int totalCompleted = allOrders.Count(o => o.OrderStatus == OrderStatus.Completed);
            int totalCanceled = allOrders.Count(o => o.OrderStatus == OrderStatus.Cancelled);
            decimal totalSales = allOrders.Sum(o => o.TotalAmount);

            // Prepare the filter view model for the PDF report.
            var filter = new SalesReportFilterViewModel
            {
                DateRangeOption = dateRangeOption,
                StartDate = startDate,
                EndDate = endDate,
                Category = category,
                OrderStatus = orderStatus
            };

            // Assemble the view model for the PDF report.
            var vm = new SalesReportViewModel
            {
                Filter = filter,
                TotalOrders = totalOrders,
                TotalPending = totalPending,
                TotalCompleted = totalCompleted,
                TotalCanceled = totalCanceled,
                TotalSales = totalSales,
                Orders = allOrders  // In the PDF, show all orders (no paging)
            };

            // Return the PDF result using Rotativa:
            // - "SalesReportPdf" is the view that will be rendered as PDF.
            // - FileName, PageSize, and PageOrientation are configured for proper formatting.
            return new ViewAsPdf("SalesReportPdf", vm)
            {
                FileName = $"SalesReport_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }

        // AJAX endpoint to return product details for a given order.
        [HttpGet]
        public IActionResult GetOrderProducts(int orderId)
        {
            // Retrieve order items along with their product details.
            var items = _context.OrderItemsChart
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == orderId)
                .ToList();

            // Return the partial view that renders product details.
            return PartialView("_OrderProductsPartial", items);
        }

        // Helper method to set the start and end dates based on a selected date range option.
        private void SetDatesFromOption(string dateRangeOption, out DateTime startDate, out DateTime endDate)
        {
            // If "All" is selected, return the widest possible date range.
            if (string.Equals(dateRangeOption, "All", StringComparison.OrdinalIgnoreCase))
            {
                startDate = DateTime.MinValue;
                endDate = DateTime.MaxValue;
                return;
            }

            // By default, use today's date as the end date.
            endDate = DateTime.Today;
            switch (dateRangeOption)
            {
                case "Today":
                    startDate = DateTime.Today;
                    endDate = DateTime.Today;
                    break;
                case "Yesterday":
                    startDate = DateTime.Today.AddDays(-1);
                    endDate = DateTime.Today.AddDays(-1);
                    break;
                case "Last 7 Days":
                    startDate = DateTime.Today.AddDays(-6);
                    break;
                case "This Month":
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    break;
                case "Last Month":
                    var lastMonth = DateTime.Today.AddMonths(-1);
                    startDate = new DateTime(lastMonth.Year, lastMonth.Month, 1); //1st January, 2025
                    endDate = startDate.AddMonths(1).AddDays(-1); //31st january
                    break;
                case "Last 3 Months":
                    startDate = DateTime.Today.AddMonths(-3);
                    break;
                case "Last 6 Months":
                    startDate = DateTime.Today.AddMonths(-6);
                    break;
                case "Last 1 Year":
                    startDate = DateTime.Today.AddYears(-1);
                    break;
                default:
                    startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    break;
            }
        }
    }
}