using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Service;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var response = await _service.GetProducts();

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            var response = await _service.GetProduct(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> CreateProduct(ProductRequest productRequest)
        {
            var response = await _service.CreateProduct(productRequest);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(int id, ProductRequest productRequest)
        {
            var response = await _service.UpdateProduct(id, productRequest);

            if (!response.IsValid())
                 return BadRequest(response.Notifications);

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductResponse>> DeleteProduct(int id)
        {
            var response = await _service.DeleteProduct(id);
            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }
    }
}
