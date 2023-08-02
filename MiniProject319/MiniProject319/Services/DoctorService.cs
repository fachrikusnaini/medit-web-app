using System.Drawing;
using System.Text;
using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class DoctorService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteApi = "";
        private VMResponse respon = new VMResponse();

        public DoctorService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteApi = this.configuration["RouteAPI"];
        }

        public async Task<VMDoctorSpecialist> GetProfileDoctor(int IdDoctor)
        {
            VMDoctorSpecialist data = new VMDoctorSpecialist();
            string apiRespon = await client.GetStringAsync(RouteApi + $"apiDoctorProfile/GetProfileDoctor/{IdDoctor}");
            data = JsonConvert.DeserializeObject<VMDoctorSpecialist>(apiRespon)!;

            return data;
        }

        public async Task<List<VMListDoctor>> GetAllDataDoctor()
        {
            List<VMListDoctor> data = new List<VMListDoctor>();
            string apiRespon = await client.GetStringAsync(RouteApi + $"apiDoctorProfile/GetAllDataDoctor");
            data = JsonConvert.DeserializeObject<List<VMListDoctor>>(apiRespon)!;

            return data;
        }

        public async Task<VMCariDokter> GetCariDoctor()
        {
            VMCariDokter data = new VMCariDokter();
            string apiRespon = await client.GetStringAsync(RouteApi + $"apiDoctorProfile/GetCariDokter");
            data = JsonConvert.DeserializeObject<VMCariDokter>(apiRespon)!;

            return data;
        }

        public async Task<VMResponse> Edit(VMDoctorSpecialist data)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(data);

            //proses mengubah string menjadi json 
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //memanggil API
            var request = await client.PutAsync(RouteApi + "apiDoctorProfile/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //membaca respon dari API
                var apiRespon = await request.Content.ReadAsStringAsync();

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
