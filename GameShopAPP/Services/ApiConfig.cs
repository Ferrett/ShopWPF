using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GameShopAPP.Services
{
    public static class ApiConfig
    {
        public static readonly string ApiURL;

        static ApiConfig()
        {
            string json = File.ReadAllText("appsettings.json");
            dynamic jsonObj = JsonConvert.DeserializeObject(json)!;

            ApiURL = jsonObj.ApiUrl;
        }
    }
}
