namespace InventoryManagement.Models
{
    public class Category
    {
        public Category() {}

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }


        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int AddId(int id)
        {
            return Id = id;
        }
    }
}
