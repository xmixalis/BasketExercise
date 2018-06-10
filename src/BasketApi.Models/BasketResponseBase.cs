using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Basic info for a response
    /// </summary>
    public class BasketResponseBase
    {
        [JsonProperty("ok")]
        public bool Success { get; set; }
    }
}
