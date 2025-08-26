using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.UnitOfWork;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IUnitOfWork _uow;

        public OrderItemService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<OrderItemResponse>> GetOrderItems()
        {
            var response = new List<OrderItemResponse>();

            var result = await _uow.OrderItems.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(OrderItemMapper.OrderItemMapperView(item));
            }

            return response;
        }

        public async Task<OrderItemResponse> GetOrderItem(int id)
        {
            var response = new OrderItemResponse();

            var result = await _uow.OrderItems.GetByIdAsync(id);

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
