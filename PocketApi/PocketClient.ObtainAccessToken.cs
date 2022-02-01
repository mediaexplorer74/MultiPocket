using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; //using System.Text.Json;
using System.Threading.Tasks;

namespace PocketApi
{
    public partial class PocketClient
    {
        private static Uri _obtainAccessTokenUri = new Uri($"https://getpocket.com/v3/oauth/authorize");

        public async Task<AccessToken> ObtainAccessTokenAsync(RequestToken RequestToken)
        {
            string response = await ApiPostAsync(
                  _obtainAccessTokenUri,
                   new ObtainAccessTokenBody()
                   {
                       ConsumerKey = _consumerKey,
                       RequestTokenCode = RequestToken.Code
                   }
               );
            AccessToken token = JsonConvert.DeserializeObject<AccessToken>(response);
            token.ConsumerKey = _consumerKey;

            this._accessToken = token;
            return token;
        }
    }
}
