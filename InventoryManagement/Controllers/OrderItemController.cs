using InventoryManagement.Dto.Response;
using InventoryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly OrderItemService _service;

        public OrderItemController(OrderItemService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItems()
        {
            var response = await _service.GetOrderItems();

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(int id)
        {
            var response = await _service.GetOrderItem(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }
    }
}
