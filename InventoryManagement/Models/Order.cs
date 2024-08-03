namespace InventoryManagement.Models
{
    public class Order
    {
        public Order() { }

        public Order(int customerId, string status, List<OrderItem> orderItems)
        {
            CustomerId = customerId;
            OrderDate = new DateTime();
            Status = status;
            OrderItems = orderItems;
        }


        public int Id { get; private set; }
        public int CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order AddListOrderItems(List<OrderItem> orderItems)
        {
            this.OrderItems = orderItems;
            return this;
        }

        public int AddId(int id)
        {
            return Id = id;
        }
    }
}
