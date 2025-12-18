using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace StravigoAI.Pages
{
    public partial class MainDashboardPage : ContentPage
    {
        public MainDashboardPage()
        {
            InitializeComponent();
        }

        // Sidebar button handlers
        private async void Dashboard_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new MainDashboardPage());

        private async void Projects_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new ProjectsPage());

        private async void BillingAndTimesheets_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new BillingAndTimesheetsPage());

        private async void GlobalAIInsights_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new GlobalAIInsightsPage());

        private async void Templates_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new TemplatesPage());

        private async void MeetingNotes_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new MeetingNotesPage());

        private async void ClientPortal_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new ClientPortalPage());

        private async void MeetingsCalendar_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new MeetingsCalenderPage());

        private async void Teams_Clicked(Object sender, EventArgs e) =>
            await Navigation.PushAsync(new TeamMembersPage());

        private async void UploadData_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new UploadFilesPage());

        private async void GenerateReport_Clicked(object sender, EventArgs e) =>
            await DisplayAlert("Report", "Generate Report clicked", "OK");

        private async void AddProject_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new ProjectsPage());

        private async void Notifications_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new NotificationsPage());
        private async void LogOut_Clicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new LogOutPage());
    }
}
