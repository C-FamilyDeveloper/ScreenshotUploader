using ScreenshotUploader.Extensions;
using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Implementations.SteamGame
{
    public class GameNameSpecification : ISpecification<Game>
    {
        private ISpecification<Game> specification;

        public GameNameSpecification(string name)
        {
            specification = new GameNameEqualSpecification(name)
                .Or(new GameNameStartWithSpecification(name));
        }
        public bool IsSatisfiedBy(Game source)
        {
            return specification.IsSatisfiedBy(source);
        }
    }
}
