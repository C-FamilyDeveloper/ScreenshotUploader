using ScreenshotUploader.Builders;
using ScreenshotUploader.Extensions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class ScreenshotsFormingService : IScreenshotsFormingService
    {
        public IEnumerable<Screenshot> CreateScreenshots(IEnumerable<string> paths, Game game)
        {
            if (!paths.Any())
            {
                throw new ArgumentException("Не выбраны скриншоты");
            }

            if (game is null)
            {
                throw new ArgumentException("Не выбрана игра");
            }

            return EnumerableBuilder.Create<Screenshot>(paths.Count())
                .With(i => i.GameName, game.Name)
                .With(i => i.AppId, game.AppId)
                .With(i => i.ScreenshotPath, paths);
        }
    }
}
