using ScreenshotUploader.Builders.FileDestinationBuilder.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Builders.FileDestinationBuilder.BuilderParts.Abstractions
{
    public interface IAppIdPart<T> 
    {
        T AddFileGroupAppId(string appId);
    }
}
