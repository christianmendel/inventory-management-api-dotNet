using InventoryManagement.Models;
using InventoryManagement.Repository;
using System.Data;
using System.Data.Common;

namespace InventoryManagement.Contracts.Repository
{
    public interface IInventoryMovementRepository
    {
        Task<InventoryMovement> AddAsync(InventoryMovement movement, IDbTransaction? transaction = null);
        Task UpdateAsync(InventoryMovement movement);
        Task DeleteAsync(int id);
        Task<InventoryMovement> GetByIdAsync(int id);
        Task<IEnumerable<InventoryMovement>> GetAllAsync();
    }
}
