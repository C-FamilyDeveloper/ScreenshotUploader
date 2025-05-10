using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using ScreenshotUploader.Specifications.Abstractions;

namespace ScreenshotUploader.Specifications.Implementations.SteamGame
{
    public class GameNameStartWithSpecification : ISpecification<Game>
    {
        private readonly string name;

        public GameNameStartWithSpecification(string name)
        {
            this.name = name;
        }

        public bool IsSatisfiedBy(Game source)
        {
            return source.Name.StartsWith(name);
        }
    }
}
