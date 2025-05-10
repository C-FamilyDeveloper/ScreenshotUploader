using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Abstractions.Resources
{
    public interface IResourceSet<T>
    {
        void SetResource(IEnumerable<T> resource);
    }
}
