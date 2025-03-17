using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class SteamDirectoryService : ISteamDirectoryService
    {
        public string CreateFolder(string path, string name)
        {
            var subdirectory = Path.Combine(path, name);
            Directory.CreateDirectory(subdirectory);
            return subdirectory;
        }
    }
}
