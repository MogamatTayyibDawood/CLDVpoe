using Microsoft.AspNetCore.Mvc;
using CLDVpoe.Models;
using Microsoft.AspNetCore.Http; // Add this namespace for HttpContext.Session

namespace CLDVpoe.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = model.AuthenticateUser(model.UserEmail, model.Password);
                if (userId != -1)
                {
                    // Set user ID in session
                    _httpContextAccessor.HttpContext.Session.SetInt32("UserID", userId);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid email or password.");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            // Clear session on logout
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
