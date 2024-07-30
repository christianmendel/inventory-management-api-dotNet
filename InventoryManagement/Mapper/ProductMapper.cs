using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class ProductMapper
    {
        public static Product ProductMapperDto(ProductRequest productRequest)
        {
            return new Product(productRequest.CategoryId, productRequest.Name, productRequest.Description, productRequest.Quantity, productRequest.Price);
        }

        public static ProductResponse ProductMapperView(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price
            };
        }
    }
}
