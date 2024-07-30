using InventoryManagement.Models;

namespace InventoryManagement.Dto.Request
{
    public class OrderRequest
    {
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public List<OrderItemRequest> orderItems { get; set; }
    }
}
