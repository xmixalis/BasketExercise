using System.Threading.Tasks;
using BasketApi.Client.Helpers;
using BasketApi.Models;

namespace BasketApi.Client.Services
{
    public class BasketService : ServiceBase
    {
        public BasketService(string baseAddress) : base(baseAddress) { }

        public async Task<BasketModelResponse> GetBasketForUser(string userId)
        {
            string serviceURI = "api/Basket/{0}";
            var getBasketUri = string.Format(serviceURI, userId);
            return await new ApiHttpClient(_baseAddress).GetAsync<BasketModelResponse>(getBasketUri);
        }

        public async Task<BasketAddItemResponse> AddBasketItem(int basketId, BasketAddItemRequest request)
        {
            string serviceURI = "api/Basket/AddItem/{0}";
            var getBasketUri = string.Format(serviceURI, basketId);
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketAddItemResponse>(getBasketUri, request);
        }

        public async Task<BasketUpdateResponse> UpdateBasketItem(int basketId, BasketUpdateItemsRequest request)
        {
            string serviceURI = "api/Basket/Update/{0}";
            var getBasketUri = string.Format(serviceURI, basketId);
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketUpdateResponse>(getBasketUri, request);
        }

        public async Task<BasketRemoveItemResponse> RemoveBasketItem(int basketId, BasketRemoveItemRequest request)
        {
            string serviceURI = "api/Basket/RemoveItem/{0}";
            var getBasketUri = string.Format(serviceURI, basketId);
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketRemoveItemResponse>(getBasketUri, request);
        }
    }
}