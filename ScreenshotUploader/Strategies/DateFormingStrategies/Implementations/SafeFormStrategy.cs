using ScreenshotUploader.Strategies.DateFormingStrategies.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Strategies.DateFormingStrategies.Implementations
{
    public class SafeFormStrategy : IDateFormingStrategy
    {
        private static DateTime baseDateTime = DateTime.UtcNow;

        public DateTime FormDateTime(string sourceFilePath)
        {
            baseDateTime = baseDateTime.AddSeconds(-1);
            return baseDateTime;
        }
    }
}
