using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.DAL;
using ScreenshotUploader.Specifications.Implementations.GameFrequancies;
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
            var specification = new AppIdEqualSpecification(appId.ToString());
            var frequancy = gameUsedFrequancyService.ReadBySpecification(specification).FirstOrDefault();
            if (frequancy is not null)
            {
                var frequancyDTO = new GameFrequancy
                {
                    AppId = frequancy.GameAppId,
                    Frequency = frequancy.UsedFrequancy + count
                };
                gameUsedFrequancyService.Update(frequancyDTO);
                return;
            }
            var entity = new GameFrequancy
            {
                AppId = appId.ToString(),
                Frequency = count
            };
            gameUsedFrequancyService.Create(entity);
        }
    }
}
