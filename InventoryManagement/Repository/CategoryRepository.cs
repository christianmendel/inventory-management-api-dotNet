using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;

namespace InventoryManagement.Repository
{
    public class CategoryRepository : RepositoryBase
    {
        public CategoryRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<Category> AddAsync(Category category)
        {
            var query = "INSERT INTO categories (Name, Description) VALUES (@Name, @Description) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, category);
            category.AddId(id);
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            var query = "UPDATE categories SET Name = @Name, Description = @Description WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, category);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM categories WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM categories WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var query = "SELECT * FROM categories";
            return await _dbConnection.QueryAsync<Category>(query);
        }
    }
}
