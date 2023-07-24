using MiniProject319.ViewModels;
using Newtonsoft.Json;

namespace MiniProject319.Services
{
    public class DoctorService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteApi = "";

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
    }
}
