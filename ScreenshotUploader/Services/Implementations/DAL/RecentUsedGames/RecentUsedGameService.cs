using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Factories.Abstractions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions.DAL.RecentUsedGames;

namespace ScreenshotUploader.Services.Implementations.DAL.RecentUsedGames
{
    public class RecentUsedGameService : IRecentUsedGamesService
    {
        private readonly IFileContextFactory fileContextFactory;

        public RecentUsedGameService(IFileContextFactory fileContextFactory)
        {
            this.fileContextFactory = fileContextFactory;
        }

        public void Create(Game entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            ArgumentException.ThrowIfNullOrWhiteSpace(entity.Name, nameof(entity.Name));
            ArgumentException.ThrowIfNullOrWhiteSpace(entity.AppId, nameof(entity.AppId));
            using var dataContext = fileContextFactory.CreateContext();
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
            using var dataContext = fileContextFactory.CreateContext();
            return dataContext.RecentUsedGames.Select(i => new Game
            {
                AppId = i.AppId,
                Name = i.Name
            });
        }
    }
}
