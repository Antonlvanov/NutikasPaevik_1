using Microsoft.Maui.Controls;
using NutikasPaevik.Pages;

namespace NutikasPaevik
{
    public partial class LoginPage : ContentPage
    {
        private readonly DiaryViewModel _viewModel;

        public LoginPage(DiaryViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = this;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Симуляция аутентификации
                bool isAuthenticated = true; // Замените на реальную проверку
                if (isAuthenticated)
                {
                    App.SwitchToMainApp(_viewModel);
                }
                else
                {
                    await DisplayAlert("Viga", "Vale kasutajanimi või parool", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Sisselogimine ebaõnnestus: {ex.Message}", "OK");
            }
        }
    }
}