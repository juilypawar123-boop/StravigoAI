using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using StravigoAI.Models;
using StravigoAI.Data;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class TeamMemberFormPage : ContentPage
    {
        public TeamMemberFormPage()
        {
            InitializeComponent();
            LoadPickers();
        }

        private void LoadPickers()
        {
            // Sample roles
            RolePicker.ItemsSource = new string[] { "Manager", "Consultant", "Developer" };

            // Sample projects
            ProjectPicker.ItemsSource = ProjectDataStore.Projects.Select(p => p.ProjectName).ToList();
        }

        private async void AddMember_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(EmailEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneEntry.Text) ||
                RolePicker.SelectedItem == null ||
                ProjectPicker.SelectedItem == null)
            {
                await DisplayAlert("Validation Error", "Please fill all fields", "OK");
                return;
            }

            var newMember = new TeamMember
            {
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneEntry.Text,
                Role = RolePicker.SelectedItem.ToString(),
                AssignedProjects = new string[] { ProjectPicker.SelectedItem.ToString() }.ToList()
            };

            // Add to data store
            TeamMemberDataStore.Members.Add(newMember);

            // Clear form
            NameEntry.Text = "";
            EmailEntry.Text = "";
            PhoneEntry.Text = "";
            RolePicker.SelectedItem = null;
            ProjectPicker.SelectedItem = null;

            // Navigate back to TeamMembersPage and refresh
            await Navigation.PopAsync();
        }
    }
}
