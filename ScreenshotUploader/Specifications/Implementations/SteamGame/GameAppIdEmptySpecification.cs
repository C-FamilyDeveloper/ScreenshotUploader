using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Implementations
{
    public class GameAppIdEmptySpecification : ISpecification<Game>
    {
        public bool IsSatisfiedBy(Game source)
        {
            return string.IsNullOrWhiteSpace(source.AppId);
        }
    }
}
