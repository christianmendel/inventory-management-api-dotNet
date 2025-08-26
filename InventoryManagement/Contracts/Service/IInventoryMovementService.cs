using InventoryManagement.Dto.Response;

namespace InventoryManagement.Contracts.Service
{
    public interface IInventoryMovementService
    {
        Task<List<InventoryMovementResponse>> GetInventoryMovements();
        Task<InventoryMovementResponse> GetInventoryMovement(int id);
    }
}
