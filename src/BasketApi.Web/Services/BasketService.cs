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
        private readonly IAsyncRepository<Basket> _basketRepositoryAsync;
        private readonly IBasketRepository _brbasketRepository;

        public BasketService(IAsyncRepository<Basket> basketRepositoryAsync,IBasketRepository br)
        {
            _basketRepositoryAsync = basketRepositoryAsync;
            _brbasketRepository = br;
        }

        #region GetBasketByUserId
        public async Task<Basket> GetOrCreateBasketForUser(string userId)
        {
            Basket basket = await GetBasketByUserId(userId);

            if (basket == null)
                basket = await CreateBasketForUser(userId);

            basket = await _brbasketRepository.GetByIdWithItemsAsync(basket.Id);
            return basket;
        }
        async Task<Basket> GetBasketByUserId(string userId)
        {
            Basket basket = (await _basketRepositoryAsync.ListAsync(b => b.UserId == userId)).FirstOrDefault();

            return basket;
        }
        private async Task<Basket> CreateBasketForUser(string userId)
        {
            //improvement to make the ID of the entity to be auto increment
            Basket basket = new Basket() { UserId = userId };
            basket.Id = _basketRepositoryAsync.GetNewId(basket);
            await _basketRepositoryAsync.AddAsync(basket);

            return basket;
        }
        #endregion


        public async Task AddItemToBasket(int basketId, int productItemId, decimal price, int quantity)
        {
            var basket = await _brbasketRepository.GetByIdWithItemsAsync(basketId);
            //_basketRepository.GetByIdAsync(basketId);

            basket.AddItem(productItemId, price, quantity);

            await _basketRepositoryAsync.UpdateAsync(basket);
        }
        public async Task RemoveItemFromBasket(int basketId, int productItemId)
        {
            Basket basket = await _brbasketRepository.GetByIdWithItemsAsync(basketId);
                //_basketRepository.GetByIdAsync(basketId);

            basket.RemoveItem(productItemId);

            await _basketRepositoryAsync.UpdateAsync(basket);
        }
        public async Task ClearItemsFromBasket(int basketId)
        {
            Basket basket = await _brbasketRepository.GetByIdWithItemsAsync(basketId);
                //_basketRepository.GetByIdAsync(basketId);

            basket.ClearItems();

            await _basketRepositoryAsync.UpdateAsync(basket);
        }
        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepositoryAsync.GetByIdAsync(basketId);

            await _basketRepositoryAsync.DeleteAsync(basket);
        }

        public async Task<int> GetBasketItemCountAsync(string userName)
        {
            Utils.ParameterNotNullOrEmpty(userName, nameof(userName));
            Basket basket = (await _basketRepositoryAsync.ListAsync(b => b.UserId == userName)).FirstOrDefault();
            if (basket == null)
            {
                return 0;
            }
            int count = basket.Items.Sum(i => i.Quantity);

            return count;
        }
        public async Task SetQuantities(int basketId, Dictionary<string, int> quantities)
        {
            Utils.ParameterNotNull(quantities, nameof(quantities));
            Basket basket = await _brbasketRepository.GetByIdWithItemsAsync(basketId);
                
            Utils.EntityNotNull(basketId, basket);

            //assumption when an input product to update does not exists in basket products
            //do nothing with this product
            foreach (var item in basket.Items)
            {
                if (quantities.TryGetValue(item.ProductItemId.ToString(), out var quantity))
                {
                    item.Quantity = quantity;
                }
            }
            await _basketRepositoryAsync.UpdateAsync(basket);
        }
    }

}
