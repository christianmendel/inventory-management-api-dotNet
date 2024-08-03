using InventoryManagement.Dto.Response;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Service;
using InventoryManagement.Settings.HttpException;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class InventoryMovementController : Controller
    {
        private readonly InventoryMovementService _service;

        public InventoryMovementController(InventoryMovementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryMovementResponse>>> GetInventoryMovements()
        {
            var response = await _service.GetInventoryMovements();

            if (!response.All(item => item.IsValid()))
            {
                var customer = response.Where(item => !item.IsValid()).FirstOrDefault();
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, customer.Notifications));
            }

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovementResponse>> GetInventoryMovement(int id)
        {
            var response = await _service.GetInventoryMovement(id);

            if (!response.IsValid())
                return BadRequest(new HttpException(StatusCodes.Status400BadRequest, response.Notifications));

            return response;
        }
    }
}
