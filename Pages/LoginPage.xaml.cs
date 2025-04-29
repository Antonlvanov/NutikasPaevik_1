using NutikasPaevik.Pages.Views;

namespace NutikasPaevik
{
    public partial class LoginPage : ContentPage
    {
        private readonly DiaryViewModel _viewModel;

        public LoginPage(DiaryViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = this; // Или другой ViewModel для логина
        }

        // Пример метода для кнопки логина
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Здесь должна быть логика аутентификации
                // Например, проверка логина и пароля
                bool isAuthenticated = true; // Замените на реальную проверку

                if (isAuthenticated)
                {
                    // Переключение на AppShell
                    App.SwitchToMainApp();
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