using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketResponseBase
    {
        [JsonProperty("ok")]
        public bool Success { get; set; }
    }
}
