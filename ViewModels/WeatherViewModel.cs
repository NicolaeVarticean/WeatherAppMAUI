using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
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

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _backgroundImage;
        public string BackgroundImage
        {
            get => _backgroundImage;
            set
            {
                _backgroundImage = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }

        public WeatherViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            RefreshCommand = new Command(async () => await LoadWeatherDataAsync());
            _ = LoadWeatherDataAsync();
        }

        private async Task LoadWeatherDataAsync()
        {
            try
            {
                IsLoading = true;
                WeatherData = await _weatherService.GetWeatherAsync("Chisinau");
                BackgroundImage = GetBackgroundImage(WeatherData.Main);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private static string GetBackgroundImage(string main)
        {
            return main.ToLower() switch
            {
                "thunderstorm" => "thunderstorm.jpg",
                "drizzle" => "drizzle.jpg",
                "rain" => "rainy.jpg",
                "snow" => "snowy.jpg",
                "clear" => "clear_sky.jpg",
                "clouds" => "cloudy.jpg",
                "mist" or "smoke" or "haze" or "dust" or "fog" => "misty.jpg",
                "sand" or "ash" => "dusty.jpg",
                "squall" or "tornado" => "stormy.jpg",
                _ => "default_background.jpg"
            };
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
