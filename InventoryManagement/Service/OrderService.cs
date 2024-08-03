using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Service
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _repository;
        private readonly ProductRepository _productRepository;

        public OrderService(OrderRepository repository, ProductRepository productRepository)
        {
            _repository = repository;
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
                response.AddNotification(new Notification("Comprador não encotrado!"));
                return response;
            };

            response = OrderMapper.OrderMapperView(result);

            return response;
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest orderRequest)
        {
            var result = OrderMapper.OrderMapperDto(orderRequest);

            var order = await _repository.AddAsync(result);

            return await GetOrder(order.Id);
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

            await _repository.UpdateAsync(OrderMapper.OrderMapperDto(orderRequest));

            return response;
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
