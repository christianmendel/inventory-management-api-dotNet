using InventoryManagement.Enums;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Dto.Response
{
    public class InventoryMovementResponse : Notifiable
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
        public DateTime MovementDate { get; set; }
        public MovementType MovementType { get; set; }
    }
}
