using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BasketCore.Interfaces
{
    public interface IBasketService
    {
        Task AddItemToBasket(int basketId, int productItemId, decimal price, int quantity);
        Task RemoveItemFromBasket(int basketId, int productItemId);
        Task ClearItemsFromBasket(int basketId);
        Task DeleteBasketAsync(int basketId);
        Task<int> GetBasketItemCountAsync(string userName);
        Task SetQuantities(int basketId, Dictionary<string, int> quantities);
    }
}
