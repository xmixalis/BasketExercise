using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketModelResponse
    {
        [JsonProperty("basketid", NullValueHandling = NullValueHandling.Ignore)]
        public int BasketId { get; set; }
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<BasketModelItem> Items { get; set; }
    }
}
