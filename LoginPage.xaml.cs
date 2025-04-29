namespace NutikasPaevik
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            // После успешной авторизации
            App.SwitchToMainApp();
        }

    }
}