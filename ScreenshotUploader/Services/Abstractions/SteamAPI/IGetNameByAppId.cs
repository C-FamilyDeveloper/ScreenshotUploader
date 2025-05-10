using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.SteamAPI
{
    public interface IGetNameByAppId
    {
        Task<string> GetGameNameByAppIdAsync(string appId, CancellationToken cancellationToken);
    }
}
