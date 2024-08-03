using InventoryManagement.Models;

namespace InventoryManagement.Contracts.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
