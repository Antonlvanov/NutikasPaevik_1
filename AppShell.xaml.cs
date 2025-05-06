using Microsoft.Maui.Controls;
using NutikasPaevik.Pages;
using System.Diagnostics;

namespace NutikasPaevik
{
    public partial class AppShell : Shell
    {
        public AppShell(DiaryViewModel viewModel)
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(DiaryPage), typeof(DiaryPage));
            Routing.RegisterRoute(nameof(PlannerPage), typeof(PlannerPage));
            Routing.RegisterRoute(nameof(Settings), typeof(Settings));
            Routing.RegisterRoute(nameof(Account), typeof(Account));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            // Установка DiaryPage с viewModel
            var diarySection = (ShellSection)Items[1].Items[0];
            diarySection.Items[0].Content = new DiaryPage(viewModel);
            Debug.WriteLine($"AppShell: DiaryPage created with viewModel: {viewModel?.GetType().Name}");
        }

        private async void OnProfileClicked(object sender, EventArgs e)
        {
            await GoToAsync(nameof(Account));
            FlyoutIsPresented = false;
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await GoToAsync(nameof(Settings));
            FlyoutIsPresented = false;
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Kinnitus", "Kas soovid välja logida?", "Jah", "Ei");
            if (confirm)
            {
                await GoToAsync(nameof(LoginPage));
                FlyoutIsPresented = false;
            }
        }
    }
}