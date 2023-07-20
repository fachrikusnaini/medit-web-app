using MiniProject319.viewmodels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class RoleService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        private VMResponse respon = new VMResponse();

        public RoleService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }


        public async Task<List<VMMrole>> GetAllData()
        {
            List<VMMrole> data = new List<VMMrole>();

            string apiResponse = await client.GetStringAsync(RouteAPI + "apiRole/GetAllData");
            data = JsonConvert.DeserializeObject<List<VMMrole>>(apiResponse);

            return data;
        }

        public async Task<VMResponse> Create(VMMrole dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu dikirim sebagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PostAsync(RouteAPI + "apiRole/Save", content);

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

        public async Task<bool> CheckRoleByName(string roleName, int id)
        {
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiRole/CheckRoleByName/{roleName}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiRespon);

            return isExist;

        }

        public async Task<VMMrole> GetDataById(int id)
        {
            VMMrole data = new VMMrole();
            string apiRespon = await client.GetStringAsync(RouteAPI + $"apiRole/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<VMMrole>(apiRespon);
            return data;

        }

        public async Task<VMResponse> Edit(VMMrole dataParam)
        {
            //proses convert dari object ke string
            string json = JsonConvert.SerializeObject(dataParam);

            //proses mengubah string menjadi json lalu dikirim sebagai request body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //proses memanggil API dan mengirimkan Body
            var request = await client.PutAsync(RouteAPI + "apiRole/Edit", content);

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

        //public async Task<VMMrole> Delete(int id)
        //{
        //    var request = await client.DeleteAsync(RouteAPI + $"apiRole/Delete/{id}");

        //    if (request.IsSuccessStatusCode)
        //    {
        //        var apiRespon = await request.Content.ReadAsStringAsync();

        //        respon = JsonConvert.DeserializeObject<VMResponse>(apiRespon);
        //    }
        //    else
        //    {
        //        respon.Success = false;
        //        respon.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
        //    }

        //    return respon;
        //}
    }
}
