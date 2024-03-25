using GameShopAPP.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace GameShopAPP.Services.Requests
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
