using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; //using System.Text.Json;

namespace PocketApi
{
    public partial class PocketClient
    {
        private static Uri _obtainRequestTokenUri = new Uri($"https://getpocket.com/v3/oauth/request");

        public async Task<RequestToken> ObtainRequestTokenAsync(Uri CallBackUri)
        {
            string response = await ApiPostAsync(
               _obtainRequestTokenUri,
                new ObtainRequestTokenBody()
                {
                    ConsumerKey = _consumerKey,
                    RedirectUri = CallBackUri.ToString()
                }
            );

            RequestToken token = JsonConvert.DeserializeObject<RequestToken>(response); //JsonSerializer.Deserialize<RequestToken>(response);
            
            return token;
        }
    }
}
