namespace WeatherApp.Models
{
    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public string Condition { get; set; }
    }
}
