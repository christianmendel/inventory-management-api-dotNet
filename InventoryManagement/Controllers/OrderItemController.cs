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
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItems = await _repository.GetAllAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await _repository.GetByIdAsync(id);
            if (orderItem == null) return NotFound();
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            var createdOrderItem = await _repository.AddAsync(orderItem);
            return CreatedAtAction(nameof(GetOrderItem), new { id = createdOrderItem.Id }, createdOrderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id) return BadRequest();
            await _repository.UpdateAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
