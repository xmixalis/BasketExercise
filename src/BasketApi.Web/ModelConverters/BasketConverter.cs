using System.Collections.Generic;
using System.Linq;
using BasketApi.Models;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Web.ModelConverters
{
    public static class BasketConverter
    {
        public static BasketModelResponse ToBasketModelResponse(this Basket b, IEnumerable<ProductItem> products)
        {
            return new BasketModelResponse()
            {
                BasketId = b.Id,
                Items = (from bi in b.Items
                         join prod in products on bi.ProductItemId equals prod.Id
                         select new BasketModelItem()
                         {
                             Price = bi.UnitPrice,
                             ProductId = bi.ProductItemId,
                             ProductName = prod.Name,
                             Quantity = bi.Quantity
                         }).ToList()
            };
        }
    }
}
