using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketRemoveItemRequest
    {
        [JsonProperty("productid")]
        public int ProductId { get; set; }
    }
}
