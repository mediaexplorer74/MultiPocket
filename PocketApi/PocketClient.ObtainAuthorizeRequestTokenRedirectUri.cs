using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketApi
{
    public partial class PocketClient
    {
        public Uri ObtainAuthorizeRequestTokenRedirectUri(RequestToken RequestToken, Uri RedirectUri)
        {
            Uri uri = 
                new Uri
                (
                    $"https://getpocket.com/auth/authorize?request_token={RequestToken.Code}" +
                    $"&redirect_uri={RedirectUri.ToString()}"
                );
            return uri;
        }
    }
}
