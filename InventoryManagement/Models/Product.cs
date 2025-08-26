namespace InventoryManagement.Models
{
    public class Product
    {
        public Product() { }

        public Product(int categoryId, string name, string description, int quantity, decimal price)
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        public int Id { get; private set; }
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int AddId(int id)
        {
            return Id = id;
        }

        public int UpdateQuantityMinus(int quantity)
        {
            return Quantity = Quantity - quantity;
        }
    }
}
