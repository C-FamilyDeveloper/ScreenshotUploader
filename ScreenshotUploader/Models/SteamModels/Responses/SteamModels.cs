using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models.SteamModels.Responces
{
    public class SteamResponse
    {
        public Game game { get; set; }
    }

    public class Game
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string type { get; set; }
        public string name { get; set; }
    }
}
