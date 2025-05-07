using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using NutikasPaevik.Database;
using System.Diagnostics;

namespace NutikasPaevik
{
    public partial class DiaryPage : ContentPage
    {
        private readonly DiaryViewModel _viewModel;

        public DiaryPage()
        {
            InitializeComponent();
            _viewModel = MauiProgram.CreateMauiApp().Services.GetService<DiaryViewModel>();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext != _viewModel)
            {
                BindingContext = _viewModel;
            }
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await _viewModel.ShowNotePopup();
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
            var titleLabel = new Label { Text = "Uus märge", FontSize = 20, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
            _titleEntry = new Entry { Placeholder = "Märkmiku pealkiri", Margin = new Thickness(0, 10, 0, 0) };
            _contentEntry = new Editor { Placeholder = "Märkmiku sisu", HeightRequest = 200, Margin = new Thickness(0, 10, 0, 0) };
            var saveButton = new Button { Text = "Salvesta", BackgroundColor = Color.FromArgb("#4CAF50"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 10, 0, 0) };
            saveButton.Clicked += OnSaveClicked;
            var cancelButton = new Button { Text = "Tühista", BackgroundColor = Color.FromArgb("#FF4444"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 5, 0, 0) };
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
                double rotationAngle = random.NextDouble() * 10 - 5;
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
                return width / 2 - 20;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}