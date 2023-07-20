using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class AuthServices
    {
        VMResponse response = new VMResponse();
        private static readonly HttpClient _httpClient = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";

        public AuthServices(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<bool> CheckRegisterByEmail(string email, int id)
        {
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiAuth/CheckRegisterByEmail/{email}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
            return isExist;
        }

        public async Task<VMResponse> Register(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + "apiAuth/Register", content);

            if (request.IsSuccessStatusCode)
            {
                var apiResponse = await request.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            }
            else
            {
                response.Success = false;
                response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return response;
        }
    }
}
