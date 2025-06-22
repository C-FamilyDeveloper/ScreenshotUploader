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
    public class ScreenshotsStatisticsFacadeService : IScreenshotsStatisticsFacadeService
    {
        private readonly IFrequancyStatisticsService frequancyStatisticsService;
        private readonly IDateTimeStatisticsService dateTimeStatisticsService;

        public ScreenshotsStatisticsFacadeService(IFrequancyStatisticsService frequancyStatisticsService, IDateTimeStatisticsService dateTimeStatisticsService)
        {
            this.frequancyStatisticsService = frequancyStatisticsService;
            this.dateTimeStatisticsService = dateTimeStatisticsService;
        }

        public void ActualizeStatistics(string appId, int frequancy)
        {
            frequancyStatisticsService.ActualizeFrequancy(appId, frequancy);
            dateTimeStatisticsService.ActualizeDate(appId);
        }
    }
}
