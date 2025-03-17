using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Builders.FileDestinationBuilder.Abstractions
{
    public interface IBuilder <T>
    {
        T Build();
    }
}
