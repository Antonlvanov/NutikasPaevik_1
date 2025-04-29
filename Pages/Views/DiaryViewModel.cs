using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore;
using NutikasPaevik.Database;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace NutikasPaevik.Pages.Views;
// ViewModel
public class DiaryViewModel : INotifyPropertyChanged
{
    private readonly AppDbContext _dbContext;
    public ObservableCollection<Note> Notes { get; set; }
    public ICommand AddNoteCommand { get; }
    public ICommand DeleteNoteCommand { get; }
    
    public DateTime CurrentDate => DateTime.Today;

    public DiaryViewModel(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Notes = new ObservableCollection<Note>();
        AddNoteCommand = new Command(async () => await ShowNotePopup());
        DeleteNoteCommand = new Command<Note>(async (note) => await DeleteNote(note));
        LoadNotes();
    }

    private void LoadNotes()
    {
        var notes = _dbContext.Notes.ToList();
        foreach (var note in notes)
        {
            Notes.Add(note);
        }
    }

    private async Task DeleteNote(Note note)
    {
        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync();
        Notes.Remove(note);
    }

    private async Task ShowNotePopup()
    {
        try
        {
            var popup = new NotePopup();
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);
            if (result is Note note)
            {
                _dbContext.Notes.Add(note);
                await _dbContext.SaveChangesAsync();
                Notes.Add(note);
            }
        }
        catch (Exception ex)
        {   
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось сохранить заметку: {ex.Message}", "OK");
        }
    }

    // Событие для уведомления UI
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


}
