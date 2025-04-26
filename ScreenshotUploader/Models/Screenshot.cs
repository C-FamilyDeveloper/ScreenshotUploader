using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models
{
    public class Screenshot
    {
        public string ScreenshotPath { get; set; }
        public string AppId { get; set; }        
        public string GameName { get; set; }
    }
}
