using NutikasPaevik.Pages.Views;

namespace NutikasPaevik
{
    public partial class App : Application
    {
        public App(DiaryViewModel viewModel)
        {
            InitializeComponent();
            // Стартовая страница - LoginPage
            MainPage = new NavigationPage(new LoginPage(viewModel));
        }

        public static void SwitchToMainApp()
        {
            try
            {
                // Переключение на AppShell после логина
                Current.MainPage = new AppShell();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SwitchToMainApp Error: {ex.Message}");
                Current.MainPage.DisplayAlert("Viga", $"Põhirakenduse laadimine ebaõnnestus: {ex.Message}", "OK");
            }
        }
    }
}