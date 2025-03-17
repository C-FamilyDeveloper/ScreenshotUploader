using ScreenshotUploader.Models.Commands.Abstractions;
using ScreenshotUploader.Models.Commands.Implementations;
using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotUploader.Services.Implementations
{
    public class AppIdService : IAppIdService
    {
        private IEnumerable<AppIdCommandBase> commands = [new NumericAppId(), new UrlAppId()];

        public string GetCorrectAppId(string appId)
        {
            foreach (var command in commands)
            {
                if (command.IsCanExecute(appId))
                {
                    return command.GetRightAppId(appId);
                }
            }
            throw new ArgumentException("Задан неверный формат AppId");
        }
    }
}
