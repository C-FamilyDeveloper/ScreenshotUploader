using Microsoft.Extensions.Options;
using MVVMUtilities.Services;
using ScreenshotUploader.Builders.FileDestinationBuilder.Implementations;
using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responces;
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

        public SteamFacadeService(IFileQueryService fileService,
            ISteamDirectoryService steamDirectoryService)
        {
            this.fileService = fileService;
            this.steamDirectoryService = steamDirectoryService;
        }

        public void UploadScreenshots(string? remoteSteamPath, IEnumerable<Screenshot> screenshots)
        {
            if (remoteSteamPath is null)
            {
                throw new ArgumentException("Необходимо выбрать корневую папку Steam");
            }

            if (!screenshots.Any())
            {
                throw new ArgumentException("Необходимо выбрать изображения");
            }

            var groups = screenshots.GroupBy(i => i.AppId);
            foreach (var group in groups)
            {
                UploadGame(remoteSteamPath, group.Select(i => i.ScreenshotPath), group.Key);
            }
        }

        private void UploadGame(string remoteSteamPath, IEnumerable<string> screenshots, string gameId)
        {
            var gameFolderPath = steamDirectoryService.CreateFolder(remoteSteamPath, gameId);
            var screenPath = steamDirectoryService.CreateFolder(gameFolderPath, @"screenshots");
            var tumbPath = steamDirectoryService.CreateFolder(screenPath, @"thumbnails");

            var fileQuery = new FileQueryBuilder()
                .AddFileGroupAppId(gameId)
                .AddFilePaths(screenshots)
                .AddDestination(screenPath)
                .AddDestination(tumbPath)
                .Build();

            fileService.Upload(fileQuery);
        }
    }
}
