using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NutikasPaevik.Views;
// ViewModel
public class DiaryViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand AddNoteCommand { get; }
    public DateTime CurrentDate => DateTime.Today;

    public DiaryViewModel()
    {
        Notes = new ObservableCollection<Note>();
        AddNoteCommand = new Command(async () => await ShowNotePopup());
    }

    private async Task ShowNotePopup()
    {
        var popup = new NotePopup();
        var result = await Application.Current.MainPage.ShowPopupAsync(popup);
        if (result is Note note)
        {
            Notes.Add(note);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class HalfWidthConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        if (value is double width)
        {
            return width / 2 - 20;
        }
        return 0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class Note
{
    public DateTime CreationTime { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public double RotationAngle { get; set; }
    public string StickerImage { get; set; }
}