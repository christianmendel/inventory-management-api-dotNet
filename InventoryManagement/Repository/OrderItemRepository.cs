using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;

namespace InventoryManagement.Repository
{
    public class OrderItemRepository : RepositoryBase
    {
        public OrderItemRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            var query = "INSERT INTO order_items (OrderId, ProductId, Quantity, UnitPrice) VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, orderItem);
            orderItem.AddId(id);
            return orderItem;
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            var query = "UPDATE order_items SET OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, orderItem);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM order_items WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM order_items WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<OrderItem>(query, new { Id = id });
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var query = "SELECT * FROM order_items";
            return await _dbConnection.QueryAsync<OrderItem>(query);
        }
        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(int orderId)
        {
            var query = "SELECT * FROM order_items WHERE OrderId = @OrderId";
            return await _dbConnection.QueryAsync<OrderItem>(query, new { OrderId = orderId });
        }
    }
}
