using InventoryManagement.Models;

namespace InventoryManagement.Contracts.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
