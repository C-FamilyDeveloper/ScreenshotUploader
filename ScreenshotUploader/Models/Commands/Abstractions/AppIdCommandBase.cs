using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models.Commands.Abstractions
{
    public abstract class AppIdCommandBase
    {
        public abstract bool IsCanExecute(string appId);
        public abstract string GetRightAppId(string appId);
    }
}
