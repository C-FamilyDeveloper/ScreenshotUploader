using ScreenshotUploader.Models.Commands.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models.Commands.Implementations
{
    public class UrlAppId : AppIdCommandBase
    {
        private readonly Regex regexFirst = new(@"app/(\d+)/");
        private readonly Regex regexSecond = new (@"app/(\d+)");

        public override string GetRightAppId(string appId)
        {
            if (regexFirst.IsMatch(appId))
            {
                return regexFirst.Match(appId).Groups[1].Value;
            }
            return regexSecond.Match(appId).Groups[1].Value;
        }

        public override bool IsCanExecute(string appId)
        {
            return regexFirst.IsMatch(appId) || regexSecond.IsMatch(appId);
        }
    }
}
