using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Repository;
using InventoryManagement.Settings.HttpException;
using InventoryManagement.Settings.Validations;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly OrderItemRepository _repository;

        public OrderItemService(OrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderItemResponse>> GetOrderItems()
        {
            var response = new List<OrderItemResponse>();

            var result = await _repository.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(OrderItemMapper.OrderItemMapperView(item));
            }

            return response;
        }

        public async Task<OrderItemResponse> GetOrderItem(int id)
        {
            var response = new OrderItemResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem Item não encotrado!"));
                return response;
            };

            response = OrderItemMapper.OrderItemMapperView(result);

            return response;
        }
    }
}
