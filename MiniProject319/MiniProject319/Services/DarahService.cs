using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class DarahService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public DarahService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<MBloodGroup>> GetAllData()
        {
            List<MBloodGroup> data = new List<MBloodGroup>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiBlood/GetAllData");
            data = JsonConvert.DeserializeObject<List<MBloodGroup>>(apiResponse);

            return data;
        }

        public async Task<MBloodGroup> GetDataById(int id)
        {
            MBloodGroup data = new MBloodGroup();
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiBlood/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MBloodGroup>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Create(MBloodGroup dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiBlood/Save", content);

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

        public async Task<bool> CheckDarah(string code, int id)
        {
            string apiResponse = await client.GetStringAsync(RouteAPI + $"apiBlood/CheckDarah/{code}/{id}");
            bool isexist = JsonConvert.DeserializeObject<bool>(apiResponse);

            return isexist;
        }
        //apiBlood belum ada perintah checkbyname

        public async Task<VMResponse> Edit(MBloodGroup dataParam)
        {
            //convert object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //convert string ke json then dikirim sebagai req body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiBlood/Edit", content);

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
            var request = await client.DeleteAsync(RouteAPI + $"apiBlood/Delete/{id}");

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
