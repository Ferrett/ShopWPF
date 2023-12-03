using GameShopAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests.DeveloperRequest
{
    public interface IDeveloperApiRequest
    {
        Task<HttpResponseMessage> PostDeveloperRequest(Developer developer);
        Task<HttpResponseMessage> GetDeveloperRequest(int developerID);
        Task<HttpResponseMessage> GetAllDevelopersRequest();
        Task<HttpResponseMessage> PutDeveloperRequest(int developerID, Developer developer);
        Task<HttpResponseMessage> PutDeveloperLogoRequest(int developerID, BitmapImage bitmapImage);
        Task<HttpResponseMessage> DeleteDeveloperRequest(int developerID);
    }
}
