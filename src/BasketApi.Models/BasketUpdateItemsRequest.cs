using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketUpdateItemsRequest
    {
        [JsonProperty("basketitems")]
        public List<BasketUpdateItem> Items { get; set; }
    }
}
