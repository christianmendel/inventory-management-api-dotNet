using InventoryManagement.Models;
using InventoryManagement.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    public class InventoryMovementController : Controller
    {
        private readonly InventoryMovementRepository _repository;

        public InventoryMovementController(InventoryMovementRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryMovement>>> GetInventoryMovements()
        {
            var movements = await _repository.GetAllAsync();
            return Ok(movements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryMovement>> GetInventoryMovement(int id)
        {
            var movement = await _repository.GetByIdAsync(id);
            if (movement == null) return NotFound();
            return Ok(movement);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryMovement>> CreateInventoryMovement(InventoryMovement movement)
        {
            var createdMovement = await _repository.AddAsync(movement);
            return CreatedAtAction(nameof(GetInventoryMovement), new { id = createdMovement.Id }, createdMovement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryMovement(int id, InventoryMovement category)
        {
            if (id != category.Id) return BadRequest();
            await _repository.UpdateAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryMovement(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
