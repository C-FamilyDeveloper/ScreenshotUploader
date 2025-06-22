using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Factories.Abstractions;
using ScreenshotUploader.Factories.Implementations;
using ScreenshotUploader.Services.Abstractions.DAL;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations.DAL
{
    public class GameLastUsingDateTimeService : IGameLastUsingDateTimeService
    {
        private readonly IFileContextFactory fileContextFactory;

        public GameLastUsingDateTimeService(IFileContextFactory fileContextFactory)
        {
            this.fileContextFactory = fileContextFactory;
        }

        public void Create(GameLastUsingDateTime entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            using var dataContext = fileContextFactory.CreateContext();
            if (dataContext.GameLastUsingDateTimes.Any(i => i.AppId == entity.AppId))
            {
                throw new ArgumentException($"Сущность {nameof(entity)} уже создана");
            }
            var newDalObject = new GameLastUsingDateTime
            {
                AppId = entity.AppId,
                LastUsingDateTime = entity.LastUsingDateTime
            };
            dataContext.GameLastUsingDateTimes = dataContext.GameLastUsingDateTimes.Append(newDalObject);
            dataContext.SaveChanges();
        }

        public IEnumerable<GameLastUsingDateTime> Read()
        {
            using var dataContext = fileContextFactory.CreateContext();
            return dataContext.GameLastUsingDateTimes;
        }

        public IEnumerable<GameLastUsingDateTime> ReadBySpecification(ISpecification<GameLastUsingDateTime> specification)
        {
            using var dataContext = fileContextFactory.CreateContext();
            return dataContext.GameLastUsingDateTimes.Where(specification.IsSatisfiedBy);
        }

        public void Update(GameLastUsingDateTime entity)
        {
            using var dataContext = fileContextFactory.CreateContext();
            var dto = dataContext.GameLastUsingDateTimes.Where(i => i.AppId == entity.AppId)
                .FirstOrDefault() ?? throw new ArgumentException($"Сущность {nameof(entity)} не создана");
            dto.LastUsingDateTime = entity.LastUsingDateTime;
            dataContext.SaveChanges();
        }
    }
}
