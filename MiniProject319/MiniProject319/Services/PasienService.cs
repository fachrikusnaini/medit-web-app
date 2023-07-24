using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class PasienService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public PasienService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMMPasien>> GetAllData()
        {
            List<VMMPasien> data = new List<VMMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiPasien/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMMPasien>>(apiResponse);


            Console.WriteLine(JsonConvert.SerializeObject(data));
            return data;
        }

        public async Task<List<VMMPasien>> GetDataById(int id)
        {
            List<VMMPasien> data = new List<VMMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<List<VMMPasien>>(apiResponse);

            return data;

        }
    }
}
