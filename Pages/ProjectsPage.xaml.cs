using Microsoft.Maui.Controls;
using StravigoAI.Data;
using System;

namespace StravigoAI.Pages
{
    public partial class ProjectsPage : ContentPage
    {
        public ProjectsPage()
        {
            InitializeComponent();
            LoadProjects();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadProjects();
        }

        private void LoadProjects()
        {
            ProjectsList.Children.Clear();

            foreach (var project in ProjectDataStore.Projects)
            {
                ProjectsList.Children.Add(new Frame
                {
                    BackgroundColor = Colors.LightGray,
                    CornerRadius = 12,
                    Padding = 15,
                    Content = new VerticalStackLayout
                    {
                        Children =
                        {
                            new Label { Text = project.ProjectName, FontAttributes = FontAttributes.Bold, FontSize = 18 },
                            new Label { Text = project.Objectives, FontSize = 14 },
                            new Label { Text = $"Goal: {project.Goals}", FontSize = 14 },
                            new Label { Text = $"Challenges: {project.Challenges}", FontSize = 14 }
                        }
                    }
                });
            }

            ActiveProjectsLabel.Text = ProjectDataStore.Projects.Count.ToString();
        }

        private async void AddProject_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProjectRegistrationPage());
        }
    }
}
