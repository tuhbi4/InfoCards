using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InfoCards.PL.APIClient
{
    public class ApiClient
    {
        private readonly HttpClient _client;

        public ApiClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44308/");
        }

        public async Task<List<InfoCardModel>> GetAllInfoCardsAsync()
        {
            HttpResponseMessage response = await _client.GetAsync("InfoCard");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<InfoCardModel>>(responseBody);
        }

        public async Task<InfoCardModel> GetInfoCardByIdAsync(int id)
        {
            HttpResponseMessage response = await _client.GetAsync("InfoCard/" + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InfoCardModel>(responseBody);
        }

        public async Task<InfoCardModel> CreateInfoCardAsync(InfoCardModel infoCardModel)
        {
            var json = JsonConvert.SerializeObject(infoCardModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("InfoCard", data);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InfoCardModel>(responseBody);
        }

        public async Task<InfoCardModel> UpdateInfoCardByIdAsync(int id, InfoCardModel infoCardModel)
        {
            var json = JsonConvert.SerializeObject(infoCardModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("InfoCard/" + id, data);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InfoCardModel>(responseBody);
        }

        public async Task<InfoCardModel> DeleteInfoCardByIdAsync(int id)
        {
            var response = await _client.DeleteAsync("InfoCard/" + id);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InfoCardModel>(responseBody);
        }
    }
}