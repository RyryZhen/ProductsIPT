using Microsoft.AspNetCore.Mvc;
using AssignmentFinals.Models;
using AssignmentFinals.Services;

namespace AssignmentFinals.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductXmlService _service;

        public ProductsController(ProductXmlService service)
        {
            _service = service;
        }

        // READ
        public IActionResult Index()
        {
            return View(_service.GetProducts());
        }

        // CREATE
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            // AUTO-GENERATE ID
            product.ProductID = GenerateProductId();

            _service.AddProduct(product);

            return RedirectToAction("Index");
        }
        private string GenerateProductId()
        {
            var products = _service.GetProducts();

            var numericIds = products
                .Where(p => !string.IsNullOrWhiteSpace(p.ProductID))
                .Select(p =>
                {
                    int number;
                    return int.TryParse(p.ProductID.Replace("P", ""), out number) ? number : 0;
                })
                .ToList();

            int max = numericIds.Any() ? numericIds.Max() : 0;

            return $"P{(max + 1).ToString("D3")}";
        }

        // EDIT
        public IActionResult Edit(string id)
        {
            return View(_service.GetProductById(id));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _service.UpdateProduct(product);
            return RedirectToAction("Index");
        }

        // DELETE
        [HttpPost]
        public IActionResult Delete(string id)
        {
            _service.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}