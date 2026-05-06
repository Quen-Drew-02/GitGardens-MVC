using GitGardens.Models;

namespace GitGardens.Interface
{
    public interface IWeatherApiService
    {
        Task<ExternalWeatherResponse?> GetWeatherAsync(string city);
    }
}