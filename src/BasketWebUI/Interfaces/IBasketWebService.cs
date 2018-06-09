using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Models;
using BasketWebUI.Models;

namespace BasketWebUI.Interfaces
{
    public interface IBasketWebService
    {
        Task<BasketIndexViewModel> GetOrCreateBasketForUser(string userId);
        Task<BasketAddItemResponse> AddItemToBasket(int basketId, int productId, decimal price, int quantity);
        Task<BasketUpdateResponse> UpdateBasketItem(int basketid, Dictionary<int, int> quantities);
        Task<BasketRemoveItemResponse> RemoveBasketItem(int basketId, int productId);
    }
}
