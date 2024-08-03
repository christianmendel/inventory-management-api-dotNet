using Dapper;
using InventoryManagement.Settings.Base;
using InventoryManagement.Models;
using System.Data;
using InventoryManagement.Contracts.Repository;

namespace InventoryManagement.Repository
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public CustomerRepository(IDbConnection dbConnection) : base(dbConnection) { }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var query = "INSERT INTO customers (Name, Email, Address, PhoneNumber) VALUES (@Name, @Email, @Address, @PhoneNumber) RETURNING id";
            var id = await _dbConnection.ExecuteScalarAsync<int>(query, customer);
            customer.AddId(id);
            return customer;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var query = "UPDATE customers SET Name = @Name, Email = @Email, Address = @Address, PhoneNumber = @PhoneNumber WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, customer);
        }

        public async Task DeleteAsync(int id)
        {
            var query = "DELETE FROM customers WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM customers WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var query = "SELECT * FROM customers";
            return await _dbConnection.QueryAsync<Customer>(query);
        }
    }
}
