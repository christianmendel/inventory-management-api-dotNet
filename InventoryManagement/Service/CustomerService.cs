using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repository;

        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerResponse>> GetCustomers()
        {
            var response = new List<CustomerResponse>();

            var result = await _repository.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(CustomerMapper.CustomerMapperView(item));
            }

            return response;
        }

        public async Task<CustomerResponse> GetCustomer(int id)
        {
            var response = new CustomerResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Comprador não encotrado!"));
                return response;
            };

            response = CustomerMapper.CustomerMapperView(result);

            return response;
        }

        public async Task<CustomerResponse> CreateCustomer(CustomerRequest customerRequest)
        {
            var result = CustomerMapper.CustomerMapperDto(customerRequest);

            var customer = await _repository.AddAsync(result);

            return await GetCustomer(customer.Id);
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            var response = new CustomerResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Comprador não encotrado!"));
                return response;
            };

            Customer customer = CustomerMapper.CustomerMapperDto(customerRequest);
            customer.AddId(id);

            await _repository.UpdateAsync(customer);

            return await GetCustomer(id);
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var response = new CustomerResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Comprador não encotrado!"));
                return response;
            };

            await _repository.DeleteAsync(id);

            return response;
        }
    }
}
