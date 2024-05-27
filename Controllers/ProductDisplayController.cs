using Microsoft.AspNetCore.Mvc;
using CLDVpoe.Models;

namespace CLDVpoe.Controllers
{
    public class ProductDisplayController : Controller
    {
        private readonly productTable _productTable;

        public ProductDisplayController(productTable productTable)
        {
            _productTable = productTable;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = productTable.GetAllProducts();
            return View(products);
        }
    }
}
