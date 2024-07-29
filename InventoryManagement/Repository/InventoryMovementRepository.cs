using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;

namespace InventoryManagement.Repository
{
    public class InventoryMovementRepository : RepositoryBase
    {
        public InventoryMovementRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<InventoryMovement> AddAsync(InventoryMovement movement)
        {
            var query = "INSERT INTO inventory_movements (ProductId, QuantityChange, MovementDate, MovementType) VALUES (@ProductId, @QuantityChange, @MovementDate, @MovementType)";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, movement);
            movement.Id = id;
            return movement;
        }

        public async Task UpdateAsync(InventoryMovement movement)
        {
            var query = "UPDATE inventory_movements SET ProductId = @ProductId, QuantityChange = @QuantityChange, MovementDate = @MovementDate, MovementType = @MovementType WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, movement);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM inventory_movements WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<InventoryMovement> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM inventory_movements WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<InventoryMovement>(query, new { Id = id });
        }

        public async Task<IEnumerable<InventoryMovement>> GetAllAsync()
        {
            var query = "SELECT * FROM inventory_movements";
            return await _dbConnection.QueryAsync<InventoryMovement>(query);
        }
    }
}
