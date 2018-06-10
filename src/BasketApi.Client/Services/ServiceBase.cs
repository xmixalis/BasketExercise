using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Client.Services
{
    /// <summary>
    /// Base class for client services.
    /// Ensures that a service will be accessed given a base address
    /// </summary>
    public abstract class ServiceBase
    {
        protected string _baseAddress;
        public ServiceBase(string baseAddress)
        {
            _baseAddress = baseAddress;
        }
    }
}
