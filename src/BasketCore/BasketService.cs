using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasketCore.Entities;
using BasketCore.Helpers;
using BasketCore.Interfaces;

namespace BasketCore
{
    public class BasketService 
    {
        private readonly IAsyncRepository<Basket> _basketRepository;

        public BasketService(IAsyncRepository<Basket> basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task AddItemToBasket(int basketId, int productItemId, decimal price, int quantity)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            basket.AddItem(productItemId, price, quantity);

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task RemoveItemFromBasket(int basketId, int productItemId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);

            basket.RemoveItem(productItemId);

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task ClearItemsFromBasket(int basketId)
        {
            Basket basket = await _basketRepository.GetByIdAsync(basketId);

            basket.ClearItems();

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            await _basketRepository.DeleteAsync(basket);
        }

        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Utils.ParameterNotNullOrEmpty(userName, nameof(userName));
            Basket basket = (await _basketRepository.ListAsync(b => b.UserId == userName)).FirstOrDefault();
            if (basket == null)
            {
                return 0;
            }
            int count = basket.Items.Sum(i => i.Quantity);

            return count;
        }
        public async Task SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            Helpers.Utils.ParameterNotNull(quantities, nameof(quantities));
            Basket basket = await _basketRepository.GetByIdAsync(basketId);
            Helpers.Utils.EntityNotNull(basketId, basket);

            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.Id.ToString(), out var quantity))
                {
                    //_logger.LogInformation($"Updating quantity of item ID:{item.Id} to {quantity}.");
                    item.Quantity = quantity;
                }
            }
            await _basketRepository.UpdateAsync(basket);
        }
    }

}
