using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class OrderMapper
    {
        public static Order OrderMapperDto(OrderRequest orderRequest)
        {
            return new Order(orderRequest.CustomerId, orderRequest.Status, orderRequest.orderItems.Select(item => OrderItemMapper.OrderItemMapperDto(item)).ToList());
        }

        public static OrderResponse OrderMapperView(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                OrderDate = order.OrderDate,
                orderItems = order.OrderItems.Select(item => OrderItemMapper.OrderItemMapperView(item)).ToList(),
            };
        }
    }
}
