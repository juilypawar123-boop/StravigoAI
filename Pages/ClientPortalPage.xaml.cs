namespace StravigoAI.Pages
{
    public partial class ClientPortalPage : ContentPage
    {
        public ClientPortalPage()
        {
            InitializeComponent();
        }
        private async void RequestMeeting_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestMeetingFormPage());
        }
        private async void AskAIQuestion_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Ask AI", "AI query functionality coming soon!", "OK");
        }
    }
}
