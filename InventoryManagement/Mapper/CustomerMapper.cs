using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class CustomerMapper
    {
        public static Customer CustomerMapperDto(CustomerRequest customerRequest)
        {
            return new Customer(customerRequest.Name, customerRequest.Email, customerRequest.Address, customerRequest.PhoneNumber);
        }

        public static CustomerResponse CustomerMapperView(Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Address = customer.Address,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
                Name = customer.Name
            };
        }
    }
}
