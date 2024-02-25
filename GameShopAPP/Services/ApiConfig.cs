using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static string Token;
        private static readonly string ConfigPath = "appsettings.json";

        static ApiConfig()
        {
            string json = File.ReadAllText(ConfigPath);
            dynamic jsonObj = JsonConvert.DeserializeObject(json)!;

            ApiURL = jsonObj.ApiUrl;
            Token = jsonObj.Token;  
        }

        public static void UpdateToken(string token)
        {
            Token = token;
            JObject jsonObject = JObject.Parse(File.ReadAllText(ConfigPath));
            jsonObject["Token"] = token;

            File.WriteAllText(ConfigPath, jsonObject.ToString());
        }
    }
}
