using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Web.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductItem>> GetAllItemsAsync();
        Task<IEnumerable<ProductItem>> ListAsync(Expression<Func<ProductItem, bool>> criteria);
    }
}
