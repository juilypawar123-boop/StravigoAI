using Microsoft.Maui.Controls;

namespace StravigoAI.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text ?? string.Empty;

            if (email == "admin@example.com" && password == "1234")
            {
                await Navigation.PushAsync(new MainDashboardPage());
            }
            else
            {
                await DisplayAlert("Login Failed",
                    "Incorrect email or password. Try again.", "OK");
            }
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateYourAccountPage());
        }

        private bool _isPasswordHidden = true;

        private void OnTogglePasswordClicked(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden;
            PasswordEntry.IsPassword = _isPasswordHidden;
            TogglePasswordButton.Source =
                _isPasswordHidden ? "eye_closed.png" : "eye_open.png";
        }
    }
}
