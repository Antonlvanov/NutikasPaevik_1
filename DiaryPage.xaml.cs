using CommunityToolkit.Maui.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using NutikasPaevik.Views;

namespace NutikasPaevik
{
    public partial class DiaryPage : ContentPage
    {
        public DiaryPage()
        {
            InitializeComponent();
            BindingContext = new DiaryViewModel();
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
            var titleLabel = new Label { Text = "Новая заметка", FontSize = 20, FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center };
            _titleEntry = new Entry { Placeholder = "Название заметки", Margin = new Thickness(0, 10, 0, 0) };
            _contentEntry = new Editor { Placeholder = "Содержимое заметки", HeightRequest = 200, Margin = new Thickness(0, 10, 0, 0) };
            var saveButton = new Button { Text = "Сохранить", BackgroundColor = Color.FromArgb("#4CAF50"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 10, 0, 0) };
            saveButton.Clicked += OnSaveClicked;
            var cancelButton = new Button { Text = "Отмена", BackgroundColor = Color.FromArgb("#FF4444"), TextColor = Colors.White, CornerRadius = 5, Margin = new Thickness(0, 5, 0, 0) };
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

                double rotationAngle = random.NextDouble() * 10 - 5; // от -5 до 5

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
                Application.Current?.MainPage?.DisplayAlert("Ошибка", "Заполните все поля", "ОК");
            }
        }
    }
}