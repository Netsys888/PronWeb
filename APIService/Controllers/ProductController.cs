using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIService.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly SapOrderService _sapOrderService;

        public ProductController(SapOrderService sapOrderService)
        {
            _sapOrderService = sapOrderService;
        }

        [HttpGet]
        public IActionResult GetProduct()
        {
            return Ok("Product Data");
        }

        [HttpPost("create")]
        public IActionResult CreateOrder()
        {
            _sapOrderService.CreateOrder();

            return Ok("Order created");
        }
    }
}
