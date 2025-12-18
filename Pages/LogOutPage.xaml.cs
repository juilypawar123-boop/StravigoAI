using Microsoft.Maui.Controls;

namespace StravigoAI.Pages
{
    public partial class LogOutPage : ContentPage
    {
        public LogOutPage()
        {
            InitializeComponent();
        }

        // Handle the log out action
        private async void LogoutConfirmed_Clicked(object sender, EventArgs e)
        {
            // TODO: Clear user session or saved credentials here if needed

            // Navigate back to login page or main root page
            await Navigation.PopToRootAsync();
        }

        // Cancel and go back to the previous page
        private async void CancelLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
