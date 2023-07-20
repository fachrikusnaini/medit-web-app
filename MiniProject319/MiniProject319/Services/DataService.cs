using Newtonsoft.Json;
using MiniProject319.ViewModels;
using MiniProject319.DataModels;
using System.Text;

namespace MiniProject319.Services
{
    public class DataService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public DataService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMUser>> GetAllData()
        {
            List<VMUser> data = new List<VMUser>();
            string apiResponse = await client.GetStringAsync(RouteAPI + "apiProfile/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMUser>>(apiResponse);

            return data;

        }

        public async Task<VMUser> GetDataById(int id)
        {
            VMUser data = new VMUser();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMUser>(apiResponse);

            return data;
        }


        public async Task<VMResponse> Edit(VMUser dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiProfile/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //proses membaca respon dari API
                var apiRespon = await request.Content.ReadAsStringAsync();
                //proses convert hasil respon dari API ke object
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;

        }



    }
}
