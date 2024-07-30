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
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repository;

        public CategoryController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryReponse>>> GetCategories()
        {
            var categoryResponse = new List<CategoryReponse>();

            var categories = await _repository.GetAllAsync();

            foreach (var item in categories)
            {
                categoryResponse.Add(CategoryMapper.CategoryMapperView(item));
            }

            return categoryResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReponse>> GetCategory(int id)
        {
            var categoryResponse = new CategoryReponse();

            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            categoryResponse = CategoryMapper.CategoryMapperView(category);

            return categoryResponse;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CategoryRequest categoryRequest)
        {
            var category = CategoryMapper.CategoryMapperDto(categoryRequest);

            var createdCategory = await _repository.AddAsync(category);
            
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            await _repository.UpdateAsync(CategoryMapper.CategoryMapperDto(categoryRequest));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
