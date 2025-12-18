using Microsoft.Maui.Controls;
using StravigoAI.Pages;

namespace StravigoAI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
           
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
