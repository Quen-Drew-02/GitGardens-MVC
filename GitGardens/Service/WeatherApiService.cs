using GitGardens.Interface;
using GitGardens.Models;
using System.Text.Json;

/*
     Title: Disclosure of AI Usage in my Assessment.
     • Section: WeatherApi Service & WeatherController.
     • AI Tool: Gemini
     • Purpose/intention : Design of a Weather Service that fetches weather data from an external API and integrates it into the application.
     • Date(s) 05/05/2026.
     • https://chatgpt.com/share/69fb03d0-798c-83ea-8f7b-9ec21b3d39b6
     */

namespace GitGardens.Services
{
    public class WeatherApiService : IWeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        public WeatherApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            // Accessing the nested JSON values
            _apiKey = configuration["ExternalApis:WeatherService:ApiKey"];
            _baseUrl = configuration["ExternalApis:WeatherService:BaseUrl"];
        }

        public async Task<ExternalWeatherResponse?> GetWeatherAsync(string city)
        {
            var url = $"{_baseUrl}weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)// Check if the response is successful
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ExternalWeatherResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null;
        }
    }
}