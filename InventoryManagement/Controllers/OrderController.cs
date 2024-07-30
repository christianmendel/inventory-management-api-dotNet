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
    public class OrderController : Controller
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders()
        {
            var orderResponse = new List<OrderResponse>();

            var orders = await _repository.GetAllAsync();

            foreach (var item in orders)
            {
                orderResponse.Add(OrderMapper.OrderMapperView(item));
            }

            return orderResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var orderResponse = new OrderResponse();

            var order = await _repository.GetByIdAsync(id);
            if (order == null) return NotFound();

            orderResponse = OrderMapper.OrderMapperView(order);

            return orderResponse;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderRequest orderRequest)
        {
            var order = OrderMapper.OrderMapperDto(orderRequest);
            var orderItems = new List<OrderItem>();
            
            foreach (var item in orderRequest.orderItems)
            {
                orderItems.Add(OrderItemMapper.OrderItemMapperDto(item));
            }

            order.AddListOrderItems(orderItems);

            var createdOrder = await _repository.AddAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, OrderRequest orderRequest)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _repository.UpdateAsync(OrderMapper.OrderMapperDto(orderRequest));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
