using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.DAL.DataObjects
{
    public class GameLastUsingDateTime
    {
        public string AppId { get; set; }
        public DateTime LastUsingDateTime { get; set; }
    }
}
