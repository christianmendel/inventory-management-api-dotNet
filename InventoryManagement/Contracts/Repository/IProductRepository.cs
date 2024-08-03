using InventoryManagement.Models;

namespace InventoryManagement.Contracts.Repository
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
