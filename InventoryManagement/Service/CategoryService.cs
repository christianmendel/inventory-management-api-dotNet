using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Settings.UnitOfWork;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<CategoryReponse>> GetCategories()
        {
            var response = new List<CategoryReponse>();

            var result = await _uow.Categories.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(CategoryMapper.CategoryMapperView(item));
            }

            return response;
        }

        public async Task<CategoryReponse> GetCategory(int id)
        {
            var response = new CategoryReponse();

            var result = await _uow.Categories.GetByIdAsync(id);
            
            if (result == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            response = CategoryMapper.CategoryMapperView(result);

            return response;
        }

        public async Task<CategoryReponse> CreateCategory(CategoryRequest categoryRequest)
        {
            var category = await _uow.Categories.AddAsync(CategoryMapper.CategoryMapperDto(categoryRequest));

            return await GetCategory(category.Id);
        }

        public async Task<CategoryReponse> UpdateCategory(int id, CategoryRequest categoryRequest)
        {
            var response = new CategoryReponse();

            var result = await _uow.Categories.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            Category category = CategoryMapper.CategoryMapperDto(categoryRequest);
            category.AddId(id);

            await _uow.Categories.UpdateAsync(category);

            return await GetCategory(id);
        }

        public async Task<CategoryReponse> DeleteCategory(int id)
        {
            var response = new CategoryReponse();

            var result = await _uow.Categories.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            await _uow.Categories.DeleteAsync(id);

            return response;
        }
    }
}
