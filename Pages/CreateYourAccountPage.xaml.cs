using Microsoft.Maui.Controls;

namespace StravigoAI.Pages
{
    public partial class CreateYourAccountPage : ContentPage
    {
        public CreateYourAccountPage()
        {
            InitializeComponent();
        }

        private async void OnSignInClicked(object sender, EventArgs e)
        {
            string name = NameEntry?.Text?.Trim() ?? string.Empty;
            string email = EmailEntry?.Text?.Trim() ?? string.Empty;
            string password = PasswordEntry?.Text?.Trim() ?? string.Empty;
            string confirmPassword = ConfirmPasswordEntry?.Text?.Trim() ?? string.Empty;

            // Validate input
            if (string.IsNullOrEmpty(name) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword))
            {
                await DisplayAlert("Error", "Please fill all fields.", "OK");
                return;
            }

            // Password match check
            if (password != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Successful navigation
            await Navigation.PushAsync(new MainDashboardPage());
        }

        private bool _isPasswordHidden = true;
        private bool _isConfirmPasswordHidden = true;

        private void OnTogglePasswordClicked(object sender, EventArgs e)
        {
            _isPasswordHidden = !_isPasswordHidden;
            PasswordEntry.IsPassword = _isPasswordHidden;
            TogglePasswordButton.Source = _isPasswordHidden ? "eye_closed.png" : "eye_open.png";
        }
    }
}
