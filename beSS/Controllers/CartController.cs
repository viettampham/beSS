using System;
using beSS.Models.RequestModels;
using beSS.Services;
using Microsoft.AspNetCore.Mvc;

namespace beSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController:ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("get-cart-by-user")]
        public IActionResult GetCartByUser(Guid id)
        {
            var targetCart = _cartService.GetCartByUser(id);
            return Ok(targetCart);
        }

        [HttpGet("get-all-cart")]
        public IActionResult GetCart()
        {
            return Ok(_cartService.GetList());
            
        }

        [HttpPost("add-cart")]
        public IActionResult AddCart(Guid id)
        {
            var newCart = _cartService.CreateCart(id);
            return Ok(newCart);
        }

        [HttpDelete("delete-cart")]
        public IActionResult DeleteCart(Guid id)
        {
            return Ok(_cartService.DeleteCart(id));
        }
    }
}