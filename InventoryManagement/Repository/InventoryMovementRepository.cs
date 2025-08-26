using Dapper;
using InventoryManagement.Contracts.Repository;
using InventoryManagement.Models;
using InventoryManagement.Settings.Base;
using System.Data;

namespace InventoryManagement.Repository
{
    public class InventoryMovementRepository : RepositoryBase, IInventoryMovementRepository
    {
        public InventoryMovementRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<InventoryMovement> AddAsync(InventoryMovement movement, IDbTransaction? transaction = null)
        {
            var query = "INSERT INTO inventory_movements (ProductId, QuantityChange, MovementType) VALUES (@ProductId, @QuantityChange, @MovementType) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, movement, transaction ?? _dbTransaction);
            movement.AddId(id);
            return movement;
        }

        public async Task UpdateAsync(InventoryMovement movement)
        {
            var query = "UPDATE inventory_movements SET ProductId = @ProductId, QuantityChange = @QuantityChange, MovementType = @MovementType WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, movement, _dbTransaction);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM inventory_movements WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id }, _dbTransaction);
        }

        public async Task<InventoryMovement> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM inventory_movements WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<InventoryMovement>(query, new { Id = id }, _dbTransaction);
        }

        public async Task<IEnumerable<InventoryMovement>> GetAllAsync()
        {
            var query = "SELECT * FROM inventory_movements";
            return await _dbConnection.QueryAsync<InventoryMovement>(query, _dbTransaction);
        }
    }
}
