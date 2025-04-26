using ScreenshotUploader.DAL.DataContext.Abstractions;
using ScreenshotUploader.DAL.DataContext.Implementations;
using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions.DAL.RecentUsedGames;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations.DAL.RecentUsedGames
{
    public class RecentUsedGameService : IRecentUsedGamesService
    {
        public void Create(Game entity)
        {
            var dataContext = new FileDataContext();
            if (dataContext.RecentUsedGames.Any(i => i.Name == entity.Name && i.AppId == entity.AppId))
            {
                throw new Exception("Игра уже добавлена");
            }
            var newDalObject = new RecentUsedGame
            {
                AppId = entity.AppId,
                Name = entity.Name,
            };
            dataContext.RecentUsedGames = dataContext.RecentUsedGames.Append(newDalObject);
            dataContext.SaveChanges();
        }

        public IEnumerable<Game> Read()
        {
            var dataContext = new FileDataContext();
            return dataContext.RecentUsedGames.Select(i => new Game
            {
                AppId = i.AppId,
                Name = i.Name
            });
        }
    }
}
