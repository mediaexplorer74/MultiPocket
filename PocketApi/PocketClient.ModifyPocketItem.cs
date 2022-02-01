using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Serialization;

using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PocketApi
{
    public partial class PocketClient
    {
        private static Uri _modifyUri = new Uri($"https://getpocket.com/v3/send");

        public async Task<bool> DeletePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction pocketDeleteAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Delete,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(pocketDeleteAction);
        }

        public async Task<bool> ArchivePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction archiveModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Archive,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(archiveModifyAction);
        }

        public async Task<bool> ReAddPocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction reAddModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.ReAdd,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(reAddModifyAction);
        }

        public async Task<bool> MarkFavoritePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction favoriteModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Favorite,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(favoriteModifyAction);
        }

        public async Task<bool> UnFavoritePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction unfavoriteModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.UnFavorite,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(unfavoriteModifyAction);
        }

        private async Task<bool> ModifyPocketItemAsync(PocketModifyAction pocketModifyAction)
        {
            string apiResponse = await ApiPostAsync(
                _modifyUri,
                new ModifyPocketItemBody()
                {
                    ConsumerKey = _accessToken.ConsumerKey,
                    AccessToken = _accessToken.Token,
                    Actions = new PocketModifyAction[]
                    {
                        pocketModifyAction
                    }
                });

            //JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponse);
            var apiJsonDocument = JToken.Parse(apiResponse);

            //RnD
            //foreach (JsonProperty property in apiJsonDocument.Root.AsJEnumerable())//apiJsonDocument.RootElement.EnumerateObject()
            //{
            //    if (property.Name == "status")
            //    {
            //        if (property.Value.GetDouble() != 1)
            //            return false;
            //        else
            //            return true;
            //    }
            //}

            return false;
        }
    }
}
