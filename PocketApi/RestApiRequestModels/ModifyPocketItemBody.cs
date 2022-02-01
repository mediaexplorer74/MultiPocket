﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; 

namespace PocketApi.RestApiRequestModels
{
    internal class ModifyPocketItemBody
    {
        [JsonProperty("consumer_key")]
        public string ConsumerKey { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("actions")]
        public PocketModifyAction[] Actions { get; set; }

    }
}
