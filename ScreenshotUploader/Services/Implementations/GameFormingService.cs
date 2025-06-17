using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class GameFormingService : IGameFormingService
    {
        private readonly IGameUsedFrequancyService gameUsedFrequancyService;
        private readonly IRecentUsedGamesService recentUsedGamesService;

        public GameFormingService(IGameUsedFrequancyService gameUsedFrequancyService, IRecentUsedGamesService recentUsedGamesService)
        {
            this.gameUsedFrequancyService = gameUsedFrequancyService;
            this.recentUsedGamesService = recentUsedGamesService;
        }

        public IEnumerable<Game> GetGamesWithStatisticsSorting()
        {
            var games = recentUsedGamesService.Read();
            var statistics = gameUsedFrequancyService.Read();
            var joined = from game in games
                join s in statistics on game.AppId equals s.AppId into gamestats
                from gs in gamestats.DefaultIfEmpty()
                orderby gs?.Frequency descending
                select game;
            return joined;
        }
    }
}
