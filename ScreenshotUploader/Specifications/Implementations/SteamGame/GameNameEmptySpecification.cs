using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Abstractions;

namespace ScreenshotUploader.Specifications.Implementations
{
    public class GameNameEmptySpecification : ISpecification<Game>
    {
        public bool IsSatisfiedBy(Game source)
        {
            return string.IsNullOrWhiteSpace(source.AppId);
        }
    }
}
