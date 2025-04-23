namespace NutikasPaevik;

public partial class FlyoutMenuPage : ContentPage
{
    public event EventHandler<Type> MenuItemSelected;

    public FlyoutMenuPage()
    {
        InitializeComponent();
        collectionView.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FlyoutPageItem item)
        {
            MenuItemSelected?.Invoke(this, item.TargetType);
            collectionView.SelectedItem = null; // Сбрасываем выбор
        }
    }
}