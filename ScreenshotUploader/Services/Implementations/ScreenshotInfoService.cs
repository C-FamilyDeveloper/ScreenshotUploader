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
    public class ScreenshotInfoService : IScreenshotInfoService
    {
        public ScreenshotInfo? GetInfo(string directoryPath)
        {
            var files = Directory.GetFiles(directoryPath)
                .Select(i =>
                {
                    return (Path: i, Number: GetScreenshotNumber(i));
                });

            if (!files.Any())
            {
                return null;
            }

            var lastScreenshot =
                files.OrderByDescending(i => i.Number)
                .First();

            return new()
            {
                Date = GetScreenshotDate(lastScreenshot.Path),
                Number = lastScreenshot.Number
            };
        }

        private int GetScreenshotNumber(string path)
        {
            return Convert.ToInt32(Path.GetFileNameWithoutExtension(path).Split('_').Last());
        }

        private DateTime GetScreenshotDate(string path)
        {
            var filename = Path.GetFileNameWithoutExtension(path);
            return DateTime.ParseExact(filename.Split('_').First(), "yyyyMMddHHmmss", null);
        }
    }
}
