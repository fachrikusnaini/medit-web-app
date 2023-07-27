using MiniProject319.DataModels;
using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class DoctorService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

      

        public DoctorService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<VMMDoctorSpecialist> GetProfileDoctor(int IdDoctor)
        {
            VMMDoctorSpecialist data = new VMMDoctorSpecialist();
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiDoctorProfile/GetProfileDoctor/{IdDoctor}");
            data = JsonConvert.DeserializeObject<VMMDoctorSpecialist>(apiRespon)!;

            return data;
        }

        public async Task<VMResponse> Create(TDoctorTreatment dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu dikirim sebagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiDoctorProfile/Save", content);

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
        public async Task<bool> CheckTDoctorTreatmentByName(string Name, int id)
        {
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiDoctorProfile/CheckTDoctorTreatmentByName/{Name}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiRespon);

            return isExist;

        }
        public async Task<TDoctorTreatment> GetDataById(int id)
        {
            TDoctorTreatment data = new TDoctorTreatment();
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiDoctorProfile/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<TDoctorTreatment>(apiRespon);
            return data;

        }

        public async Task<VMResponse> Delete(int id)
        {
            var request = await client.DeleteAsync(RouteAPI + $"apiDoctorProfile/Delete/{id}");

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
