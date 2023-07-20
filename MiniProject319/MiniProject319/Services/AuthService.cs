
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.services
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();
        public AuthService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<VMMUser> CheckLogin(string email, string password)
        {
            VMMUser data = new VMMUser();

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/CheckLogin/{email}/{password}");
            data = JsonConvert.DeserializeObject<VMMUser>(apiResponse)!;

            return data;
        }


        //public async Task<List<VMMenuAccess>> MenuAccess(int IdRole)
        //{
        //    List<VMMenuAccess> data = new List<VMMenuAccess>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/MenuAccess/{IdRole}");
        //    data = JsonConvert.DeserializeObject<List<VMMenuAccess>>(apiResponse)!;

        //    return data;
        //}
    }
}
