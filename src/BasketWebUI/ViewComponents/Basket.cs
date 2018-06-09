using BasketWebUI.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BasketWebUI;
using BasketWebUI.Interfaces;
using BasketWebUI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BasketWebUI.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketWebService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Basket(IBasketWebService basketService,
                        SignInManager<ApplicationUser> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var vm = new BasketComponentViewModel();
            vm.ItemsCount = (await GetBasketViewModelAsync()).BasketItems.Sum(i => i.Quantity);
            return View(vm);
        }

        private async Task<BasketIndexViewModel> GetBasketViewModelAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                return await _basketService.GetOrCreateBasketForUser(User.Identity.Name);
            }
            string anonymousId = GetBasketIdFromCookie();
            if (anonymousId == null) return new BasketIndexViewModel();
            return await _basketService.GetOrCreateBasketForUser(anonymousId);
        }

        private string GetBasketIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            }
            return null;
        }
    }
}
