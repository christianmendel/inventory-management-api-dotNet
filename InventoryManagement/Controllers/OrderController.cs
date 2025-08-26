using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            var response = await _service.GetOrders();

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var response = await _service.GetOrder(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder(OrderRequest orderRequest)
        {
            var response = await _service.CreateOrder(orderRequest);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderResponse>> UpdateOrder(int id, OrderRequest orderRequest)
        {
            var response = await _service.UpdateOrder(id, orderRequest);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> DeleteOrder(int id)
        {
            var response = await _service.DeleteOrder(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }
    }
}
