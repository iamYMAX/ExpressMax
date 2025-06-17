using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
// Add this using statement for [Authorize]
using Microsoft.AspNetCore.Authorization;
// Assuming a YourAppName.Web.Models namespace for ErrorViewModel if it exists
// using YourAppName.Web.Models;

namespace YourAppName.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize] // Protect this action
        public IActionResult Privacy()
        {
            return View();
        }

        // Assuming ErrorViewModel is defined, possibly in Models/ErrorViewModel.cs
        // or can be a generic model if a specific ErrorViewModel.cs wasn't created by the template.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Using a simple anonymous object for the Error view if ErrorViewModel is not critical here.
            return View(new { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
