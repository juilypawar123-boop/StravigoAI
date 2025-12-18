using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using StravigoAI.Data;
using StravigoAI.Models;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class GlobalAIInsightsPage : ContentPage
    {
        private string CurrentUserRole = "Manager";

        public GlobalAIInsightsPage()
        {
            InitializeComponent();
            LoadInsights();
        }

        private void LoadInsights()
        {
            ProjectInsightsList.Children.Clear();

            var projects = ProjectDataStore.Projects;

            if (!projects.Any())
            {
                RoleInfoLabel.Text = "No projects available.";
                return;
            }

            RoleInfoLabel.Text = CurrentUserRole == "Manager"
                ? "Viewing intelligence for all strategic initiatives."
                : "Viewing intelligence for your assigned engagements.";

            // Small aggregated stats
            var totalRisks = 0;
            var totalOpportunities = 0;

            foreach (var project in projects)
            {
                var insight = AIInsightEngine.GenerateProjectInsights(project);
                if (insight.RiskScore > 50) totalRisks++;
                if (insight.BillableAmount > 0 && insight.TotalHours > 20) totalOpportunities++;

                AddProjectInsight(project, insight);
            }

            RiskAlertsLabel.Text = $"Risk Alerts: {totalRisks}";
            OpportunitiesLabel.Text = $"Opportunities: {totalOpportunities}";
        }

        private void AddProjectInsight(ProjectModel project, InsightResult insight)
        {
            var frame = new Frame
            {
                BackgroundColor = Colors.White,
                CornerRadius = 12,
                Padding = 12,
                HasShadow = true,
                Margin = new Thickness(0, 6, 0, 6),
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children =
                    {
                        new Label { Text = project.ProjectName, FontSize = 18, FontAttributes = FontAttributes.Bold, TextColor = Colors.Black },
                        new Label { Text = insight.RiskSummary, FontSize = 13, LineBreakMode = LineBreakMode.WordWrap, TextColor = Colors.DarkSlateGray },
                        new HorizontalStackLayout
                        {
                            Spacing = 8,
                            Children =
                            {
                                new Button
                                {
                                    Text = "View Insights",
                                    BackgroundColor = Colors.CornflowerBlue,
                                    TextColor = Colors.White,
                                    Command = new Command(async () => await Navigation.PushAsync(new ProjectInsightPage(project)))
                                },
                                new Label { Text = $"Risk: {Math.Round(insight.RiskScore)}", VerticalOptions = LayoutOptions.Center }
                            }
                        }
                    }
                }
            };

            ProjectInsightsList.Children.Add(frame);
        }
    }
}
