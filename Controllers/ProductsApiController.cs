using Microsoft.AspNetCore.Mvc;
using AssignmentFinals.Services;

namespace AssignmentFinals.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ProductXmlService _service;

        public ProductsApiController(ProductXmlService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var product = _service
                .GetProducts()
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("lowstock")]
        public IActionResult GetLowStock()
        {
            var products = _service
                .GetProducts()
                .Where(p => p.Quantity > 0 && p.Quantity < 10);

            return Ok(products);
        }
    }
}