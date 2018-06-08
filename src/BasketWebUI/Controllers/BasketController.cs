using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Models;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketWebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketWebService _basketService;
        public BasketController(IBasketWebService basketService) => _basketService = basketService;

        // GET: Basket
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            BasketIndexViewModel b = await GetCurrentUserBasket();

            return View(b);
        }

        // POST: /Basket/AddToBasket
        [HttpPost]
        public async Task<IActionResult> AddToBasket(ProductItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
            {
                return RedirectToAction("Index", "Products");
            }

            BasketIndexViewModel basketViewModel = await GetCurrentUserBasket();
            BasketAddItemResponse response = await _basketService.AddItemToBasket(basketViewModel.Id, productDetails.Id, productDetails.Price, 1);

            return RedirectToAction("Index");
        }

        async Task<BasketIndexViewModel> GetCurrentUserBasket()
        {
            //get userId from cookie.
            //in case of an authenticated user this whould be the 
            //authenticated UserId
            string anonymousId = GetOrSetBasketCookie();

            BasketIndexViewModel b = await _basketService.GetOrCreateBasketForUser(anonymousId);

            return b;
        }
        private string GetOrSetBasketCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            string anonymousId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions);
            return anonymousId;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(Dictionary<int, int> items)
        {
            if (items == null)
            {
                return RedirectToAction("Index", "Products");
            }

            BasketIndexViewModel basketViewModel = await GetCurrentUserBasket();
            BasketUpdateResponse response = await _basketService.UpdateBasketItem(basketViewModel.Id, items);

            return RedirectToAction("Index");
        }
    }
}