namespace NutikasPaevik;

public partial class AppShell : Shell
{
    public Command NavigateToSettingsCommand { get; }

    public AppShell()
    {
        try
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(DiaryPage), typeof(DiaryPage));
            Routing.RegisterRoute(nameof(PlannerPage), typeof(PlannerPage));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(Account), typeof(Account));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"AppShell Error: {ex.Message}");
            throw; // Для отладки, потом можно убрать
        }
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await GoToAsync(nameof(Account));  // Используем относительный маршрут
        FlyoutIsPresented = false;
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await GoToAsync(nameof(Settings));  // Используем относительный маршрут
        FlyoutIsPresented = false;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Kinnitus", "Kas soovid välja logida?", "Jah", "Ei");
        if (confirm)
        {
            await GoToAsync(nameof(LoginPage));  // Используем относительный маршрут
            FlyoutIsPresented = false;
        }
    }
}
