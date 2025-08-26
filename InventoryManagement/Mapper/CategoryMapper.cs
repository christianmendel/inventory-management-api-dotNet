using InventoryManagement.Dto.Request;
using InventoryManagement.Dto.Response;
using InventoryManagement.Models;

namespace InventoryManagement.Mapper
{
    public class CategoryMapper
    {
        public static Category CategoryMapperDto(CategoryRequest categoryRequest)
        {
            return new Category(categoryRequest.Name, categoryRequest.Description);
        }

        public static CategoryReponse CategoryMapperView(Category category)
        {
            return new CategoryReponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}
