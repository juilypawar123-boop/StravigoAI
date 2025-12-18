using System;
using Microsoft.Maui.Controls;
using StravigoAI.Services;

namespace StravigoAI.Pages
{
    public partial class RequestMeetingFormPage : ContentPage
    {
        public RequestMeetingFormPage()
        {
            InitializeComponent();
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(SubjectEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a meeting subject.", "OK");
                return;
            }

            if (DurationPicker.SelectedIndex == -1 || TypePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please select duration and type.", "OK");
                return;
            }

            // Create request
            var meetingRequest = new MeetingRequest
            {
                Subject = SubjectEntry.Text,
                Date = MeetingDatePicker.Date,
                Time = MeetingTimePicker.Time,
                Duration = DurationPicker.SelectedItem.ToString(),
                Type = TypePicker.SelectedItem.ToString(),
                Notes = NotesEditor.Text
            };

            // Show pop-up confirmation
            await DisplayAlert("Request Sent", "Your meeting request has been submitted successfully!", "OK");

            // Add to NotificationService
            await NotificationService.Instance.AddNotification(meetingRequest);

            // Clear form
            SubjectEntry.Text = string.Empty;
            NotesEditor.Text = string.Empty;
            DurationPicker.SelectedIndex = -1;
            TypePicker.SelectedIndex = -1;
            MeetingDatePicker.Date = DateTime.Today;
            MeetingTimePicker.Time = new TimeSpan(9, 0, 0);
        }
    }
}
