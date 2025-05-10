using ScreenshotUploader.DAL.DataContext.Abstractions;
using ScreenshotUploader.DAL.DataContext.Implementations;
using ScreenshotUploader.Factories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Factories.Implementations
{
    public class FileContextFactory : IFileContextFactory
    {
        public IFileDataContext CreateContext()
        {
            return new FileDataContext();
        }
    }
}
