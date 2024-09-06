using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Contracts.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryReponse>> GetCategories();
        Task<CategoryReponse> GetCategory(int id);
        Task<CategoryReponse> CreateCategory(CategoryRequest categoryRequest);
        Task<CategoryReponse> UpdateCategory(int id, CategoryRequest categoryRequest);
        Task<CategoryReponse> DeleteCategory(int id);
    }
}
