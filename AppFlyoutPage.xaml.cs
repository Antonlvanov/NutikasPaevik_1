namespace NutikasPaevik
{
    public partial class AppFlyoutPage : FlyoutPage
    {
        public AppFlyoutPage()
        {
            InitializeComponent();
            // Инициализируем меню и деталь
            Flyout = new FlyoutMenuPage();
            Detail = new NavigationPage(new HomePage());
            // Подписываемся на событие выбора
            (Flyout as FlyoutMenuPage).MenuItemSelected += OnMenuItemSelected;
        }

        private void OnMenuItemSelected(object sender, Type pageType)
        {
            if (pageType != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(pageType));
                IsPresented = false;
            }
        }
    }
}