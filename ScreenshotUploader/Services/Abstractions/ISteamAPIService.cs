using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions
{
    public interface ISteamAPIService 
    {
        public Task<string> GetGameNameByAppId(string appId, CancellationToken cancellationToken);
    }
}
