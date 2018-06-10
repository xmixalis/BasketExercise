using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Web.Controllers
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [Route("")]
        public IActionResult Get()
        {
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            string message="";
            if (exceptionFeature != null)
            {
                // Get which route the exception occurred at
                string routeWhereExceptionOccurred = exceptionFeature.Path;

                // Get the exception that occurred
                Exception exceptionThatOccurred = exceptionFeature.Error;
                message = exceptionThatOccurred.Message;
                // TODO: Do something with the exception
                // Log it with NLog?
                // Send an e-mail, text? 
            }
            
            return BadRequest(message);
            //return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
