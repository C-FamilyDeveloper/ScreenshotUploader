using ScreenshotUploader.DAL.DataContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Factories.Abstractions
{
    public interface IFileContextFactory
    {
        IFileDataContext CreateContext();
    }
}
