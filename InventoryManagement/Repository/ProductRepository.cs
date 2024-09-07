using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;
using InventoryManagement.Contracts.Repository;

namespace InventoryManagement.Repository
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<Product> AddAsync(Product product)
        {
            var query = "INSERT INTO products (CategoryId, Name, Description, Quantity, Price) VALUES (@CategoryId, @Name, @Description, @Quantity, @Price) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, product);
            product.AddId(id);
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var query = "UPDATE products SET CategoryId = @CategoryId, Name = @Name, Description = @Description, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, product);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM products WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM products WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var query = "SELECT * FROM products";
            return await _dbConnection.QueryAsync<Product>(query);
        }
    }
}
