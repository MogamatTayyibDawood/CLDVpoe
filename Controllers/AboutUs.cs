using Microsoft.AspNetCore.Mvc;

namespace CLDVpoe.Controllers
{
    public class AboutUs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
