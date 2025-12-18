using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System;
using System.Collections.ObjectModel;

namespace StravigoAI.Pages
{
    public partial class UploadFilesPage : ContentPage
    {
        // ObservableCollection to store uploaded files
        public ObservableCollection<UploadFile> FilesList { get; set; } = new ObservableCollection<UploadFile>();

        public UploadFilesPage()
        {
            InitializeComponent();

            // Bind the CollectionView to the FilesList
            FilesCollectionView.ItemsSource = FilesList;
        }

        private async void OnUploadFileClicked(object sender, EventArgs e)
        {
            // Validate project name
            if (string.IsNullOrWhiteSpace(ProjectEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a project name.", "OK");
                return;
            }

            // Pick a file
            var file = await FilePicker.PickAsync();

            if (file == null)
            {
                SelectedFileLabel.Text = "No file selected";
                return;
            }

            // Update label with selected file
            SelectedFileLabel.Text = $"Selected: {file.FileName}";

            // Add the uploaded file to the ObservableCollection
            FilesList.Add(new UploadFile
            {
                Project = ProjectEntry.Text,
                FileName = file.FileName
            });

            // Show confirmation
            await DisplayAlert("Success!", "Your file has been selected and saved on this page!", "OK");

            // Clear project entry
            ProjectEntry.Text = string.Empty;
        }
    }

    // Model class for uploaded files
    public class UploadFile
    {
        public string Project { get; set; }
        public string FileName { get; set; }
    }
}
