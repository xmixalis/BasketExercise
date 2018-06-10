using System;
using System.IO;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketApi.Web.Controllers
{
    /// <summary>
    /// Errors controller of the API. 
    /// If a runtime error happens it is redirected here so that is handled if needed.
    /// </summary>
    [Route("[controller]")]
    public class ErrorController : Controller
    {

        /// <summary>
        /// Get and handle (if needed) the error
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IActionResult GetAsync()
        {
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                // Get which route the exception occurred at
                string routeWhereExceptionOccurred = exceptionFeature.Path;

                // Get the exception that occurred
                Exception exceptionThatOccurred = exceptionFeature.Error;

                // TODO: Do something with the exception
                // Log it with NLog?
                // Send an e-mail, text? 
                
            }
            
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
