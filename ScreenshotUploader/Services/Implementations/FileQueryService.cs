using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class FileQueryService : IFileQueryService
    {
        private readonly IFreeDateReservingService freeDateReservingService;
        private readonly IScreenshotInfoService screenshotInfoService;

        public FileQueryService(IFreeDateReservingService freeDateReservingService, IScreenshotInfoService screenshotInfoService)
        {
            this.freeDateReservingService = freeDateReservingService;
            this.screenshotInfoService = screenshotInfoService;
        }

        public void Upload(FileQuery fileQuery)
        {
            var lastScreenshotIndex = screenshotInfoService
                .GetInfo(fileQuery.Destinations.First())?.Number ?? 0;
            freeDateReservingService.Reset(fileQuery.FilePaths.Count());
            for (var i = 0; i < fileQuery.FilePaths.Count(); i++)
            {
                var newDate = freeDateReservingService.Reserve();
                var newName = $"{newDate:yyyyMMddHHmmss}_{lastScreenshotIndex + i + 1}.jpg";
                foreach (var destination in fileQuery.Destinations)
                {
                    CopyFileToDirectoryWithNewName(fileQuery.FilePaths.ElementAt(i), destination, newName);
                    SetFileMetadata(fileQuery.FilePaths.ElementAt(i), newDate);
                }
            }
        }

        private void CopyFileToDirectoryWithNewName(string filePath, string directoryPath, string newName)
        {
            File.Copy(filePath, Path.Combine(directoryPath, newName));
        }

        private void SetFileMetadata(string fileName, DateTime dateTime)
        {
            File.SetCreationTimeUtc(fileName, dateTime);
            File.SetLastAccessTimeUtc(fileName, dateTime);
            File.SetLastWriteTimeUtc(fileName, dateTime);
        }
    }
}
