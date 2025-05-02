using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScreenshotUploader.Models.SteamModels.Responces;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Strategies.SerializeStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class SteamAPIService : ISteamAPIService
    {
        public async Task<string> GetGameNameByAppId(string appId, CancellationToken cancellationToken)
        {
            using var handler = new HttpClientHandler()
            {
                Proxy = HttpClient.DefaultProxy,
                UseProxy = true
            };
            using var httpClient = new HttpClient(handler);
            var response = await httpClient.GetAsync(
                $"https://store.steampowered.com/api/appdetails?appids={Convert.ToInt32(appId)}",
                cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var settings = new JsonSerializerSettings
            {       
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SteamAppIdNamingStrategy(appId)
                }
            };

            var json = JsonConvert.DeserializeObject<SteamResponse>(content, settings);
            return json.game.data.name;
        }
    }
}
