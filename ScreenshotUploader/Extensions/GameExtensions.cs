using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Implementations.SteamGame;

namespace ScreenshotUploader.Extensions
{
    public static class GameExtensions
    {
        public static bool IsEmpty(this Game game)
        {
            return new GameEmptySpecification().IsSatisfiedBy(game);
        }

        public static bool IsGameDLC(this Game game, Game pretendDLC)
        {
            return new GameNameStartWithSpecification(game.Name).IsSatisfiedBy(pretendDLC);
        }
    }
}
