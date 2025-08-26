using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Settings.UnitOfWork;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            var response = new List<OrderResponse>();

            var orders = await _uow.Orders.GetAllAsync();

            foreach (var order in orders)
            {
                var orderItems = await _uow.OrderItems.GetAllByOrderIdAsync(order.Id);
                order.AddListOrderItems(orderItems.ToList());

                response.Add(OrderMapper.OrderMapperView(order));
            }

            return response;
        }

        public async Task<OrderResponse> GetOrder(int id)
        {
            var response = new OrderResponse();

            var result = await _uow.Orders.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            }
            ;

            IEnumerable<OrderItem> orderItems = await _uow.OrderItems.GetAllByOrderIdAsync(id);
            result.AddListOrderItems(orderItems.ToList());
            response = OrderMapper.OrderMapperView(result);

            return response;
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            try
            {
                _uow.Begin();

                var response = new OrderResponse();

                var order = OrderMapper.OrderMapperDto(orderRequest);

                foreach (var item in order.OrderItems)
                {
                    var product = await _uow.Products.GetByIdAsync(item.ProductId);

                    if (product == null)
                    {
                        response.AddNotification(new Notification("Produto não encontrado!"));
                        _uow.Rollback();
                        return response;
                    }

                    if (item.Quantity > product.Quantity)
                    {
                        response.AddNotification(new Notification("Produto não tem essa quantidade"));
                        _uow.Rollback();
                        return response;
                    }
                }

                order = await _uow.Orders.AddAsync(order);

                foreach (var item in order.OrderItems)
                {
                    item.AddOrderId(order.Id);
                    await _uow.OrderItems.AddAsync(item);

                    var produto = await _uow.Products.GetByIdAsync(item.ProductId);
                    produto.UpdateQuantityMinus(item.Quantity);
                    await _uow.Products.UpdateAsync(produto);

                    var movement = new InventoryMovement(item.ProductId, item.Quantity);
                    await _uow.InventoryMovements.AddAsync(movement);
                }

                _uow.Commit();

                return await GetOrder(order.Id);
            }
            catch
            {
                _uow.Rollback();
                throw;
            }
        }

        public async Task<OrderResponse> UpdateOrder(int id, OrderRequest orderRequest)
        {
            var response = new OrderResponse();

            var result = await _uow.Orders.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            }
            ;

            Order order = OrderMapper.OrderMapperDto(orderRequest);
            order.AddId(id);

            await _uow.Orders.UpdateAsync(order);

            return await GetOrder(id);
        }

        public async Task<OrderResponse> DeleteOrder(int id)
        {
            var response = new OrderResponse();

            var result = await _uow.Orders.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            }
            ;

            await _uow.Orders.DeleteAsync(id);

            return response;
        }
    }
}
