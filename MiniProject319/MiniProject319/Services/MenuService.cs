using Microsoft.EntityFrameworkCore.ChangeTracking;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class MenuService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteApi = "";

        public MenuService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteApi = this.configuration["RouteAPI"];
        }

        public async Task<List<VMListMenu>> GetListMenu(int IdRole)
        {
            List<VMListMenu> data = new List<VMListMenu> ();
            string apiRespon = await client.GetStringAsync(RouteApi + "apiMenu/GetListMenu/{IdRole}");
            data = JsonConvert.DeserializeObject<List<VMListMenu>>(apiRespon)!;

            return data;
        }
    }
}
