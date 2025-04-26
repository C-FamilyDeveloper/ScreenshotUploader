using Newtonsoft.Json.Serialization;
using ScreenshotUploader.Models.SteamModels.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenshotUploader.Strategies.SerializeStrategies
{
    public class SteamAppIdNamingStrategy : NamingStrategy
    {
        private readonly string appId;

        public SteamAppIdNamingStrategy(string appId)
        {
            this.appId = appId;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == nameof(SteamResponse.game))
            {
                return appId;
            }
            return propertyName;
        }
    }
}
