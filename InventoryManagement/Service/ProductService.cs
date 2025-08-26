using InventoryManagement.Contracts.Service;
using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Models;
using InventoryManagement.Settings.UnitOfWork;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<ProductResponse>> GetProducts()
        {
            var response = new List<ProductResponse>();

            var result = await _uow.Products.GetAllAsync();

            foreach (var item in result)
            {
                response.Add(ProductMapper.ProductMapperView(item));
            }

            return response;
        }

        public async Task<ProductResponse> GetProduct(int id)
        {
            var response = new ProductResponse();

            var result = await _uow.Products.GetByIdAsync(id);

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

            var category = await _uow.Categories.GetByIdAsync(productRequest.CategoryId);
            
            if (category == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            var product = await _uow.Products.AddAsync(ProductMapper.ProductMapperDto(productRequest));

            response = await GetProduct(product.Id);

            return response;
        }

        public async Task<ProductResponse> UpdateProduct(int id, ProductRequest productRequest)
        {
            var response = new ProductResponse();

            var result = await _uow.Products.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Produto não encotrado!"));
                return response;
            };

            var category = await _uow.Categories.GetByIdAsync(productRequest.CategoryId);

            if (category == null)
            {
                response.AddNotification(new Notification("Categoria não encotrada!"));
                return response;
            };

            Product product = ProductMapper.ProductMapperDto(productRequest);
            product.AddId(id);

            await _uow.Products.UpdateAsync(product);

            return await GetProduct(id);
        }

        public async Task<ProductResponse> DeleteProduct(int id)
        {
            var response = new ProductResponse();

            var result = await _uow.Products.GetByIdAsync(id);

            if (result == null)
            {
                response.AddNotification(new Notification("Produto não encotrado!"));
                return response;
            };

            await _uow.Products.DeleteAsync(id);

            return response;
        }
    }
}
