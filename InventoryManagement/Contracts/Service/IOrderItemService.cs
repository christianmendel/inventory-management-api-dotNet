using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Contracts.Service
{
    public interface IOrderItemService
    {
        Task<List<OrderItemResponse>> GetOrderItems();
        Task<OrderItemResponse> GetOrderItem(int id);
    }
}
