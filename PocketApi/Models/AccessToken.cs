using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PocketApi.Models
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("consumerkey")]
        public string ConsumerKey { get; set; }
    }
}
