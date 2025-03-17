using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class SearchRemoteFolderSteamService : ISearchRemoteFolderSteamService
    {
        public string GetRemoteFolderSteam(string rootSteamDirectory)
        {
            if (!IsRightSteamDirectory(rootSteamDirectory))
            {
                throw new ArgumentException("Выбранная папка не является корневой папкой Steam");
            }
            return GetRemoteSubDirectory(rootSteamDirectory);
        }

        private bool IsRightSteamDirectory(string rootSteamDirectory)
        {
            return rootSteamDirectory.EndsWith(@"\Steam");
        }

        private string GetRemoteSubDirectory(string rootSteamDirectory)
        {
            var userData = Path.Combine(rootSteamDirectory, @"userdata");
            return GetConcreteSubDirectory(userData);
        }

        private string GetConcreteSubDirectory(string currentDirectory)
        {
            var subDirectories = Directory.EnumerateDirectories(currentDirectory,
                "*",
                SearchOption.AllDirectories);
            var sourceDirectory = string.Empty;
            Parallel.ForEach(subDirectories, (i, state) =>
            {
                if (IsRightRemoteDirectory(i))
                {
                    sourceDirectory = i;
                    state.Break();
                }
            });
            if (sourceDirectory == string.Empty)
            {
                throw new Exception("Произошла непредвиденная ошибка");
            }
            return GetParentDirectory(GetParentDirectory(GetParentDirectory(sourceDirectory)));
        }

        private bool IsRightRemoteDirectory(string path)
        {
            var Regex = new Regex(@"\\(\d+)\\(\d+)\\remote\\(\d+)\\screenshots");
            return Regex.IsMatch(path);
        }

        private string GetParentDirectory(string path)
        {
            return Directory.GetParent(path).FullName;
        }
    }
}
