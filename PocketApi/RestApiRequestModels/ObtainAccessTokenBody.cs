using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;

namespace PocketApi.RestApiRequestModels
{
    internal class ObtainAccessTokenBody
    {
        [JsonProperty("consumer_key")]
        public string ConsumerKey { get; set; }
        [JsonProperty("code")]
        public string RequestTokenCode { get; set; }
    }
}
