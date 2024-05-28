using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using CLDVpoe.Models;
using Microsoft.AspNetCore.Http;

namespace CLDVpoe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly productTable _productTable;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, productTable productTable)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _productTable = productTable;
        }

        public IActionResult Index()
        {
            // Retrieve all products from the database
            List<productTable> products = productTable.GetAllProducts();

            // Retrieve userID and email from session
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            string userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");

            // Pass products, userID, and userEmail to the view
            ViewData["Products"] = products;
            ViewData["UserID"] = userID;
            ViewData["UserEmail"] = userEmail;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}