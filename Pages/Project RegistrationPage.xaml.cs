using Microsoft.Maui.Controls;
using System;
using StravigoAI.Data;
using StravigoAI.Models;

namespace StravigoAI.Pages
{
    public partial class ProjectRegistrationPage : ContentPage
    {
        public ProjectRegistrationPage()
        {
            InitializeComponent();
        }

        private async void OnSaveProject_Clicked(object sender, EventArgs e)
        {
            var project = new ProjectModel
            {
                ProjectName = ProjectNameEntry.Text,
                Objectives = ProjectObjectives.Text,
                Goals = ProjectGoal.Text,
                Challenges = Challenges.Text
            };

            ProjectDataStore.Projects.Add(project);

            // Clear input fields
            ProjectNameEntry.Text = string.Empty;
            ProjectObjectives.Text = string.Empty;
            ProjectGoal.Text = string.Empty;
            Challenges.Text = string.Empty;

            await DisplayAlert("Success", "Project has been added successfully!", "OK");

            await Navigation.PopAsync(); 
        }
    }
}