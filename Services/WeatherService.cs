﻿using System.Net.Http.Json;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService(HttpClient httpClient) : IWeatherService
    {
        private const string ApiKey = "bcc67c3b72f328e1768bda6a5ccc559f";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/forecast";

        public async Task<WeatherData> GetWeatherAsync(string location)
        {
            var url = $"{BaseUrl}?q={location}&appid={ApiKey}&units=metric";
            var response = await httpClient.GetFromJsonAsync<OpenWeatherMapResponse>(url);

            if (response != null)
            {
                return new WeatherData
                {
                    Location = response.City.Name,
                    Temperature = response.List[0].Main.Temp,
                    Condition = response.List[0].Weather[0].Description,
                    Forecasts = response.List
                        .GroupBy(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).Date)
                        .Select(g => new Forecast
                        {
                            Date = g.Key,
                            Temperature = g.Average(f => f.Main.Temp),
                            Condition = g.First().Weather[0].Description
                        })
                        .Take(5)
                        .ToList()
                };
            }
            else
            {
                throw new Exception("Failed to fetch weather data.");
            }
        }
        private class OpenWeatherMapResponse
        {
            public List<ForecastItem> List { get; set; }
            public City City { get; set; }
        }

        private class ForecastItem
        {
            public long Dt { get; set; }
            public Main Main { get; set; }
            public List<Weather> Weather { get; set; }
        }

        private class Main
        {
            public double Temp { get; set; }
        }

        private class Weather
        {
            public string Description { get; set; }
        }

        private class City
        {
            public string Name { get; set; }
        }
    }
}
