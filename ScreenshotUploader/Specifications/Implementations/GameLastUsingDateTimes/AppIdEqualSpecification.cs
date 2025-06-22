using ScreenshotUploader.DAL.DataObjects;
using ScreenshotUploader.Specifications.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Specifications.Implementations.GameLastUsingDateTimes
{
    public class AppIdEqualSpecification : ISpecification<GameLastUsingDateTime>
    {
        private readonly string appId;

        public AppIdEqualSpecification(string appId)
        {
            this.appId = appId;
        }

        public bool IsSatisfiedBy(GameLastUsingDateTime source)
        {
            return source.AppId == appId;
        }
    }
}
