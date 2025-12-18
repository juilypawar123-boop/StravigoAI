using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace StravigoAI.Pages
{
    public partial class WriteNotesPage : ContentPage
    {
        public ObservableCollection<MeetingNote> NotesList { get; set; } = new ObservableCollection<MeetingNote>();
        public WriteNotesPage()
        {
            InitializeComponent();
            NotesCollectionView.ItemsSource = NotesList;
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProjectEntry.Text))
            {
                DisplayAlert("Error", "Please enter a project name.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(NotesEditor.Text))
            {
                DisplayAlert("Error", "Please enter meeting notes.", "OK");
                return;
            }

            var note = new MeetingNote
            {
                Project = ProjectEntry.Text,
                Notes = NotesEditor.Text
            };

            NotesList.Add(note);

            ProjectEntry.Text = string.Empty;
            NotesEditor.Text = string.Empty;
        }
    }

    public class MeetingNote
    {
        public string Project { get; set; }
        public string Notes { get; set; }
    }

}
