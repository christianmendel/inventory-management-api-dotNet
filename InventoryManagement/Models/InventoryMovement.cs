using InventoryManagement.Enums;

namespace InventoryManagement.Models
{
    public class InventoryMovement
    {
        public InventoryMovement() { }

        public InventoryMovement(int productId, int quantityChange)
        {
            ProductId = productId;
            QuantityChange = quantityChange;
            MovementType = MovementType.Compra;
        }

        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int QuantityChange { get; private set; }
        public DateTime MovementDate { get; private set; }
        public MovementType MovementType { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int AddId(int id)
        {
            return Id = id;
        }
    }
}
