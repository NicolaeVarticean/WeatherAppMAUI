namespace WeatherApp.Models
{
    public class WeatherData
    {
        public string Location { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
        public List<Forecast> Forecasts { get; set; }
        public List<HourlyForecast> HourlyForecasts { get; set; }
    }
}
