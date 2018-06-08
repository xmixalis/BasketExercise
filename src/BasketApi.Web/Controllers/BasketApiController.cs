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

        [HttpGet("{userid}")]
        public async Task<BasketModelResponse> GetForUser(string userid)
        {
            Basket b = await _basketService.GetOrCreateBasketForUser(userid);
            List<ProductItem> productItems = await _productsService.GetAllItems();
            BasketModelResponse response = b.ToBasketModelResponse(productItems);
            return response;
        }

        [HttpPost("Update/{basketid}")]
        public async Task<IActionResult> UpdateBasket(int basketid, [FromBody]BasketUpdateItemsRequest requestObject)
        {
            Dictionary<string, int> data = requestObject.Items.Select(i => new { product=i.ProductId, qnt=i.Quantity}).ToDictionary(d=>d.product.ToString(),d=>d.qnt);
            await _basketService.SetQuantities(basketid, data);
            return Ok(new BasketUpdateResponse() { Success = true });
        }

        [HttpPost("RemoveItem/{basketid}")]
        public async Task<IActionResult> RemoveItemFromBasket(int basketid, [FromBody]BasketRemoveItemRequest request)
        {
            await _basketService.RemoveItemFromBasket(basketid, request.ProductId);
            return Ok(new BasketRemoveItemResponse() { Success = true });
        }

        [HttpPost("AddItem/{basketid}")]
        public async Task<IActionResult> AddItemToBasket(int basketid, [FromBody]BasketAddItemRequest request)
        {
            await _basketService.AddItemToBasket(basketid, request.ProductId, request.Price, request.Quantity);

            return Ok(new BasketAddItemResponse() { Success = true });            
        }
        /*
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
