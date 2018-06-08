using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;

namespace BasketApi.Web.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductItem>> GetAllItems();
    }
}
