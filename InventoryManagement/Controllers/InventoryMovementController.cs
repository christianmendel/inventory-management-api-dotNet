using InventoryManagement.Dto.Response;
using InventoryManagement.Service;
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

            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovementResponse>> GetInventoryMovement(int id)
        {
            var response = await _service.GetInventoryMovement(id);

            if (!response.IsValid())
                return BadRequest(response.Notifications);

            return response;
        }
    }
}
