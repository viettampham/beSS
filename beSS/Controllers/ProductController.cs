using System;
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
        
        [HttpGet("get-product-by-category-id/{id}")]
        public IActionResult GetProductBuCategoryID(Guid id)
        {
            var listProduct = _productService.GetProductByCategoryID(id);
            return Ok(listProduct);
        }
        
        [HttpGet("get-brand")]
        public IActionResult GetBrand()
        {
            var listBrand = _productService.Brand();
            return Ok(listBrand);
        }
        
        [HttpGet("get-product-by-brand/{brand}")]
        public IActionResult GetBrand(string brand)
        {
            var listBrand = _productService.GetProductByBrand(brand);
            return Ok(listBrand);
        }
        
        [HttpPost("add-product")]
        public IActionResult AddProduct(CreateProduct request)
        {
            var newProduct = _productService.CreateProduct(request);
            return Ok(newProduct);
        }
    }
}