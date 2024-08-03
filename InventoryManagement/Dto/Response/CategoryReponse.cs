using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Dto.Response
{
    public class CategoryReponse : Notifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
