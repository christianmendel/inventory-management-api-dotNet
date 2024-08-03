using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.Validations;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Contracts.Service
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetOrders();
        Task<OrderResponse> GetOrder(int id);
        Task<OrderResponse> CreateOrder(OrderRequest orderRequest);
        Task<OrderResponse> UpdateOrder(int id, OrderRequest orderRequest);
        Task<OrderResponse> DeleteOrder(int id);
    }
}
