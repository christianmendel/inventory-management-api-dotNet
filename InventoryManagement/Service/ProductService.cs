using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductResponse>> GetProducts()
        {
            var response = new List<ProductResponse>();

            var result = await _repository.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(ProductMapper.ProductMapperView(item));
            }

            return response;
        }

        public async Task<ProductResponse> GetProduct(int id)
        {
            var response = new ProductResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Produto não encotrado!"));
                return response;
            };

            response = ProductMapper.ProductMapperView(result);

            return response;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest productRequest)
        {
            var result = ProductMapper.ProductMapperDto(productRequest);

            var product = await _repository.AddAsync(result);

            return await GetProduct(product.Id);
        }

        public async Task<ProductResponse> UpdateProduct(int id, ProductRequest productRequest)
        {
            var response = new ProductResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Produto não encotrado!"));
                return response;
            };

            Product product = ProductMapper.ProductMapperDto(productRequest);
            product.AddId(id);

            await _repository.UpdateAsync(product);

            return await GetProduct(id);
        }

        public async Task<ProductResponse> DeleteProduct(int id)
        {
            var response = new ProductResponse();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Produto não encotrado!"));
                return response;
            };

            await _repository.DeleteAsync(id);

            return response;
        }
    }
}
