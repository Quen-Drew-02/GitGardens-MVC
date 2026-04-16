namespace GitGardens.Models
{
        public class ExternalWeatherResponse
        {
            public MainData Main { get; set; }
            public string Name { get; set; }
        }

        public class MainData
        {
            public decimal Temp { get; set; }
            public int Humidity { get; set; }

        }
    
}
