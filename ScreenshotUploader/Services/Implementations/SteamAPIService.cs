using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ScreenshotUploader.Models.SteamModels.Responces;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Services.Abstractions.SteamAPI;
using ScreenshotUploader.Strategies.SerializeStrategies;
using System.Net.Http;
using System.Net.Http.Json;

namespace ScreenshotUploader.Services.Implementations
{
    public class SteamAPIService : ISteamAPIService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public SteamAPIService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Models.Game>> GetGameListAsync(CancellationToken cancellationToken)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetFromJsonAsync<SteamApps>(
                $"https://api.steampowered.com/ISteamApps/GetAppList/v2",
                cancellationToken);
            return response.applist.apps.AsParallel().Select(i => new Models.Game
            {
                AppId = $"{i.appid}",
                Name = i.name          
            });
        }

        public async Task<string> GetGameNameByAppIdAsync(string appId, CancellationToken cancellationToken)
        {
            using var httpClient = httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(
            $"https://store.steampowered.com/api/appdetails?appids={Convert.ToInt32(appId)}&cc=us",
            cancellationToken);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SteamAppIdNamingStrategy(appId)
                }
            };

            var json = JsonConvert.DeserializeObject<SteamConcreteResponse>(content, settings);
            return json.game.data.name;
        }
    }
}
