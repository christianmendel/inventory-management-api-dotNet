using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _repository;

        public ProductController(ProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetProducts()
        {
            var productResponse = new List<ProductResponse>();

            var products = await _repository.GetAllAsync();
            
            foreach (var item in products)
            {
                productResponse.Add(ProductMapper.ProductMapperView(item));
            }
            
            return productResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(int id)
        {
            var productResponse = new ProductResponse();

            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            productResponse = ProductMapper.ProductMapperView(product);

            return productResponse;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(ProductRequest productRequest)
        {
            var product = ProductMapper.ProductMapperDto(productRequest);
            var createdProduct = await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequest productRequest)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _repository.UpdateAsync(ProductMapper.ProductMapperDto(productRequest));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
