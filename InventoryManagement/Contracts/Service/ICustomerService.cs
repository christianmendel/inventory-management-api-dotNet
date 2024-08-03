using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Contracts.Service
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetCustomers();
        Task<CustomerResponse> GetCustomer(int id);
        Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest);
        Task<CustomerResponse> UpdateCustomer(int id, CustomerRequest customerRequest);
        Task<CustomerResponse> DeleteCustomer(int id);
    }
}
