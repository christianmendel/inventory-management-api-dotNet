using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _repository;
        private readonly OrderItemRepository _orderItemRepository;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository repository, OrderItemRepository orderItemRepository, ProductRepository productRepository)
        {
            _repository = repository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
        }

        public async Task<List<OrderResponse>> GetOrders()
        {
            var response = new List<OrderResponse>();

            var result = await _repository.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(OrderMapper.OrderMapperView(item));
            }

            return response;
        }

        public async Task<OrderResponse> GetOrder(int id)
        {
            var response = new OrderResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            };

            IEnumerable<OrderItem> orderItems = await _orderItemRepository.GetAllByOrderIdAsync(id);
            result.AddListOrderItems(orderItems.ToList());
            response = OrderMapper.OrderMapperView(result);

            return response;
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            var response = new OrderResponse();

            foreach (var orderItem in orderRequest.orderItems)
            {

                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);

                if (product == null) {
                    response.AddNotification(new Notification("Produto não encotrado!"));
                    return response;
                }

                if (orderItem.Quantity > product.Quantity)
                {
                    response.AddNotification(new Notification("Produto não tem essa quantidade"));
                    return response;
                }
            }

            var result = await _repository.AddAsync(OrderMapper.OrderMapperDto(orderRequest));

            return await GetOrder(result.Id);
        }

        public async Task<OrderResponse> UpdateOrder(int id, OrderRequest orderRequest)
        {
            var response = new OrderResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            };

            Order order = OrderMapper.OrderMapperDto(orderRequest);
            order.AddId(id);

            await _repository.UpdateAsync(order);

            return await GetOrder(id);
        }

        public async Task<OrderResponse> DeleteOrder(int id)
        {
            var response = new OrderResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Ordem não encotrada!"));
                return response;
            };

            await _repository.DeleteAsync(id);

            return response;
        }
    }
}
