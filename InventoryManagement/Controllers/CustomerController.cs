using Azure;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Service;
using InventoryManagement.Settings.HttpException;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly CustomerService _service;

        public CustomerController(CustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponse>>> GetCustomers()
        {
            var response = await _service.GetCustomers();

            if (!response.All(item => item.IsValid()))
            {
                var customer = response.Where(item => !item.IsValid()).FirstOrDefault();
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, customer.Notifications));
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomer(int id)
        {
            var response = await _service.GetCustomer(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> CreateCustomer(CustomerRequest customerRequest)
        {
            var response = await _service.CreateCustomer(customerRequest);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponse>> UpdateCustomer(int id, CustomerRequest customerRequest)
        {
            var response = await _service.UpdateCustomer(id, customerRequest);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerResponse>> DeleteCustomer(int id)
        {
            var response = await _service.DeleteCustomer(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }
    }
}
