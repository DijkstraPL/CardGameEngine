using CardGame_Client.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CardGame_Client.Services
{
    public class DecksProvider : Service, IDecksProvider
    {
        public DecksProvider()
        {
        }

        public async Task<IEnumerable<string>> GetDecks()
        {
            using (WebClient webClient = new WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(Url + "/api/decks");
                return JsonConvert.DeserializeObject<List<string>>(json);
            }
        }
    }
}
