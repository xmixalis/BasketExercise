using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BasketApi.Client.Exceptions
{
    /// <summary>
    /// Exception class for errors thrown by the Client.
    /// </summary>
    public class ApiResponseException : Exception
    {
        /// <summary>
        /// HTTP status code returned by call to service.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; protected set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="httpCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public ApiResponseException(HttpStatusCode httpCode, string message, Exception innerException = null) :
            base(string.IsNullOrWhiteSpace(message) ? $"Request returned an error code: {httpCode}" : message, innerException)
        {
            HttpStatusCode = httpCode;
        }
    }
}
