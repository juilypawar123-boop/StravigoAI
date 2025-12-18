using Microsoft.Maui.Controls;
using StravigoAI.Models;
using StravigoAI.Data;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class AddTimesheetPage : ContentPage
    {
        public AddTimesheetPage()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {

            ProjectPicker.ItemsSource = ProjectDataStore.Projects.Select(p => p.ProjectName).ToList();
        }

        private async void OnSubmitTimesheetClicked(object sender, EventArgs e)
        {
            if (ProjectPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(HoursEntry.Text) || string.IsNullOrWhiteSpace(TaskDescriptionEntry.Text))
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            if (!double.TryParse(HoursEntry.Text, out double hours))
            {
                await DisplayAlert("Error", "Enter a valid number for hours", "OK");
                return;
            }

            var timesheet = new TimesheetModel
            {
                Date = DatePickerEntry.Date,
                Project = ProjectPicker.SelectedItem.ToString(),
                Hours = hours,
                Task = TaskDescriptionEntry.Text
            };


            TimesheetDataStore.Timesheets.Add(timesheet);

            DatePickerEntry.Date = DateTime.Today;
            ProjectPicker.SelectedIndex = -1;
            HoursEntry.Text = string.Empty;
            TaskDescriptionEntry.Text = string.Empty;

            await DisplayAlert("Success", "Timesheet added successfully!", "OK");

            await Navigation.PopAsync();

        }

    }
}
