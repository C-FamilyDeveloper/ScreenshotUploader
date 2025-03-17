using Microsoft.Extensions.Options;
using MVVMUtilities.Services;
using ScreenshotUploader.Builders.FileDestinationBuilder.Implementations;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class SteamFacadeService : ISteamFacadeService
    {
        private readonly IFileQueryService fileService;
        private readonly ISteamDirectoryService steamDirectoryService;
        private readonly IAppIdService appIdService;

        public SteamFacadeService(IFileQueryService fileService,
            ISteamDirectoryService steamDirectoryService,
            IAppIdService appIdService)
        {
            this.fileService = fileService;
            this.steamDirectoryService = steamDirectoryService;
            this.appIdService = appIdService;
        }

        public void UploadScreenshots(string? remoteSteamPath, IEnumerable<string> screenshots, string gameId)
        {
            if (remoteSteamPath is null)
            {
                throw new ArgumentException("Необходимо выбрать корневую папку Steam");
            }

            if (!screenshots.Any())
            {
                throw new ArgumentException("Необходимо выбрать изображения");
            }

            if (string.IsNullOrWhiteSpace(gameId))
            {
                throw new ArgumentException("Необходимо ввести AppId приложения", "Ошибка");
            }

            var correctId = appIdService.GetCorrectAppId(gameId);
            var gameFolderPath = steamDirectoryService.CreateFolder(remoteSteamPath, correctId);
            var screenPath = steamDirectoryService.CreateFolder(gameFolderPath, @"screenshots");
            var tumbPath = steamDirectoryService.CreateFolder(screenPath, @"thumbnails");

            var fileQuery = new FileQueryBuilder()
                .AddFileGroupAppId(correctId)
                .AddFilePaths(screenshots)
                .AddDestination(screenPath)
                .AddDestination(tumbPath)
                .Build();

            fileService.Upload(fileQuery);
        }
    }
}
