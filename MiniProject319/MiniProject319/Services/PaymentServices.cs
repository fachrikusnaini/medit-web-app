using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class PaymentServices
    {
        VMResponse response = new VMResponse();
        private static readonly HttpClient _httpClient = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        public PaymentServices(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<MPaymentMethod>> GetAllData()
        {
            List<MPaymentMethod> data = new List<MPaymentMethod>();

            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + "apiPayment/GetAllData");
            data = JsonConvert.DeserializeObject<List<MPaymentMethod>>(apiResponse)!;

            return data;
        }
        public async Task<bool> CheckNameIsExist(string name, int id)
        {
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiPayment/CheckNameIsExist/{name}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
            return isExist;
        }

        public async Task<VMResponse> Create(MPaymentMethod data)
        {
            //process conver from object to string
            string json = JsonConvert.SerializeObject(data);

            //process change string to be json then send by Request Body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //process call API and sending Body Request
            var request = await _httpClient.PostAsync(RouteAPI + "apiPayment/Save", content);

            if (request.IsSuccessStatusCode)
            {
                //process reading response from API
                var apiResponse = await request.Content.ReadAsStringAsync();

                //process conver from API to Object
                response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            }
            else
            {
                response.Success = false;
                response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return response;
        }
        public async Task<MPaymentMethod> GetDataById(int id)
        {
            MPaymentMethod data = new MPaymentMethod();
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiPayment/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MPaymentMethod>(apiResponse)!;
            return data;
        }

        public async Task<VMResponse> Edit(MPaymentMethod dataParam)
        {
            //process convert from object to string
            string json = JsonConvert.SerializeObject(dataParam);
            //process change string to be json then send by Request Body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //process call API and sending Body Request
            var request = await _httpClient.PostAsync(RouteAPI + "apiPayment/Edit", content);

            if (request.IsSuccessStatusCode)
            {
                //process reading response from API
                var apiResponse = await request.Content.ReadAsStringAsync();

                //process conver from API to Object
                response = JsonConvert.DeserializeObject<VMResponse>(apiResponse)!;
            }
            else
            {
                response.Success = false;
                response.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return response;
        }
        public async Task<VMResponse> Delete(int id)
        {
            //process call API and sending Body Request
            var request = await _httpClient.DeleteAsync(RouteAPI + $"apiPayment/Delete/{id}");

            if (request.IsSuccessStatusCode)
            {
                //process reading response from API
                var apiResponse = await request.Content.ReadAsStringAsync();

                //process conver from API to Object
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
