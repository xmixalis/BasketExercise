using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Client;
using BasketApi.Models;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using Microsoft.Extensions.Logging;

namespace BasketWebUI.Services
{
    /// <summary>
    /// This is a UI-specific service so belongs in UI project. 
    /// It works with UI-specific types.
    /// </summary>
    public class BasketService : IBasketWebService
    {
        //depedencies to inject
        private readonly ILogger<BasketService> _logger;
        private readonly BasketAPISettings _config;

        //constructor
        public BasketService(ILoggerFactory loggerFactory, BasketAPISettings config)
        {
            _logger = loggerFactory.CreateLogger<BasketService>();
            _config = config;
        }

        public async Task<BasketIndexViewModel> GetOrCreateBasketForUser(string userId)
        {
            _logger.LogInformation("GetOrCreateBasketForUser called.");

            BasketApiClient client = new BasketApiClient(_config.APIBaseUrl);
            BasketModelResponse basket = await client.BasketService.GetBasketForUser(userId);

            return GetViewModelFromBasket(basket); 
        }
        private BasketIndexViewModel GetViewModelFromBasket(BasketModelResponse basket)
        {
            BasketIndexViewModel model = new BasketIndexViewModel();
            model.Id = basket.BasketId;
            model.UserId = basket.UserId;
            model.BasketItems = basket.Items.Select(i =>
            {
                BasketItemViewModel itemModel = new BasketItemViewModel()
                {
                    UnitPrice = i.Price,
                    Quantity = i.Quantity,
                    ProductItemId = i.ProductId,
                    ProductName = i.ProductName
                };

                return itemModel;
            }).ToList();

            return model;
        }

        public async Task<BasketAddItemResponse> AddItemToBasket(int basketid, int productid, decimal price, int quantity )
        {
            BasketApiClient client = new BasketApiClient(_config.APIBaseUrl);
            BasketAddItemResponse response = 
                await client.BasketService.AddBasketItem(
                    basketid, 
                    new BasketAddItemRequest()
                    {
                        Price = price,
                        ProductId = productid,
                        Quantity = quantity
                    });

            return response;
        }
        public async Task<BasketUpdateResponse> UpdateBasketItem(int basketid, Dictionary<int,int> quantities)
        {
            BasketApiClient client = new BasketApiClient(_config.APIBaseUrl);
            BasketUpdateResponse response =
                await client.BasketService.UpdateBasketItem(
                    basketid,
                    new BasketUpdateItemsRequest()
                    {
                        Items = quantities.Select(q => 
                            new BasketUpdateItem()
                            {
                                ProductId = q.Key,
                                Quantity = q.Value
                            }).ToList()
                    });

            return response;
        }

        public async Task<BasketRemoveItemResponse> RemoveBasketItem(int basketId, int productId)
        {
            BasketApiClient client = new BasketApiClient(_config.APIBaseUrl);
            BasketRemoveItemResponse response =
                await client.BasketService.RemoveBasketItem(
                    basketId, 
                    new BasketRemoveItemRequest() { ProductId = productId });
            return response;
        }
    }
}
