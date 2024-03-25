using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace GameShopAPP.Services
{
    public static class ApiConfig
    {
        private static readonly string ConfigPath = "appsettings.json";
        public static readonly string ApiURL;
        public static string Token;

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
