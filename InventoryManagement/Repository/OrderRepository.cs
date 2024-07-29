using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;

namespace InventoryManagement.Repository
{
    public class OrderRepository : RepositoryBase
    {
        public OrderRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<Order> AddAsync(Order order)
        {
            var query = "INSERT INTO orders (CustomerId, OrderDate, Status) VALUES (@CustomerId, @OrderDate, @Status)";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, order);
            order.Id = id;
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            var query = "UPDATE orders SET CustomerId = @CustomerId, OrderDate = @OrderDate, Status = @Status WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, order);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM orders WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM orders WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Order>(query, new { Id = id });
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var query = "SELECT * FROM orders";
            return await _dbConnection.QueryAsync<Order>(query);
        }
    }
}
