using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;
using InventoryManagement.Contracts.Repository;

namespace InventoryManagement.Repository
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private readonly OrderItemRepository _orderItemRepository;
        private readonly InventoryMovementRepository _inventoryMovementRepository;
        private readonly ProductRepository _productRepository;

        public OrderRepository(IDbConnection dbConnection, OrderItemRepository orderItemRepository, InventoryMovementRepository inventoryMovementRepository, ProductRepository productRepository) : base(dbConnection)
        {
            _orderItemRepository = orderItemRepository;
            _inventoryMovementRepository = inventoryMovementRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> AddAsync(Order order)
        {
            var query = "INSERT INTO orders (CustomerId, Status) VALUES (@CustomerId, @Status) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, order);
            order.AddId(id);

            foreach (var item in order.OrderItems)
            {
                item.AddOrderId(id);
                await _orderItemRepository.AddAsync(item);

                var produto = await _productRepository.GetByIdAsync(item.ProductId);

                produto.UpdateQuantityMinus(item.Quantity);
                await _productRepository.UpdateAsync(produto);
            }

            foreach (var item in order.OrderItems)
            {
                var inventoryMovement = new InventoryMovement(item.ProductId, item.Quantity);
                await _inventoryMovementRepository.AddAsync(inventoryMovement);
            }

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            var query = "UPDATE orders SET CustomerId = @CustomerId, Status = @Status WHERE Id = @Id";
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

            var orders = await _dbConnection.QueryAsync<Order>(query);

            foreach (var item in orders)
            {
                var orderItems = await _orderItemRepository.GetAllByOrderIdAsync(item.Id);
                item.AddListOrderItems(orderItems.AsList());
            }

            return orders;
        }
    }
}
