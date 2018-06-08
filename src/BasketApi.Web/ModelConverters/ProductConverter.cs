using BasketApi.Models;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Web.ModelConverters
{
    public static class ProductConverter
    {
        public static ProductModelResponse ToProductModelResponse(this ProductItem p)
        {
            return new ProductModelResponse()
            {
                ProductId = p.Id,
                Description = p.Description,
                Name = p.Name,
                Price = p.Price
            };
        }
    }
}
