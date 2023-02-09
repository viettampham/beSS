using beSS.Models.RequestModels;
using beSS.Services;
using Microsoft.AspNetCore.Mvc;

namespace beSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController:ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-product")]
        public IActionResult GetProduct()
        {
            var listProduct = _productService.GetProduct();
            return Ok(listProduct);
        }
        
        [HttpPost("add-product")]
        public IActionResult AddProduct(CreateProduct request)
        {
            var newProduct = _productService.CreateProduct(request);
            return Ok(newProduct);
        }
    }
}