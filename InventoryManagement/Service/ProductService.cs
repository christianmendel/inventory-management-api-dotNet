using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;
        private readonly CategoryRepository _categoryRepository;

        public ProductService(ProductRepository repository, CategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
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
            var response = new ProductResponse();

            var category = await _categoryRepository.GetByIdAsync(productRequest.CategoryId);
            
            if (category == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            var product = await _repository.AddAsync(ProductMapper.ProductMapperDto(productRequest));

            response = await GetProduct(product.Id);

            return response;
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

            var category = await _categoryRepository.GetByIdAsync(productRequest.CategoryId);

            if (category == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
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
