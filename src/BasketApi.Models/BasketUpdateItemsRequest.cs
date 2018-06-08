using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketUpdateItemsRequest
    {
        //[JsonProperty("basketid", NullValueHandling = NullValueHandling.Ignore)]
        //public int BasketId { get; set; }
        [JsonProperty("basketitems", NullValueHandling = NullValueHandling.Ignore)]
        public List<BasketUpdateItem> Items { get; set; }
    }
}
