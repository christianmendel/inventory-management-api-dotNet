using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;
using InventoryManagement.Contracts.Repository;

namespace InventoryManagement.Repository
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        public OrderRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<Order> AddAsync(Order order)
        {
            var query = "INSERT INTO orders (CustomerId, Status) VALUES (@CustomerId, @Status) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, order, _dbTransaction);
            order.AddId(id);
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            var query = "UPDATE orders SET CustomerId = @CustomerId, Status = @Status WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, order, _dbTransaction);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM orders WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id }, _dbTransaction);
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM orders WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(query, new { Id = id }, _dbTransaction);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var query = "SELECT * FROM orders";
            return await _dbConnection.QueryAsync<Order>(query, _dbTransaction);
        }
    }
}
