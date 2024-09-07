using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;

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
