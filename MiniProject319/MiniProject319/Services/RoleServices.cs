using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class RoleServices
    {
        VMResponse response = new VMResponse();
        private static readonly HttpClient _httpClient = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        public RoleServices(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMm_role>> GetAllData()
        {
            List<VMm_role> data = new List<VMm_role>();

            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + "apiRole/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMm_role>>(apiResponse)!;

            return data;
        }
    }
}
