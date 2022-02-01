using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Serialization;//using System.Text.Json.Serialization;
using Newtonsoft.Json; //using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

namespace PocketApi
{
    public partial class PocketClient
    {
        private HttpClient _httpClient = new HttpClient();

        private void InitializeHttpClient()
        {
            _httpClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
        }

        private async Task<string> ApiPostAsync(Uri requestUri, object body)
        {
            string jsonBody = JsonConvert.SerializeObject(body);
            StringContent content = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
