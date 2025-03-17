using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions
{
    public interface ISteamFacadeService
    {
        void UploadScreenshots(string? remoteSteamPath, IEnumerable<string> screenshots, string gameId);
    }
}
