using System.Threading.Tasks;
using BasketApi.Client.Helpers;
using BasketApi.Models;

namespace BasketApi.Client.Services
{
    /// <summary>
    /// Class that serves requests supported for Basket
    /// </summary>
    public class BasketService : ServiceBase
    {
        public BasketService(string baseAddress) : base(baseAddress) { }

        /// <summary>
        /// Returns a basket for a specific user
        /// </summary>
        /// <param name="userId">Unique User ID of the user for the basket
        /// It could be the logged on user or a unique value (ie a GUID stored in a cookie)
        /// </param>
        /// <returns>Basket object for the user</returns>
        public async Task<BasketModelResponse> GetBasketForUser(string userId)
        {
            return await new ApiHttpClient(_baseAddress).GetAsync<BasketModelResponse>(UriHelpers.UserBasketUri(userId));
        }

        /// <summary>
        /// Adds an item to a specific basket.
        /// If the item already exists in the basket, the quantity is increased instead.
        /// </summary>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemRequest">Object with details for the item to be added
        /// </param>
        /// <returns>Action success response</returns>
        public async Task<BasketAddItemResponse> AddBasketItem(int basketId, BasketAddItemRequest itemRequest)
        {
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketAddItemResponse>(UriHelpers.AddBasketItemUri(basketId), itemRequest);
        }

        /// <summary>
        /// Updates the quantity of basket items supplied
        /// </summary>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemsRequest">Object with details for the items to be updated</param>
        /// <returns>ction success response</returns>
        public async Task<BasketUpdateResponse> UpdateBasketItem(int basketId, BasketUpdateItemsRequest itemsRequest)
        {
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketUpdateResponse>(UriHelpers.UpdateBasketItemUri(basketId), itemsRequest);
        }

        /// <summary>
        /// Removes a specific item from a basket
        /// </summary>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemRequest">Object with details for the item to be removed</param>
        /// <returns>ction success response</returns>
        public async Task<BasketRemoveItemResponse> RemoveBasketItem(int basketId, BasketRemoveItemRequest itemRequest)
        {
            return await new ApiHttpClient(_baseAddress).PostAsJsonAsync<BasketRemoveItemResponse>(UriHelpers.RemoveBasketItemUri(basketId), itemRequest);
        }
    }
}