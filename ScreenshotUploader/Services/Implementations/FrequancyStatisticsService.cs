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
    public class FrequancyStatisticsService : IFrequancyStatisticsService
    {
        private readonly IGameUsedFrequancyService gameUsedFrequancyService;

        public FrequancyStatisticsService(IGameUsedFrequancyService gameUsedFrequancyService)
        {
            this.gameUsedFrequancyService = gameUsedFrequancyService;
        }

        public void ActualizeFrequancy(string appId, int frequancy)
        {
            var specification = new AppIdEqualSpecification(appId);
            var frequancyDTO = gameUsedFrequancyService.ReadBySpecification(specification).FirstOrDefault();
            if (frequancyDTO is not null)
            {
                var updatedFrequancyDTO = new GameFrequancy
                {
                    AppId = frequancyDTO.GameAppId,
                    Frequency = frequancyDTO.UsedFrequancy + frequancy
                };
                gameUsedFrequancyService.Update(updatedFrequancyDTO);
                return;
            }
            var entity = new GameFrequancy
            {
                AppId = appId,
                Frequency = frequancy
            };
            gameUsedFrequancyService.Create(entity);
        }
    }
}
