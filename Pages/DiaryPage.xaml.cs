using CommunityToolkit.Maui.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using NutikasPaevik.Pages.Views;
using NutikasPaevik.Database;

namespace NutikasPaevik
{
    public partial class DiaryPage : ContentPage
    {
        public DiaryPage() : this(DependencyService.Get<DiaryViewModel>())
        {
        }

        public DiaryPage(DiaryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }


    public class NotePopup : Popup
    {
        private Entry _titleEntry;
        private Editor _contentEntry;

        private static readonly string[] StickerImages = new[]
        {
            "rednote.png",
            "bluenote.png",
            "greennote.png",
            "yellownote.png"
        };

        public NotePopup()
        {
            Size = new Size(300, 400);
            Color = Color.FromArgb("#FFFFFF");

            var stackLayout = new StackLayout { Padding = 20, Spacing = 10 };
            var titleLabel = new Label { Text = "Ķīāą˙ ēąģåņźą", FontSize = 20, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
            _titleEntry = new Entry { Placeholder = "Ķąēāąķčå ēąģåņźč", Margin = new Thickness(0, 10, 0, 0) };
            _contentEntry = new Editor { Placeholder = "Ńīäåšęčģīå ēąģåņźč", HeightRequest = 200, Margin = new Thickness(0, 10, 0, 0) };
            var saveButton = new Button { Text = "Ńīõšąķčņü", BackgroundColor = Color.FromArgb("#4CAF50"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 10, 0, 0) };
            saveButton.Clicked += OnSaveClicked;
            var cancelButton = new Button { Text = "Īņģåķą", BackgroundColor = Color.FromArgb("#FF4444"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 5, 0, 0) };
            cancelButton.Clicked += (s, e) => Close();
            stackLayout.Children.Add(titleLabel);
            stackLayout.Children.Add(_titleEntry);
            stackLayout.Children.Add(_contentEntry);
            stackLayout.Children.Add(saveButton);
            stackLayout.Children.Add(cancelButton);
            Content = stackLayout;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_titleEntry.Text) && !string.IsNullOrWhiteSpace(_contentEntry.Text))
            {
                var random = new Random();
                double rotationAngle = random.NextDouble() * 10 - 5; // от -5 до 5 градусов
                string stickerImage = StickerImages[random.Next(StickerImages.Length)];

                var note = new Note
                {
                    CreationTime = DateTime.Now,
                    Title = _titleEntry.Text,
                    Content = _contentEntry.Text,
                    RotationAngle = rotationAngle,
                    StickerImage = stickerImage
                };
                Close(note);
            }
            else
            {
                Application.Current?.MainPage?.DisplayAlert("Viga", "Täitke kõik väljad", "OK");
            }
        }
    }

    public class HalfWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double width)
            {
                return width / 2 - 20; // Учитываем отступы
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}