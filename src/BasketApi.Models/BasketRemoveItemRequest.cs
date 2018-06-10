using Newtonsoft.Json;

namespace BasketApi.Models
{
    /// <summary>
    /// Object to request a basket item removal
    /// </summary>
    public class BasketRemoveItemRequest
    {
        /// <summary>
        /// Product ID that has to be removed from the basket
        /// </summary>
        [JsonProperty("productid")]
        public int ProductId { get; set; }
    }
}
