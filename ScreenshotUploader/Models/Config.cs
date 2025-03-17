using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScreenshotUploader.Models
{
    public class Config 
    {
        public string SteamFolder { get; set; }

        public string RemoteFolder { get; set; }
    }
}
