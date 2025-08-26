using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Dto.Response
{
    public class ProductResponse : Notifiable
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get;  set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
