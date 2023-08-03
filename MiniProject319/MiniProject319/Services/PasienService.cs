using Newtonsoft.Json;
using MiniProject319.ViewModels;
using System.Text;
using Newtonsoft.Json;
using MiniProject319.DataModels;

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

        public async Task<VMPasien> GetDataById(int id)
        {
            VMPasien data = new VMPasien();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMPasien>(apiResponse)!;

            return data;

        }

        public async Task<List<VMPasien>> GetDataByIdParent(int id)
        {
            List<VMPasien> data = new List<VMPasien>();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiPasien/GetDataByIdParent/{id}");
            data = JsonConvert.DeserializeObject<List<VMPasien>>(apiResponse)!;

            return data;

        }

        public async Task<VMResponse> Create(VMPasien dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiPasien/Save", content);

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

        public async Task<VMResponse> Edit(VMPasien dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiPasien/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiPasien/Delete/{id}");

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

        public async Task<VMResponse> MultipleDelete(List<int> listId)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(listId);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiPasien/MultipleDelete", content);

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
