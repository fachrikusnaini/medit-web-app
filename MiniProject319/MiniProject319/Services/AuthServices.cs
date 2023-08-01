using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;
using MailKit.Net.Smtp;
using MiniProject319.DataModels;

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

        public async Task<VMResponse> SetPassword(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/SetPassword", content);


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

        public async Task<VMResponse> Biodata(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/Biodata", content);


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
        //public async Task<bool> CheckOTP(string token)
        //{
        //    string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiAuth/CheckOTP/{token}");
        //    bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
        //    return isExist;
        //}

        public async Task<VMResponse> CheckOTP(string token)
        {
            string json = JsonConvert.SerializeObject(token);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/CheckOTP/{token}", content);


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
        public async Task<VMResponse> CheckEmail(string email)
        {
            string json = JsonConvert.SerializeObject(email);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/CheckEmail/{email}", content);


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

        public async Task<VMResponse> ForgotPassword(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/ForgotPassword", content);


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
        public async Task<VMResponse> SetPassword_ForgotPassword(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/SetPassword_ForgotPassword", content);


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

        public async Task<VMResponse> ResendOTP(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/ResendOTP", content);


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

        public async Task<VMResponse> ResendOTPDaftar(VMm_user dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiAuth/ResendOTPDaftar", content);


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
