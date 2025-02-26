using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage(WeatherViewModel weatherViewModel)
	{
		InitializeComponent();
		BindingContext = weatherViewModel;
    }
}