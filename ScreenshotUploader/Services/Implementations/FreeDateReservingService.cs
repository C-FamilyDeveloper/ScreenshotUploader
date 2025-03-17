﻿using ScreenshotUploader.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Services.Implementations
{
    public class FreeDateReservingService : IFreeDateReservingService
    {
        private DateTime oldestDate = DateTime.UtcNow;

        private DateTime actualDate = DateTime.UtcNow;

        public DateTime Reserve()
        {
            actualDate = actualDate.AddSeconds(1);
            return actualDate;
        }

        public void Reset(int capacity)
        {
            oldestDate = oldestDate.AddSeconds(-(capacity+1));
            actualDate = oldestDate;
        }
    }
}
