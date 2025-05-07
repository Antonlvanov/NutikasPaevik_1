using Microsoft.Maui.Controls;

namespace NutikasPaevik
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        public static void SwitchToMainApp()
        {
            try
            {
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