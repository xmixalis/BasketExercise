using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketWebUI.Infrastructure.Identity;
using BasketWebUI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BasketWebUI.Services
{
    public class UserService : IUserService
    {
        private IRequestCookieCollection _requestCookies;
        private IResponseCookies _responseCookies;
        HttpContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string GetUserName(HttpContext context,
            IRequestCookieCollection requestCookies, 
            IResponseCookies responseCookies)
        {
            _requestCookies = requestCookies;
            _responseCookies = responseCookies;
            _context = context;

            //in case of an authenticated user get the basket for the user 
            //otherwise get anonymous from cookie.
            if (_signInManager.IsSignedIn(context.User))
            {
                return  context.User.Identity.Name;
            }
            else
            {
                return GetOrSetUserCookie();
            }

        }

        private string GetOrSetUserCookie()
        {
            if (_requestCookies.ContainsKey(Constants.BASKET_COOKIENAME))
            {
                return _requestCookies[Constants.BASKET_COOKIENAME];
            }
            string anonymousId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            _responseCookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions);
            return anonymousId;
        }
    }
}
