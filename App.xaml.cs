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
            Current.MainPage = new AppFlyoutPage();
        }
    }
}