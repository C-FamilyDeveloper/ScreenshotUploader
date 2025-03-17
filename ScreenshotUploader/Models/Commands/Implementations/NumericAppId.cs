using ScreenshotUploader.Models.Commands.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models.Commands.Implementations
{
    public class NumericAppId : AppIdCommandBase
    {
        private Regex regex = new(@"^\d+$");

        public override string GetRightAppId(string appId)
        {
            return appId;
        }

        public override bool IsCanExecute(string appId)
        {
            return regex.IsMatch(appId);
        }
    }
}
