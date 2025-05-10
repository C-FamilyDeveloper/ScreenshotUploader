using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.SteamAPI
{
    public interface ISteamAPIService : IGetGameList, IGetNameByAppId
    {
    }
}
