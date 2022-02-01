using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketApi
{
    public partial class PocketClient
    {
        private AccessToken _accessToken;
        private string _consumerKey;

        public PocketClient(AccessToken accessToken)
            : this(accessToken.ConsumerKey)
        {
            _accessToken = accessToken;
        }

        public PocketClient(string consumerKey)
        {
            _consumerKey = consumerKey;
            this.InitializeHttpClient();
        }

    }
}
