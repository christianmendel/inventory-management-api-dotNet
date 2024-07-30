using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class OrderItemMapper
    {
        public static OrderItem OrderItemMapperDto(OrderItemRequest orderItemRequest)
        {
            return new OrderItem(orderItemRequest.OrderId, orderItemRequest.ProductId, orderItemRequest.Quantity, orderItemRequest.UnitPrice);
        }

        public static OrderItemResponse OrderItemMapperView(OrderItem product)
        {
            return new OrderItemResponse
            {
                Id = product.Id,
                OrderId = product.OrderId,
                ProductId = product.ProductId,
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice
            };
        }
    }
}
