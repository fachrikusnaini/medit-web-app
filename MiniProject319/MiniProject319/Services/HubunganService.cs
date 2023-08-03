using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class HubunganService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public HubunganService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<VMCustomerRelation>> GetAllData()
        {
            List<VMCustomerRelation> data = new List<VMCustomerRelation>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiHubungan/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMCustomerRelation>>(apiResponse);

            return data;
        }

        public async Task<MCustomerRelation> GetDataById(int id)
        {
            MCustomerRelation data = new MCustomerRelation();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiHubungan/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MCustomerRelation>(apiResponse);

            return data;
        }

        public async Task<bool> CheckRelation(string name, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiHubungan/CheckRelation/{name}/{id}");
            bool isexist = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isexist;
        }

        public async Task<VMResponse> Create(MCustomerRelation dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiHubungan/Save", content);

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

        public async Task<VMResponse> Edit(MCustomerRelation dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiHubungan/Edit", content);

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

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiHubungan/Delete/{id}");

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
