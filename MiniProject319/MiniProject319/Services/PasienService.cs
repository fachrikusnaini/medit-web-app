using Newtonsoft.Json;
using MiniProject319.ViewModels;


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

        public async Task<List<VMPasien>> GetAllData()
        {
            List<VMPasien> data = new List<VMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiPasien/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMPasien>>(apiResponse);

         
            Console.WriteLine(JsonConvert.SerializeObject(data));
            return data;
        }

        public async Task<List<VMPasien>> GetDataById(int id)
        {
            List<VMPasien> data = new List<VMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<List<VMPasien>>(apiResponse)!;

            return data;

        }

        public async Task<List<VMPasien>> GetDataByIdParent(int id)
        {
            List<VMPasien> data = new List<VMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataByIdParent/{id}");
            data = JsonConvert.DeserializeObject<List<VMPasien>>(apiResponse)!;

            return data;

        }

    }
}
