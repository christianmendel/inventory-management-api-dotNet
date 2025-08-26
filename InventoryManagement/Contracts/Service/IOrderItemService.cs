using InventoryManagement.Dto.Response;

namespace InventoryManagement.Contracts.Service
{
    public interface IOrderItemService
    {
        Task<List<OrderItemResponse>> GetOrderItems();
        Task<OrderItemResponse> GetOrderItem(int id);
    }
}
