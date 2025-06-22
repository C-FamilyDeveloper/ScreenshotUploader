using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Models;
using ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD;
using ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD.AdditionalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.DAL
{
    public interface IGameUsedFrequancyService : ICreatable<GameFrequancy>, 
        IReadable<GameFrequancy>, 
        IUpdatable<GameFrequancy>,
        IReadableBySpecification<GameUsedFrequancy>
    {
        
    }
}
