using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore;
using NutikasPaevik.Database;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace NutikasPaevik
{
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

        public void LoadNotes()
        {
            try
            {
                var notes = _dbContext.Notes.ToList();
                Debug.WriteLine($"Loaded {notes.Count} notes from database.");
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadNotes Error: {ex.Message}");
                Application.Current?.MainPage?.DisplayAlert("Viga", $"Ei saanud märkmeid laadida: {ex.Message}", "OK");
            }
        }

        public async Task DeleteNote(Note note)
        {
            try
            {
                _dbContext.Notes.Remove(note);
                await _dbContext.SaveChangesAsync();
                Notes.Remove(note);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DeleteNote Error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Viga", $"Ei saanud märkmeid kustutada: {ex.Message}", "OK");
            }
        }

        public async Task ShowNotePopup()
        {
            try
            {
                Debug.WriteLine("Opening NotePopup...");
                var popup = new NotePopup();
                var result = await Application.Current.MainPage.ShowPopupAsync(popup);
                if (result is Note note)
                {
                    Debug.WriteLine($"Saving note: {note.Title}");
                    _dbContext.Notes.Add(note);
                    await _dbContext.SaveChangesAsync();
                    Notes.Add(note);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ShowNotePopup Error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Viga", $"Ei saanud märkmeid salvestada: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}