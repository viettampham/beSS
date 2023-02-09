using System;
using beSS.Models.RequestModels;
using beSS.Services;
using Microsoft.AspNetCore.Mvc;

namespace beSS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController:ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet("admin-get-bill")]
        public IActionResult GetAll()
        {
            return Ok(_billService.GetAll());
        }
        
        [HttpGet("user-get-bill")]
        public IActionResult GetBillByUser(Guid id)
        {
            return Ok(_billService.GetAllBillByUser(id));
        }

        [HttpPost("create-bill")]
        public IActionResult CreateBill(CreateBillRequest request)
        {
            return Ok(_billService.CreateBill(request));
        }

        [HttpDelete("delete-bill")]
        public IActionResult DeleteBill(Guid id)
        {
            return Ok(_billService.DeleteBill(id));
        }
    }
}