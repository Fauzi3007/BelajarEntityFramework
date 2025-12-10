using BelajarEntityFramework.Services;
using Microsoft.AspNetCore.Mvc;

namespace BelajarEntityFramework.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService;

        // Dependency injection of OrderService.
        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Displays a list of all orders.
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // Displays detailed information about a specific order.
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // Generates and returns the PDF for a given order.
        public async Task<IActionResult> GeneratePdf(int id)
        {
            try
            {
                var pdfBytes = await _orderService.GenerateOrderPdfAsync(id);

                return File(pdfBytes, "application/pdf", $"Order_{id}.pdf");
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while generating the PDF.";
                return RedirectToAction("Details", new { id });
            }
        }
    }
}