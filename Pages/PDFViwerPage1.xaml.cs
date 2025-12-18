using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace StravigoAI.Pages
{
    public partial class PdfViewerPage : ContentPage
    {
        public PdfViewerPage(string pdfFileName)
        {
            InitializeComponent();
            _ = LoadPdfAsync(pdfFileName);
        }

        private async Task LoadPdfAsync(string pdfFileName)
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(pdfFileName);
                if (stream == null)
                {
                    await DisplayAlert("Error", $"PDF '{pdfFileName}' not found.", "OK");
                    return;
                }

                string tempPath = Path.Combine(FileSystem.AppDataDirectory, pdfFileName);

                using (var outStream = File.OpenWrite(tempPath))
                    await stream.CopyToAsync(outStream);

                // Load the PDF from the local file into WebView
                PdfWebView.Source = DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst
                    ? $"file:///{tempPath}"   // Windows/macOS
                    : tempPath;               // Android/iOS
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to load PDF: {ex.Message}", "OK");
            }
        }
    }
}
