using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _repository;

        public CustomerController(CustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetCustomers()
        {
            var customerResponse = new List<CustomerResponse>();

            var customers = await _repository.GetAllAsync();

            foreach (var item in customers)
            {
                customerResponse.Add(CustomerMapper.CustomerMapperView(item));
            }

            return customerResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
        {
            var customerResponse = new CustomerResponse();

            var customer = await _repository.GetByIdAsync(id);

            if (customer == null) return NotFound();

            customerResponse = CustomerMapper.CustomerMapperView(customer);

            return customerResponse;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerRequest customerRequest)
        {
            var customer = CustomerMapper.CustomerMapperDto(customerRequest);

            var createdCustomer = await _repository.AddAsync(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id) return BadRequest();

            await _repository.UpdateAsync(customer);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            
            if (customer == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
