using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using StravigoAI.Models;
using StravigoAI.Data;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class TeamMembersPage : ContentPage
    {
        public TeamMembersPage()
        {
            InitializeComponent();
            LoadSampleData();
            PopulateTeamMembers();
        }

        private void LoadSampleData()
        {
            // Sample projects
            ProjectPicker.ItemsSource = ProjectDataStore.Projects.Select(p => p.ProjectName).ToList();

            // Sample roles
            RolePicker.ItemsSource = new string[] { "Manager", "Consultant", "Developer" };
        }

        private void PopulateTeamMembers()
        {
            TeamMembersList.Children.Clear();

            var members = TeamMemberDataStore.Members; // Load from your data store

            TotalMembersLabel.Text = members.Count.ToString();
            TotalProjectsLabel.Text = members.SelectMany(m => m.AssignedProjects).Distinct().Count().ToString();

            foreach (var member in members)
            {
                var frame = new Frame
                {
                    BackgroundColor = Colors.White,
                    CornerRadius = 15,
                    Padding = 15,
                    HasShadow = true,
                    Content = new VerticalStackLayout
                    {
                        Spacing = 8,
                        Children =
                        {
                            new Label { Text = member.Name, FontSize=18, FontAttributes=FontAttributes.Bold, TextColor=Colors.Black },
                            new Label { Text = $"Role: {member.Role}", FontSize=14, TextColor=Colors.Gray },
                            new Label { Text = $"Email: {member.Email}", FontSize=14, TextColor=Colors.Gray },
                            new Label { Text = $"Phone: {member.Phone}", FontSize=14, TextColor=Colors.Gray },
                            new Label { Text = $"Assigned Projects: {string.Join(", ", member.AssignedProjects)}", FontSize=14, TextColor=Colors.Gray },
                            new HorizontalStackLayout
                            {
                                Spacing=10,
                                Children =
                                {
                                    new Button
                                    {
                                        Text = "View Details",
                                        BackgroundColor = Colors.LightBlue,
                                        TextColor = Colors.White,
                                        CornerRadius = 12,
                                        Command = new Command(async () => await DisplayAlert("Team Member Details", $"Name: {member.Name}\nRole: {member.Role}\nProjects: {string.Join(", ", member.AssignedProjects)}", "OK"))
                                    },
                                    new Button
                                    {
                                        Text = "Edit",
                                        BackgroundColor = Colors.Orange,
                                        TextColor = Colors.White,
                                        CornerRadius = 12
                                        // Add edit command
                                    }
                                }
                            }
                        }
                    }
                };

                TeamMembersList.Children.Add(frame);
            }
        }

        private async void AddTeamMember_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamMemberFormPage());
            // Refresh after returning
            this.Appearing += TeamMembersPage_Appearing;
        }

        private void TeamMembersPage_Appearing(object sender, EventArgs e)
        {
            PopulateTeamMembers();
            this.Appearing -= TeamMembersPage_Appearing;
        }

    }
}
