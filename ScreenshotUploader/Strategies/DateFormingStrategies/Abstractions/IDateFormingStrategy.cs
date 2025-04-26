using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Strategies.DateFormingStrategies.Abstractions
{
    public interface IDateFormingStrategy
    {
        DateTime FormDateTime(string filePath);
    }
}
