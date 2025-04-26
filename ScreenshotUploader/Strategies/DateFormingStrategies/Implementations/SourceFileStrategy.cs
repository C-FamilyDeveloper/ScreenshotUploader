using ScreenshotUploader.Strategies.DateFormingStrategies.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Strategies.DateFormingStrategies.Implementations
{
    public class SourceFileStrategy : IDateFormingStrategy
    {
        public DateTime FormDateTime(string sourceFilePath)
        {
            return File.GetCreationTimeUtc(sourceFilePath);
        }
    }
}
