using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Specifications.Abstractions;

namespace ScreenshotUploader.Specifications.Implementations.SteamGame
{
    public class GameIdEqualSpecification : ISpecification<Game>
    {
        private readonly string appId;

        public GameIdEqualSpecification(string appId)
        {
            this.appId = appId;
        }

        public bool IsSatisfiedBy(Game source)
        {
            return source.AppId == appId;
        }
    }
}
