using System;
using System.IO;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

#if WEBASSEMBLY
using Microsoft.JSInterop;
#endif

namespace StravigoAI.Pages
{
    public partial class TemplatesPage : ContentPage
    {
        public TemplatesPage()
        {
            InitializeComponent();
        }

        private void OnProjectPlanClicked(object sender, EventArgs e) => LoadTemplates("Project Plan");
        private void OnBudgetTrackerClicked(object sender, EventArgs e) => LoadTemplates("Budget Tracker");
        private void OnMonthlyReportClicked(object sender, EventArgs e) => LoadTemplates("Monthly Report");
        private void OnKPIDashboardClicked(object sender, EventArgs e) => LoadTemplates("KPI Dashboard");

        private void LoadTemplates(string category)
        {
            SidebarTemplates.Children.Clear();

            if (category == "Project Plan")
            {
                AddTemplateItem("Corporate Project's Plan", "Corporate_project_plan.pdf");
                AddTemplateItem("B2B Project's Plan", "B2B_project_plan.pdf");
                AddTemplateItem("Private Equity Project's Plan", "Private_equity_project_plan.pdf");
                AddTemplateItem("Technical Project's Plan", "Technical_project_plan.pdf");
                AddTemplateItem("Media Project's Plan", "Media_project_plan.pdf");
                return;
            }

            if (category == "Budget Tracker")
            {
                AddTemplateItem("Budget Tracker Template 1", "Budget_tracker1.pdf");
                AddTemplateItem("Budget Tracker Template 2", "Budget_planner2.pdf");
            }
            if (category == "Monthly Report")
            {
                AddTemplateItem("Monthly Report Template 1", "Monthly_report1.pdf");
                AddTemplateItem("Mopntly Report Template 2", "Monthly_report2.pdf");
            }
            if (category == "KPI Dashboard")
            {
                AddTemplateItem("KPI Dashboard Template", "Kpi_dashboard.pdf");
               
            }
        }

        private void AddTemplateItem(string title, string fileName)
        {
            var lbl = new Label
            {
                Text = title,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                TextColor = Colors.Black
            };

            var openBtn = new Button
            {
                Text = "Open",
                BackgroundColor = Colors.DodgerBlue,
                TextColor = Colors.Black
            };
            openBtn.Clicked += async (_, __) =>
            {
                await OpenPdfInViewer(fileName);
            };

            var actions = new HorizontalStackLayout
            {
                Spacing = 10,
                Children = { openBtn }
            };

            var card = new Frame
            {
                Padding = 12,
                Margin = new Thickness(0, 0, 0, 6),
                CornerRadius = 8,
                BackgroundColor = Colors.White,
                BorderColor = Colors.LightGray,
                Content = new VerticalStackLayout
                {
                    Spacing = 8,
                    Children = { lbl, actions }
                }
            };

            SidebarTemplates.Children.Add(card);
        }

        private async Task OpenPdfInViewer(string fileName)
        {
            await Navigation.PushAsync(new PdfViewerPage(fileName));
        }
    }
}