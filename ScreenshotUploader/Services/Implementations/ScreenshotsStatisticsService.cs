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
    public class ScreenshotsStatisticsService : IScreenshotsStatisticsService
    {
        private readonly IGameUsedFrequancyService gameUsedFrequancyService;

        public ScreenshotsStatisticsService(IGameUsedFrequancyService gameUsedFrequancyService)
        {
            this.gameUsedFrequancyService = gameUsedFrequancyService;
        }

        public void AnalyzeStatistics(int appId, int count)
        {
            var entity = new GameFrequancy
            {
                AppId = appId.ToString(),
                Frequency = count
            };
            if (gameUsedFrequancyService.IsExist(entity))
            {
                gameUsedFrequancyService.Update(entity);
                return;
            }
            gameUsedFrequancyService.Create(entity);
        }
    }
}
