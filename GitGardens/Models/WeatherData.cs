namespace GitGardens.Models
{
    public class ExternalWeatherResponse
    {
        public string Name { get; set; }
        public MainData Main { get; set; }
        public List<WeatherDescription> Weather { get; set; } 
        public WindData Wind { get; set; }                   
    }

    public class MainData
    {
        public decimal Temp { get; set; }
        public int Humidity { get; set; }
        public decimal FeelsLike { get; set; }               
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class WindData
    {
        public decimal Speed { get; set; }
    }
}