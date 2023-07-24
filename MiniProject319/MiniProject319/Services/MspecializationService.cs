using MiniProject319.DataModels;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class MspecializationService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public MspecializationService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }


        public async Task<List<MSpecialization>> GetAllData()
        {
            List<MSpecialization> data = new List<MSpecialization>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiMspecialization/GetAllData");
            data = JsonConvert.DeserializeObject<List<MSpecialization>>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Create(MSpecialization dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu dikirim sebagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiMspecialization/Save", content);

            if (request.IsSuccessStatusCode)
            {
                //proses memmbaca respon dari API
                var apiRespon = await request.Content.ReadAsStringAsync();

                //proses convert hasil respon dari API ke Object
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
            }
            else
            {
                respon.Success = false;
                respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respon;
        }
        public async Task<MSpecialization> GetDataById(int id)
        {
            MSpecialization data = new MSpecialization();
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiMspecialization/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MSpecialization>(apiRespon);
            return data;

        }

        public async Task<bool> CheckMSpecializationByName(string Name, int id)
        {
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiMspecialization/CheckMSpecializationByName/{Name}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiRespon);

            return isExist;

        }

        public async Task<VMResponse> Edit(MSpecialization dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu dikirim sebagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiMspecialization/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //proses memmbaca respon dari API
                var apiRespon = await request.Content.ReadAsStringAsync();

                //proses convert hasil respon dari API ke Object
                respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon)!;
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
            var request = await client.DeleteAsync(RouteAPI + $"apiMspecialization/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                var apiRespon = await request.Content.ReadAsStringAsync();

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
