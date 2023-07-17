using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class RoleService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public RoleService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }


        public async Task<List<VMMrole>> GetAllData()
        {
            List<VMMrole> data = new List<VMMrole>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiRole/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMMrole>>(apiResponse);

            return data;
        }
    }
}
