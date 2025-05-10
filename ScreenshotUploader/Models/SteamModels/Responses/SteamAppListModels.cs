using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models.SteamModels.Responses
{
    public class SteamApps
    {
        public Applist applist { get; set; }
    }

    public class Applist
    {
        public SteamApp[] apps { get; set; }
    }

    public class SteamApp
    {
        public int appid { get; set; }
        public string name { get; set; }
    }  
}
