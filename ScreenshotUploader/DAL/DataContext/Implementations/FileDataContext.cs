using ScreenshotUploader.DAL.DataContext.Abstractions;
using ScreenshotUploader.DAL.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenshotUploader.DAL.DataContext.Implementations
{
    public class FileDataContext : FileDataContextBase, IFileDataContext
    {
        public IEnumerable<RecentUsedGame> RecentUsedGames { get ; set; }
        public IEnumerable<GameUsedFrequancy> GameUsedFrequancys { get; set; }
    }
}
