namespace StravigoAI.Pages
{
    public partial class MeetingNotesPage : ContentPage
    {
        bool isListening = false;

        public MeetingNotesPage()
        {
            InitializeComponent();
        }

        private async void NewNote_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WriteNotesPage());
        }

        private async void UploadNotes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UploadFilesPage());
        }

        private async void AIListen_Clicked(object sender, EventArgs e)
        {
            if (!isListening)
            {
                isListening = true;
                AIListenBtn.Text = "Stop AI Listening";
                await DisplayAlert("AI Listening Started", "AI is now capturing the meeting audio...", "OK");
            }
            else
            {
                isListening = false;
                AIListenBtn.Text = "Start AI Listening";
                await DisplayAlert("AI Listening Stopped", "AI has generated the meeting notes & resolutions.", "OK");

                // Add AI-generated note dynamically
                NotesList.Children.Add(new Frame
                {
                    BackgroundColor = Colors.Green,
                    Padding = 10,
                    WidthRequest = 150, 
                    Content = new Label
                    {
                        Text = "AI Note: Client requested changes to Q4 project plan. Action items assigned.",
                        TextColor = Colors.Black,
                        LineBreakMode = LineBreakMode.WordWrap
                    }
                });
            }
        }

    }

}