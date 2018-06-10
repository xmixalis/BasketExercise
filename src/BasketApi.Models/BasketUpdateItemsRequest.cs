using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object with a list of basket items to be updated
    /// </summary>
    public class BasketUpdateItemsRequest
    {
        /// <summary>
        /// List with details of the basket items to be updated
        /// </summary>
        [JsonProperty("basketitems")]
        public List<BasketUpdateItem> Items { get; set; }
    }
}
