using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Factories.Abstractions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions.DAL;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations.DAL
{
    public class GameUsedFrequancyService : IGameUsedFrequancyService
    {
        private readonly IFileContextFactory fileContextFactory;

        public GameUsedFrequancyService(IFileContextFactory fileContextFactory)
        {
            this.fileContextFactory = fileContextFactory;
        }

        public void Create(GameFrequancy entity)
        {
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            using var dataContext = fileContextFactory.CreateContext();
            if (dataContext.GameUsedFrequancys.Any(i => i.GameAppId == entity.AppId))
            {
                throw new ArgumentException($"Сущность {nameof(entity)} уже создана");
            }
            var newDalObject = new GameUsedFrequancy
            {
                GameAppId = entity.AppId,
                UsedFrequancy = entity.Frequency
            };
            dataContext.GameUsedFrequancys = dataContext.GameUsedFrequancys.Append(newDalObject);
            dataContext.SaveChanges();
        }

        public IEnumerable<GameFrequancy> Read()
        {
            using var dataContext = fileContextFactory.CreateContext();
            return dataContext.GameUsedFrequancys.Select(i => new GameFrequancy
            {
                AppId = i.GameAppId,
                Frequency = i.UsedFrequancy
            });
        }

        public IEnumerable<GameUsedFrequancy> ReadBySpecification(ISpecification<GameUsedFrequancy> specification)
        {
            using var dataContext = fileContextFactory.CreateContext();
            return dataContext.GameUsedFrequancys.Where(specification.IsSatisfiedBy);
        }

        public void Update(GameFrequancy entity)
        {
            using var dataContext = fileContextFactory.CreateContext();
            ArgumentNullException.ThrowIfNull(entity, nameof(entity));
            var selected = dataContext.GameUsedFrequancys.Where(i => i.GameAppId == entity.AppId).FirstOrDefault();
            ArgumentNullException.ThrowIfNull(selected, nameof(selected));
            selected.UsedFrequancy = entity.Frequency;
            dataContext.SaveChanges();
        }
    }
}
