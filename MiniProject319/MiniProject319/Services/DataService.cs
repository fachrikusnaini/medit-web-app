using Newtonsoft.Json;
using MiniProject319.ViewModels;
using MiniProject319.DataModels;
using System.Text;
using System.Net.Http;

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
            data = JsonConvert.DeserializeObject<VMUser>(apiResponse)!;

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

        public async Task<VMResponse> Register(VMUser dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await client.PostAsync(RouteAPI + "apiProfile/EditEmail", content);


            if (request.IsSuccessStatusCode)
            {
                var apiResponse = await request.Content.ReadAsStringAsync();
                respon = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;
        }

        public async Task<VMResponse> CheckOTP(string token)
        {
            string json = JsonConvert.SerializeObject(token);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await client.PostAsync(RouteAPI + $"apiProfile/CheckOTP/{token}", content);


            if (request.IsSuccessStatusCode)
            {
                var apiResponse = await request.Content.ReadAsStringAsync();
                respon = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;
        }

        public async Task<bool> CheckEmail(string email, int id)
        {
            //string json = JsonConvert.SerializeObject(email);
            //StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            //var request = await client.PostAsync(RouteAPI + $"apiProfile/CheckEmail/{email}", content);


            //if (request.IsSuccessStatusCode)
            //{
            //    var apiResponse = await request.Content.ReadAsStringAsync();
            //    respon = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            //}
            //else
            //{
            //    respon.Success = false;
            //    respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            //}
            //return respon;

            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/CheckEmail/{email}/{id}");
            bool isExis = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isExis;
        }

        public async Task<bool> CheckPassword(string password, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiProfile/CheckPassword/{password}/{id}");
            bool isexist = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isexist;
        }

        public async Task<VMResponse> SureEditP(MUser dataParam)
        {
            // Proses convert dari objek ke string
            string json = JsonConvert.SerializeObject(dataParam);

            // Preses convert string menjadi Json lalu dikirim sbagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Proses memanggil api dan mengirimkan body
            var request = await client.PutAsync(RouteAPI + "apiProfile/SureEditP", content);

            if (request.IsSuccessStatusCode)
            {
                // Proses membaca respon membca api
                var apiRespon = await request.Content.ReadAsStringAsync();

                // Proses Convert hasil respon dari api ke objeck
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
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
