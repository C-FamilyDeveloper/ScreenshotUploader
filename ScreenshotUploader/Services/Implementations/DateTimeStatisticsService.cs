using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using ScreenshotUploader.Services.Abstractions.DAL;
using ScreenshotUploader.Services.Implementations.DAL;
using ScreenshotUploader.Specifications.Implementations.GameLastUsingDateTimes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class DateTimeStatisticsService : IDateTimeStatisticsService
    {
        private readonly IGameLastUsingDateTimeService gameLastUsingDateTimeService;

        public DateTimeStatisticsService(IGameLastUsingDateTimeService gameLastUsingDateTimeService)
        {
            this.gameLastUsingDateTimeService = gameLastUsingDateTimeService;
        }

        public void ActualizeDate(string appId)
        {
            var specification = new AppIdEqualSpecification(appId);
            var lastDateDTO = gameLastUsingDateTimeService.ReadBySpecification(specification).FirstOrDefault();
            var entity = new GameLastUsingDateTime
            {
                AppId = appId,
                LastUsingDateTime = DateTime.UtcNow
            };
            if (lastDateDTO is not null)
            {
                gameLastUsingDateTimeService.Update(entity);
                return;
            }
            gameLastUsingDateTimeService.Create(entity);
        }
    }
}
