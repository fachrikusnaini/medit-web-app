using MiniProject319.DataModels;
using MiniProject319.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MiniProject319.Services
{
    public class BankServices
    {
        VMResponse response = new VMResponse();
        private static readonly HttpClient _httpClient = new HttpClient();
        private IConfiguration configuration;
        private string RouteAPI = "";
        public BankServices(IConfiguration _configuration)
        {
            this.configuration = _configuration;
            this.RouteAPI = this.configuration["RouteAPI"];
        }

        public async Task<List<MBank>> GetAllData()
        {
            List<MBank> data = new List<MBank>();

            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + "apiBank/GetAllData");
            data = JsonConvert.DeserializeObject<List<MBank>>(apiResponse)!;

            return data;
        }
        public async Task<bool> CheckNameIsExist(string name,  int id)
        {
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiBank/CheckNameIsExist/{name}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
            return isExist;
        }

        public async Task<bool> CheckCodeIsExist(string kodeva, int id)
        {
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiBank/CheckCodeIsExist/{kodeva}/{id}");
            bool isExist = JsonConvert.DeserializeObject<bool>(apiResponse);
            return isExist;
        }

        public async Task<VMResponse> Create(MBank data)
        {
            //process conver from object to string
            string json = JsonConvert.SerializeObject(data);

            //process change string to be json then send by Request Body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //process call API and sending Body Request
            var request = await _httpClient.PostAsync(RouteAPI + "apiBank/Save", content);

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
        public async Task<MBank> GetDataById(int id)
        {
            MBank data = new MBank();
            string apiResponse = await _httpClient.GetStringAsync(RouteAPI + $"apiBank/GetDataById/{id}");
            data = JsonConvert.DeserializeObject<MBank>(apiResponse)!;
            return data;
        }

        public async Task<VMResponse> Edit(MBank dataParam)
        {
            //process convert from object to string
            string json = JsonConvert.SerializeObject(dataParam);
            //process change string to be json then send by Request Body
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            //process call API and sending Body Request
            var request = await _httpClient.PostAsync(RouteAPI + "apiBank/Edit", content);

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
            var request = await _httpClient.DeleteAsync(RouteAPI + $"apiBank/Delete/{id}");

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


        public async Task<VMResponse> CheckIsExist(VMBank dataParam)
        {
            string json = JsonConvert.SerializeObject(dataParam);
            StringContent content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(RouteAPI + $"apiBank/CheckIsExist", content);


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
