using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Mapper;
using InventoryManagement.Settings.Validations;

namespace InventoryManagement.Contracts.Service
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetProducts();
        Task<ProductResponse> GetProduct(int id);
        Task<ProductResponse> CreateProduct(ProductRequest productRequest);
        Task<ProductResponse> UpdateProduct(int id, ProductRequest productRequest);
        Task<ProductResponse> DeleteProduct(int id);
    }
}
