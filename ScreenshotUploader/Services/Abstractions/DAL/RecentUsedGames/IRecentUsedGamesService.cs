using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.DAL.RecentUsedGames
{
    public interface IRecentUsedGamesService : ICreatable<Game>, IReadable<Game>
    {
    }
}
