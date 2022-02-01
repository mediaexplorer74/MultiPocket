using Newtonsoft.Json.Serialization;//System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    public class RequestToken
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("state")]
        public object State { get; set; }
    }
}
