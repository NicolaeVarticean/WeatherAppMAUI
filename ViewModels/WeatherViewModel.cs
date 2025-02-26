using System.ComponentModel;
using System.Runtime.CompilerServices;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.ViewModels
{
    public partial class WeatherViewModel : INotifyPropertyChanged
    {
        private readonly IWeatherService _weatherService;
        private WeatherData _weatherData;

        public event PropertyChangedEventHandler? PropertyChanged;

        public WeatherData WeatherData
        {
            get => _weatherData;
            set
            {
                _weatherData = value;
                OnPropertyChanged();
            }
        }

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
            LoadWeatherData();
        }

        private async void LoadWeatherData()
        {
            try
            {
                WeatherData = await _weatherService.GetWeatherAsync("London");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
