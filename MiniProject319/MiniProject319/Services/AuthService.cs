
using MiniProject319.viewmodels;

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

        //public async Task<VMTblCustomer> CheckLogin(string email, string password)
        //{
        //    VMTblCustomer data = new VMTblCustomer();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/CheckLogin/{email}/{password}");
        //    data = JsonConvert.DeserializeObject<VMTblCustomer>(apiResponse)!;

        //    return data;
        //}


        //public async Task<List<VMMenuAccess>> MenuAccess(int IdRole)
        //{
        //    List<VMMenuAccess> data = new List<VMMenuAccess>();

        //    string apiResponse = await client.GetStringAsync(RouteAPI + $"apiAuth/MenuAccess/{IdRole}");
        //    data = JsonConvert.DeserializeObject<List<VMMenuAccess>>(apiResponse)!;

        //    return data;
        //}
    }
}
