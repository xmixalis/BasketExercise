using System;
using System.Collections.Generic;
using System.Text;

namespace BasketApi.Client.Services
{
    public abstract class ServiceBase
    {
        protected string _baseAddress;
        public ServiceBase(string baseAddress)
        {
            _baseAddress = baseAddress;
        }
    }
}
