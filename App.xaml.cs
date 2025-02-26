using WeatherApp.Views;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App(WeatherPage weatherPage)
        {
            InitializeComponent();

            MainPage = weatherPage;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }
    }
}