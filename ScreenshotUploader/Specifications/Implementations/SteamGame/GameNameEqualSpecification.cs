using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Specifications.Abstractions;

namespace ScreenshotUploader.Specifications.Implementations.SteamGame
{
    public class GameNameEqualSpecification : ISpecification<Game>
    {
        private readonly string appName;

        public GameNameEqualSpecification(string appName)
        {
            this.appName = appName;
        }

        public bool IsSatisfiedBy(Game source)
        {
            return source.Name == appName;
        }
    }
}
