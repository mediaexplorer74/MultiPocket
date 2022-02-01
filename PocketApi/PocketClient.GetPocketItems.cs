using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json; //using System.Text.Json;
using System.Diagnostics;

namespace PocketApi
{
    public partial class PocketClient
    {
        private static Uri _getUri = new Uri($"https://getpocket.com/v3/get");

        public async Task<List<PocketItem>> GetPocketItemsAsync(DateTime lastSyncDateTime)
        {
            double lastSyncUnixTimestamp = 
                Converters.UnixTimestampConverter.ToUnixtimestamp(lastSyncDateTime);

            string apiResponse = await ApiPostAsync
            (
                _getUri,
                new GetPocketItemsBody()
                {
                    ConsumerKey = this._accessToken.ConsumerKey,
                    AccessToken = this._accessToken.Token,
                    DetailType = "complete",
                    //Since = lastSyncUnixTimestamp,
                    State = PocketItemState.All,
                    Sort = PocketItemSort.Oldest
                }
            );

            List<PocketItem> pocketItems = ConvertJsonToPocketItems(apiResponse);

            return pocketItems;
        }

        public async Task<List<PocketItem>> GetPocketItemsAsync()
        {
            List<PocketItem> pocketItems = await this.GetPocketItemsAsync(new DateTime(1970, 1, 1));
            return pocketItems;
        }

        private List<PocketItem> ConvertJsonToPocketItems(string json)
        {
            List<PocketItem> pocketItems = new List<PocketItem>();

            //JsonDocument apiJsonDocument = JsonDocument.Parse(json);
            JToken apiJsonDocument = JToken.Parse(json);

            //JsonElement jsonElement;
            JToken jsonElement;
            string statusString = "";

            //(apiJsonDocument.Root.GetProperty("status", out jsonElement))
            jsonElement = apiJsonDocument.Root.SelectToken("status");

            if (jsonElement != null)
                statusString = jsonElement.ToString();

            if (statusString == "1")
            {
                IJEnumerable<JToken> Llist = apiJsonDocument.Root.SelectToken("list").AsJEnumerable();
               
                // RnD: JsonProperty
                foreach (JToken pocketItemJsonProperty in Llist)
                {
                    /*                  
                    string s = pocketItemJsonProperty.ToString();
                    int found = s.IndexOf(":");
                    s = s.Substring(found + 2);                    

                    //RnD
                    //found = s.LastIndexOf("}");                    
                    //s = s.Substring(0, s.Length - 2);           


                    PocketItem pocketItem = JsonConvert.DeserializeObject<PocketItem>(s);
                    */

                    PocketItem pocketItem = Converters.PocketConverter.ConvertJsonToPocketItem(pocketItemJsonProperty);

                    pocketItems.Add(pocketItem);

                }
            }

            return pocketItems;
        }
    }
}
