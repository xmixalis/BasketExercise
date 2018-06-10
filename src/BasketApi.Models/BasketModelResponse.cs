using System.Collections.Generic;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object with full details for a given basket
    /// </summary>
    public class BasketModelResponse
    {
        /// <summary>
        /// Basket ID of the basket repository
        /// </summary>
        [JsonProperty("basketid")]
        public int BasketId { get; set; }
        /// <summary>
        /// UserID connected with this basket
        /// </summary>
        [JsonProperty("userid")]
        public string UserId { get; set; }
        /// <summary>
        /// Details for the products contained in this basket
        /// </summary>
        [JsonProperty("items")]
        public IEnumerable<BasketModelItem> Items { get; set; }
    }
}
