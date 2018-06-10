using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BasketWebUI.Interfaces
{
    public interface IUserService
    {
        string GetUserName(HttpContext context,
            IRequestCookieCollection requestCookies, 
            IResponseCookies responseCookies);
    }
}
