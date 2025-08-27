using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;
using InventoryManagement.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly OrderService _service;

        private readonly IDistributedCache _cache;

        public OrderController(OrderService service, IDistributedCache cache)
        {
            _service = service;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders()
        {
            string cacheKey = "orders:all";
            var cachedOrders = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedOrders))
            {
                var orders = JsonSerializer.Deserialize<List<OrderResponse>>(cachedOrders);
                return Ok(orders);
            }

            var response = await _service.GetOrders();

            var serializedOrders = JsonSerializer.Serialize(response);

            await _cache.SetStringAsync(cacheKey, serializedOrders, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var response = new OrderResponse();

            string cacheKey = $"order:{id}";
            var cachedOrder = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedOrder))
            {
                var product = JsonSerializer.Deserialize<Product>(cachedOrder);
                return Ok(product);
            }

            response = await _service.GetOrder(id);

            var serializedProduct = JsonSerializer.Serialize(response);
            await _cache.SetStringAsync(cacheKey, serializedProduct, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

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

            string cacheKey = $"order:{id}";
            var serializedOrder = JsonSerializer.Serialize(response);

            await _cache.SetStringAsync(cacheKey, serializedOrder, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderResponse>> DeleteOrder(int id)
        {
            var response = await _service.DeleteOrder(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            string cacheKey = $"order:{id}";
            await _cache.RemoveAsync(cacheKey);

            return Ok(response);
        }
    }
}
