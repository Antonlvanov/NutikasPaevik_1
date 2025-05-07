using Microsoft.Maui.Controls;

namespace NutikasPaevik
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Имитация логина
            await Task.Delay(500); // Для демонстрации
            App.SwitchToMainApp();
        }
    }
}