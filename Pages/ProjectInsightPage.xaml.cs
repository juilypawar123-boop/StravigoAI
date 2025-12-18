using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using StravigoAI.Data;
using StravigoAI.Models;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class ProjectInsightPage : ContentPage
    {
        private readonly ProjectModel _project;
        public ProjectInsightPage(ProjectModel project)
        {
            InitializeComponent();
            _project = project;
            LoadInsight();
        }

        private void LoadInsight()
        {
            var insight = AIInsightEngine.GenerateProjectInsights(_project);

            ProjectTitle.Text = _project.ProjectName;

            // Progress
            ProgressBarControl.Progress = Math.Min(1.0, insight.ProgressPercent / 100.0);
            ProgressLabel.Text = $"{Math.Round(insight.ProgressPercent, 1)}% complete ({insight.TotalHours}h / est {insight.EstimatedHours}h)";

            // Risk
            RiskScoreLabel.Text = $"{Math.Round(insight.RiskScore)} / 100";
            RiskSummaryLabel.Text = insight.RiskSummary;

            // Budget
            BudgetBar.Progress = Math.Min(1.0, insight.BudgetUsedPercent / 100.0);
            BudgetLabel.Text = $"{Math.Round(insight.BudgetUsedPercent, 1)}% used";

            // Weekly bars (group last 4 weeks)
            WeeklyBars.Children.Clear();
            var weeks = Enumerable.Range(0, 4).Select(w =>
            {
                var weekStart = DateTime.Today.AddDays(-7 * (w + 1));
                var weekEnd = weekStart.AddDays(7);
                var hrs = TimesheetDataStore.Timesheets
                    .Where(t => t.Project == _project.ProjectName && t.Date >= weekStart && t.Date < weekEnd)
                    .Sum(t => t.Hours);
                return new { Week = w, Hours = hrs };
            }).Reverse().ToList();

            double maxHrs = Math.Max(1, weeks.Max(x => x.Hours));
            foreach (var w in weeks)
            {
                var barContainer = new VerticalStackLayout { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.End, WidthRequest = 40 };
                var bar = new BoxView
                {
                    HeightRequest = Math.Max(8, (w.Hours / maxHrs) * 60),
                    WidthRequest = 28,
                    BackgroundColor = Colors.CornflowerBlue,
                    CornerRadius = 4
                };
                barContainer.Children.Add(bar);
                barContainer.Children.Add(new Label { Text = $"{w.Hours}h", FontSize = 10, HorizontalOptions = LayoutOptions.Center });
                WeeklyBars.Children.Add(barContainer);
            }

            // Recommendations
            RecommendationsLabel.Text = insight.Recommendations;

            // Timesheet list
            TimesheetBreakdownList.Children.Clear();
            var timesheets = TimesheetDataStore.Timesheets
                .Where(t => t.Project == _project.ProjectName)
                .OrderByDescending(t => t.Date);

            foreach (var t in timesheets)
            {
                TimesheetBreakdownList.Children.Add(new Frame
                {
                    Padding = 8,
                    CornerRadius = 8,
                    BackgroundColor = Colors.White,
                    Content = new VerticalStackLayout
                    {
                        Children =
                        {
                            new Label { Text = $"{t.Date:yyyy-MM-dd} — {t.Hours}h", FontAttributes = FontAttributes.Bold },
                            new Label { Text = t.Task, FontSize = 12 }
                        }
                    }
                });
            }
        }

        private async void OnExportClicked(object sender, EventArgs e)
        {
            var insight = AIInsightEngine.GenerateProjectInsights(_project);
            var summary = $"Project: {_project.ProjectName}\nRisk: {insight.RiskSummary}\nRecommendations:\n{insight.Recommendations}";
            await Clipboard.SetTextAsync(summary);
            await DisplayAlert("Exported", "Insight summary copied to clipboard.", "OK");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
