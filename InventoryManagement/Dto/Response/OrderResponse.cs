using InventoryManagement.Models;

namespace InventoryManagement.Dto.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderItemResponse> orderItems { get; set; }
    }
}
