using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketApi.Models;
using BasketWebUI.Infrastructure.Identity;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketWebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketWebService _basketService;
        private readonly IUserService _userService;

        public BasketController(
            IBasketWebService basketService,
            IUserService userservice)
        {
            _basketService = basketService;
            _userService = userservice;
        }

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
            string userId = _userService.GetUserName(HttpContext, Request.Cookies, Response.Cookies);

            BasketIndexViewModel b = await _basketService.GetOrCreateBasketForUser(userId);

            return b;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateItem(Dictionary<int, int> items)
        {
            if (items == null)
            {
                return RedirectToAction("Index", "Products");
            }

            BasketIndexViewModel basketViewModel = await GetCurrentUserBasket();
            BasketUpdateResponse response = await _basketService.UpdateBasketItem(basketViewModel.Id, items);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            BasketIndexViewModel basketViewModel = await GetCurrentUserBasket();
            BasketRemoveItemResponse response = 
                await _basketService.RemoveBasketItem(basketViewModel.Id, productId);

            return RedirectToAction("Index");
        }

    }
}