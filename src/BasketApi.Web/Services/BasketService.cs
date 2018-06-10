using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Infrastructure.Entities;
using BasketApi.Web.Helpers;
using BasketApi.Infrastructure.Interfaces;
using BasketApi.Web.Interfaces;

namespace BasketApi.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;

        public BasketService(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        #region GetBasketByUserId
        public async Task<Basket> GetOrCreateBasketForUser(string userId)
        {
            Basket basket = await GetBasketByUserId(userId);

            if (basket == null)
                basket = await CreateBasketForUser(userId);

            basket = await _basketRepository.GetByIdWithItemsAsync(basket.Id);
            return basket;
        }
        async Task<Basket> GetBasketByUserId(string userId)
        {
            Basket basket = (await _basketRepository.ListAsync(b => b.UserId == userId)).FirstOrDefault();

            return basket;
        }
        private async Task<Basket> CreateBasketForUser(string userId)
        {
            //improvement to make the ID of the entity to be auto increment
            Basket basket = new Basket() { UserId = userId };
            await _basketRepository.AddAsync(basket);

            return basket;
        }
        #endregion


        public async Task AddItemToBasket(int basketId, int productItemId, decimal price, int quantity)
        {
            Basket basket = await GetBasket(basketId);

            basket.AddItem(productItemId, price, quantity);

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task RemoveItemFromBasket(int basketId, int productItemId)
        {
            Basket basket = await GetBasket(basketId);

            basket.RemoveItem(productItemId);

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task ClearItemsFromBasket(int basketId)
        {
            Basket basket = await GetBasket(basketId);

            basket.ClearItems();

            await _basketRepository.UpdateAsync(basket);
        }
        public async Task DeleteBasketAsync(int basketId)
        {
            Basket basket = await GetBasket(basketId);

            await _basketRepository.DeleteAsync(basket);
        }

        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Guard.ParameterNotNullOrEmpty(userName, nameof(userName));
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
            Guard.ParameterNotNull(quantities, nameof(quantities));
            Basket basket = await GetBasket(basketId);

            //assumption when an input product to update does not exists in basket products
            //do nothing with this product
            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.ProductItemId.ToString(), out var quantity))
                {
                    item.Quantity = quantity;
                }
            }
            await _basketRepository.UpdateAsync(basket);
        }

        private async Task<Basket> GetBasket(int id)
        {
            Basket basket = await _basketRepository.GetByIdWithItemsAsync(id);

            Guard.EntityNotNull(id, basket);

            return basket;
        }
    }

}
