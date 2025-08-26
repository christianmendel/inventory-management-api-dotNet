namespace InventoryManagement.Models
{
    public class OrderItem
    {
        public OrderItem() { }

        public OrderItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int AddId(int id)
        {
            return Id = id;
        }

        public OrderItem AddOrderId(int id)
        {
            OrderId = id;
            return this;
        }

        public OrderItem AddUnitPrice(decimal unitPrice)
        {
            UnitPrice = unitPrice;
            return this;
        }
    }
}
