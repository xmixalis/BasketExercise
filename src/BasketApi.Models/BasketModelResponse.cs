using System.Collections.Generic;
using Newtonsoft.Json;

namespace BasketApi.Models
{
    public class BasketModelResponse
    {
        [JsonProperty("basketid")]
        public int BasketId { get; set; }
        [JsonProperty("userid")]
        public string UserId { get; set; }
        [JsonProperty("items")]
        public IEnumerable<BasketModelItem> Items { get; set; }
    }
}
