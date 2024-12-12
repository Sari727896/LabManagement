using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Implementations
{
    public class ICountClientService : IICountService
    {
        private readonly HttpClient _httpClient;
        private readonly ICountApiSettings _countApiSettings;
        //Note
        //לא מספיק בטיחותי ,צריך להוציא למשתני סביבה לא מומש מחוסר זמן
        private readonly string cid = "kenionLTD";
        private readonly string user = "gvia";
        private readonly string pass = "gvia";
        public ICountClientService(HttpClient httpClient, IOptions<ICountApiSettings> countApiSettings)
        {
            _httpClient = httpClient;

        }


        public async Task<int> GetClientId(string phone = null, string email = null)
        {
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("At least one of the parameters: phone, or email must be provided.");
            }

            var fullRequest = new
            {
                cid,
                user,
                pass,
                phone = string.IsNullOrEmpty(phone) ? null : phone,
                email = string.IsNullOrEmpty(email) ? null : email
            };

            var json = JsonConvert.SerializeObject(fullRequest, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //Note
            //לא מספיק בטיחותי ,צריך להוציא למשתני סביבה לא מומש מחוסר זמן
            HttpResponseMessage response = await _httpClient.PostAsync("https://api.icount.co.il/api/v3.php/client/info", content);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Request failed: {errorMessage}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ClientInfoResponse>(responseContent);
            if (result == null || !result.Status)
            {
                return 0;
            }

            return result.ClientId;
        }

    }

}




