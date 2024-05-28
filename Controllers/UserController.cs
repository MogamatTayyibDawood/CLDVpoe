using Microsoft.AspNetCore.Mvc;
using CLDVpoe.Models;
using Microsoft.AspNetCore.Http;

namespace CLDVpoe.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(userTable model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Check if the email already exists
                    bool emailExists = userTable.CheckExistingEmail(model.UserEmail);
                    if (emailExists)
                    {
                        ModelState.AddModelError("", "Email address already exists. Please use a different email.");
                        return View(model);
                    }

                    int result = model.InsertUser();
                    if (result > 0)
                    {
                        TempData["SuccessMessage"] = "Registration successful. Please log in.";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred during user insertion: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred. Please try again.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = model.AuthenticateUser(model.UserEmail, model.Password);
                if (userId > 0)
                {
                    // Store user ID in session
                    HttpContext.Session.SetInt32("UserID", userId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }

            return View(model);
        }
    }
}