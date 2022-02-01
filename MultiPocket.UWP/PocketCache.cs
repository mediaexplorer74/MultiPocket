using PocketApi;
using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPocket.UWP
{
    public class PocketCache
    {
        private PocketClient _pocketClient;

        public bool CurrentlySyncing { get; set; }
        public ObservableCollection<PocketItem> PocketItems { get; private set; } = new ObservableCollection<PocketItem>();
        public DateTime LastSyncDateTime { get; private set; } = new DateTime(1970, 1, 1);

        public PocketCache(PocketClient pocketClient)
        {
            _pocketClient = pocketClient;
        }

        public async Task SyncArticlesAsync()
        {
            CurrentlySyncing = true;

            try
            {
                DateTime newSyncDateTime = DateTime.UtcNow;
                IEnumerable<PocketItem> newPocketItems = 
                    await _pocketClient.GetPocketItemsAsync(LastSyncDateTime);

                foreach (PocketItem pocketItem in newPocketItems)
                {
                    switch (pocketItem.Status)
                    {
                        case "0":
                            if (!PocketItems.Any(pi => pi.Id == pocketItem.Id))
                                PocketItems.Insert(0, pocketItem);
                            break;
                        case "1":
                            if (PocketItems.Any(pi => pi.Id == pocketItem.Id))
                                PocketItems.First(pi => pi.Id == pocketItem.Id).Status = "1";
                            else
                                PocketItems.Insert(0, pocketItem);
                            break;
                        case "2":
                            if(PocketItems.Any(pi => pi.Id == pocketItem.Id))
                                PocketItems.Remove(PocketItems.First(pi => pi.Id == pocketItem.Id));
                            break;
                    }
                }

                LastSyncDateTime = newSyncDateTime;
                CurrentlySyncing = false;
            } catch (Exception e)
            {
                CurrentlySyncing = false;
                throw (e);
            }
        }

        public void SetCacheContent(DateTime newLastSyncDateTime, ObservableCollection<PocketItem> newPocketItems)
        {
            LastSyncDateTime = newLastSyncDateTime;
            PocketItems = newPocketItems;
        }

        public async Task AddArticleAsync(Uri uri)
        {
            PocketItem pocketItem = await _pocketClient.AddPocketItemAsync(uri);
            await SyncArticlesAsync();
        }

        public async Task DeleteArticleAsync(PocketItem pocketItem)
        {
            await _pocketClient.DeletePocketItemAsync(pocketItem);
            await SyncArticlesAsync();
        }

    }
}
