using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Models;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Implementations.GameFrequancies
{
    public class AppIdEqualSpecification : ISpecification<GameUsedFrequancy>
    {
        private readonly string appId;

        public AppIdEqualSpecification(string appId)
        {
            this.appId = appId;
        }

        public bool IsSatisfiedBy(GameUsedFrequancy source)
        {
            return source.GameAppId == appId;
        }
    }
}
