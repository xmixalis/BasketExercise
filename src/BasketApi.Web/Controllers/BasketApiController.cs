using System.Collections.Generic;
using System.Threading.Tasks;
using BasketApi.Models;
using BasketApi.Web.Interfaces;
using BasketApi.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using BasketApi.Web.ModelConverters;

namespace BasketApi.Web.Controllers
{
    [Route("api/Basket")]
    public class BasketApiController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IProductService _productsService;

        public BasketApiController(IBasketService basketService, IProductService productService) {
            _basketService = basketService;
            _productsService = productService;
        }

        /// <summary>
        /// Action that returns a basket for a specific user. 
        /// </summary>
        /// <param name="userid">User Id for the basket</param>
        /// <returns>
        /// The basket from the database. 
        /// If no basket is found for the user, a new one is created.
        /// </returns>
        [HttpGet("{userid}")]
        public async Task<BasketModelResponse> GetForUser(string userid)
        {
            Basket b = await _basketService.GetOrCreateBasketForUser(userid);

            //query the product items to add extra product details to the response
            int[] productIds = b.Items.Select(item => item.ProductItemId).ToArray();
            IEnumerable<ProductItem> productItems = 
                await _productsService.ListAsync(p=> productIds.Contains(p.Id));

            b.UserId = userid;
            BasketModelResponse response = b.ToBasketModelResponse(productItems);
            return response;
        }

        /// <summary>
        /// Action that updates the quantities for a specific basket 
        /// </summary>
        /// <param name="basketid">Basket ID</param>
        /// <param name="requestObject">List of the items with the quantities to be updated</param>
        /// <returns>Success if it is successfull</returns>
        [HttpPost("Update/{basketid}")]
        public async Task<IActionResult> UpdateBasket(int basketid, [FromBody]BasketUpdateItemsRequest requestObject)
        {
            Dictionary<string, int> data = requestObject.Items.Select(i => new { product=i.ProductId, qnt=i.Quantity}).ToDictionary(d=>d.product.ToString(),d=>d.qnt);
            await _basketService.SetQuantities(basketid, data);
            return Ok(new BasketUpdateResponse() { Success = true });
        }

        /// <summary>
        /// Action that removes an item from the basket
        /// </summary>
        /// <param name="basketid">Basket ID</param>
        /// <param name="request">Product ID to be removed</param>
        /// <returns>Success if it is successfull</returns>
        [HttpPost("RemoveItem/{basketid}")]
        public async Task<IActionResult> RemoveItemFromBasket(int basketid, [FromBody]BasketRemoveItemRequest request)
        {
            await _basketService.RemoveItemFromBasket(basketid, request.ProductId);
            return Ok(new BasketRemoveItemResponse() { Success = true });
        }

        /// <summary>
        /// Action that adds an item to the basket.
        /// If the item exists, it increases the quantity.
        /// </summary>
        /// <param name="basketid">Basket ID</param>
        /// <param name="request">Item to be added with details</param>
        /// <returns>Success if it is successfull</returns>
        [HttpPost("AddItem/{basketid}")]
        public async Task<IActionResult> AddItemToBasket(int basketid, [FromBody]BasketAddItemRequest request)
        {
            await _basketService.AddItemToBasket(basketid, request.ProductId, request.Price, request.Quantity);

            return Ok(new BasketAddItemResponse() { Success = true });            
        }
   }
}
