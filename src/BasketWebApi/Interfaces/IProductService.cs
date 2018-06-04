using BasketWebApi.Models;

namespace BasketWebApi.Interfaces
{
    public interface IProductService
    {
        ProductsIndexViewModel GetProductItems();
    }
}
