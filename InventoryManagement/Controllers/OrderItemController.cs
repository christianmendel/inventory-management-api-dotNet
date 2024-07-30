using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly OrderItemRepository _repository;

        public OrderItemController(OrderItemRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemResponse>>> GetOrderItems()
        {
            var orderItemResponse = new List<OrderItemResponse>();

            var orderItems = await _repository.GetAllAsync();

            foreach (var item in orderItems)
            {
                orderItemResponse.Add(OrderItemMapper.OrderItemMapperView(item));
            }

            return orderItemResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemResponse>> GetOrderItem(int id)
        {
            var orderItemResponse = new OrderItemResponse();

            var orderItem = await _repository.GetByIdAsync(id);
            if (orderItem == null) return NotFound();

            orderItemResponse = OrderItemMapper.OrderItemMapperView(orderItem);

            return orderItemResponse;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemRequest orderItemRequest)
        {
            var orderItem = await _repository.GetByIdAsync(id);
            if (orderItem == null) return NotFound();

            await _repository.UpdateAsync(OrderItemMapper.OrderItemMapperDto(orderItemRequest));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var orderItem = await _repository.GetByIdAsync(id);
            if (orderItem == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
