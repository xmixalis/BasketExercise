using BasketWebUI.Models;

namespace BasketWebUI.Interfaces
{
    public interface IProductService
    {
        ProductsIndexViewModel GetProductItems();
    }
}
