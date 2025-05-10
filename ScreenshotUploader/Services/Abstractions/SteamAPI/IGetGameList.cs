using ScreenshotUploader.Models;
using ScreenshotUploader.Models.SteamModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.SteamAPI
{
    public interface IGetGameList
    {
        Task<IEnumerable<Game>> GetGameListAsync(CancellationToken cancellationToken);
    }
}
