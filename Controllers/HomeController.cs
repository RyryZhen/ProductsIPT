using Microsoft.AspNetCore.Mvc;
using AssignmentFinals.Models;
using AssignmentFinals.Services;

namespace AssignmentFinals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductXmlService _service;

        public HomeController(ProductXmlService service)
        {
            _service = service;
        }

        // READ: Display all products from XML
        public IActionResult Index()
        {
            var products = _service.GetProducts();
            return View(products);
        }

        // CREATE (optional UI page)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _service.AddProduct(product);
            return RedirectToAction("Index");
        }

        // EDIT
        public IActionResult Edit(string id)
        {
            var product = _service.GetProductById(id);
            return View(product);
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