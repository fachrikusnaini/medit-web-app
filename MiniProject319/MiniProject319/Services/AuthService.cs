using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteApi = "";

        public AuthService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteApi = this.configuration["RouteAPI"];
        }

        public async Task<VMTblUsers> CheckLogin(string email)
        {
            VMTblUsers data = new VMTblUsers();
            string apiRespon = await client.GetStringAsync(RouteApi + $"apiAuth/CheckLogin/{email}");
            data = JsonConvert.DeserializeObject<VMTblUsers>(apiRespon)!;

            return data;
        }
    }
}
