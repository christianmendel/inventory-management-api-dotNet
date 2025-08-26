using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class InventoryMovementMapper
    {
        public static InventoryMovementResponse InventoryManagementMapperView(InventoryMovement inventoryMovement)
        {
            return new InventoryMovementResponse
            {
                Id = inventoryMovement.Id,
                MovementDate = inventoryMovement.MovementDate,
                MovementType = inventoryMovement.MovementType,
                ProductId = inventoryMovement.ProductId,
                QuantityChange = inventoryMovement.QuantityChange
            };
        }
    }
}
