using InventoryManagement.Models;
using InventoryManagement.Repository;
using System.Data;

namespace InventoryManagement.Contracts.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
    }
}
