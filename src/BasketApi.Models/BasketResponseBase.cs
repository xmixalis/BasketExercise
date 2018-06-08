using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketResponseBase
    {
        [JsonProperty("ok", NullValueHandling = NullValueHandling.Ignore)]
        public bool Success { get; set; }
    }
}
