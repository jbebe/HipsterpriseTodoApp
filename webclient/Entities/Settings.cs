using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoClient.Data
{
    public class Settings
    {
        [JsonPropertyName("api_url")]
        public string ApiUrl { get; set; }

        public async Task InitializeAsync(HttpClient http)
        {
            var settings = await http.GetFromJsonAsync<Settings>("settings.json");
            ApiUrl = settings.ApiUrl;
        }
    }
}
