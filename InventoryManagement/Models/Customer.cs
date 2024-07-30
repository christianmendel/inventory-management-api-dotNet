namespace InventoryManagement.Models
{
    public class Customer
    {
        public Customer() { }

        public Customer(string name, string email, string address, string phoneNumber)
        {
            Name = name;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public int AddId(int id) {
            return Id = id;
        }
    }
}
