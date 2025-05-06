using NutikasPaevik.Pages;

namespace NutikasPaevik
{
    public partial class App : Application
    {
        private readonly DiaryViewModel _viewModel;
        public App(DiaryViewModel viewModel)
        {
            InitializeComponent();
            // Стартовая страница - LoginPage
            MainPage = new NavigationPage(new LoginPage(viewModel));
            _viewModel = viewModel;
        }

        public static void SwitchToMainApp(DiaryViewModel viewModel)
        {
            try
            {
                // Переключение на AppShell после логина
                Current.MainPage = new AppShell(viewModel);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SwitchToMainApp Error: {ex.Message}");
                Current.MainPage.DisplayAlert("Viga", $"Põhirakenduse laadimine ebaõnnestus: {ex.Message}", "OK");
            }
        }
    }
}