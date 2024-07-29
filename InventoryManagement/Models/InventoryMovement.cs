namespace InventoryManagement.Models
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
        public DateTime MovementDate { get; set; }
        public string MovementType { get; set; }
    }
}
