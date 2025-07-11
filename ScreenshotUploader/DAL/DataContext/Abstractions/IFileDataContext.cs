﻿using ScreenshotUploader.DAL.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.DAL.DataContext.Abstractions
{
    public interface IFileDataContext : IFileDataContextBase
    {
        IEnumerable<RecentUsedGame> RecentUsedGames { get; set; }
        IEnumerable<GameUsedFrequancy> GameUsedFrequancys { get; set; }
        IEnumerable<GameLastUsingDateTime> GameLastUsingDateTimes { get; set;}
    }
}
