using WeatherApp.ViewModels;

namespace WeatherApp.Views;

public partial class WeatherPage : ContentPage
{
	public WeatherPage(WeatherViewModel weatherViewModel)
	{
		InitializeComponent();
		BindingContext = weatherViewModel;

        weatherViewModel.PropertyChanged += async (sender, e) =>
        {
            if (e.PropertyName == nameof(WeatherViewModel.IsLoading))
            {
                var refreshIcon = this.FindByName<ImageButton>("RefreshIcon");
                if (refreshIcon != null)
                {
                    if (weatherViewModel.IsLoading)
                    {
                        await refreshIcon.RotateTo(360, 1000, Easing.Linear);
                        refreshIcon.Rotation = 0;
                    }
                }
            }
        };
    }
}