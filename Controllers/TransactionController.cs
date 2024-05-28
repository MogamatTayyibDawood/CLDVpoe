using CLDVpoe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CLDVpoe.Controllers
{
    public class TransactionController : Controller
    {
        private readonly transactionTable _transactionTable;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(transactionTable transactionTable, IHttpContextAccessor httpContextAccessor)
        {
            _transactionTable = transactionTable;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public IActionResult PlaceOrder(int productID)
        {
            try
            {
                int? userId = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
                if (userId.HasValue)
                {
                    transactionTable transaction = new transactionTable
                    {
                        UserID = userId.Value,
                        ProductID = productID,
                        OrderStatus = "Pending"
                    };

                    int rowsAffected = transactionTable.InsertTransaction(transaction);
                    if (rowsAffected > 0)
                    {
                        TempData["OrderSuccessMessage"] = "Your order has been placed successfully!";
                        return RedirectToAction("MyWork", "Product");
                    }
                    else
                    {
                        return View("OrderFailed");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult UpdateStatus(int transactionID)
        {
            var result = transactionTable.UpdateOrderStatus(transactionID, "Success");

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Order processed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error processing order.";
            }

            return RedirectToAction("MyWork", "Product");
        }
    }
}
