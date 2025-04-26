using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.DAL.BaseCRUD
{
    public interface IReadable<T>
    {
        IEnumerable<T> Read();
    }
}
