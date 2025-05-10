using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.DAL.DataContext.Abstractions
{
    public interface IFileDataContextBase : IDisposable, IUnitOfWork
    {
    }
}
