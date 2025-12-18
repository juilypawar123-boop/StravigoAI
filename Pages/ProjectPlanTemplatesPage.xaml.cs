using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace StravigoAI.Pages
{
    public partial class ProjectPlanTemplatesPage : ContentPage
    {
        public ProjectPlanTemplatesPage()
        {
            InitializeComponent();
        }

        private async void OpenTemplate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PdfViewerPage("ProjectScope.pdf"));
        }

        private async void DownloadTemplate_Clicked(object sender, EventArgs e)
        {
            await SavePdfToLocal("ProjectScope.pdf");
            await DisplayAlert("Download Complete", "Template saved to your device.", "OK");
        }

        private async Task SavePdfToLocal(string fileName)
        {
            var assembly = typeof(App).Assembly;
            var resourcePath = $"StravigoAI.Resources.Raw.{fileName}";

            using Stream inputStream = assembly.GetManifestResourceStream(resourcePath);
            if (inputStream == null)
            {
                await DisplayAlert("Error", $"PDF {fileName} not found.", "OK");
                return;
            }

            string downloadsPath = FileSystem.AppDataDirectory;
            string filePath = Path.Combine(downloadsPath, fileName);

            using FileStream outputStream = File.OpenWrite(filePath);
            await inputStream.CopyToAsync(outputStream);
        }
    }
}
