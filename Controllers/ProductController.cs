using Microsoft.AspNetCore.Mvc;
using CLDVpoe.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace CLDVpoe.Controllers
{
    public class ProductController : Controller
    {
        private readonly productTable _productTable;
        private readonly transactionTable _transactionTable;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(productTable productTable, transactionTable transactionTable, IHttpContextAccessor httpContextAccessor)
        {
            _productTable = productTable;
            _transactionTable = transactionTable;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new productTable
                {
                    Name = model.Name,
                    Price = model.Price,
                    Category = model.Category,
                    Availability = model.Availability,
                };

                var result = _productTable.InsertProduct(product);

                if (result > 0)
                {
                    ViewBag.Message = "Product added successfully.";
                    return RedirectToAction("MyWork");
                }
                else
                {
                    ViewBag.Message = "Error adding product.";
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult MyWork()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            if (userID.HasValue)
            {
                var products = ProductDisplayModel.SelectProducts();
                var transactions = _transactionTable.GetTransactions(userId: userID.Value);

                ViewBag.UserID = userID.Value;
                ViewBag.Transactions = transactions;
                return View(products);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
