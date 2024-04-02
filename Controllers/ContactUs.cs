using Microsoft.AspNetCore.Mvc;

namespace CLDVpoe.Controllers
{
    public class ContactUs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
