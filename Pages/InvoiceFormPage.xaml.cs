using Microsoft.Maui.Controls;
using StravigoAI.Data;
using StravigoAI.Models;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class InvoiceFormPage : ContentPage
    {
        public InvoiceFormPage()
        {
            InitializeComponent();

            // Set default dates
            InvoiceDatePicker.Date = DateTime.Today;
            DueDatePicker.Date = DateTime.Today;

            // Load projects into the picker
            LoadProjects();
        }

        private void LoadProjects()
        {
            // Populate ProjectPicker with project names
            var projectNames = ProjectDataStore.Projects.Select(p => p.ProjectName).ToList();
            ProjectPicker.ItemsSource = projectNames;
        }

        private async void OnGenerateInvoiceClicked(object sender, EventArgs e)
        {
            // Validate project selection
            if (ProjectPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Select a project.", "OK");
                return;
            }

            // Validate hourly rate
            if (!double.TryParse(HourlyRateEntry.Text, out double rate))
            {
                await DisplayAlert("Error", "Enter a valid hourly rate.", "OK");
                return;
            }

            var selectedProject = ProjectPicker.SelectedItem.ToString();

            // Get timesheets for the selected project
            var selectedTimesheets = TimesheetDataStore.Timesheets
                .Where(t => t.Project == selectedProject)
                .ToList();

            if (!selectedTimesheets.Any())
            {
                await DisplayAlert("Error", "No timesheets found for this project.", "OK");
                return;
            }

            // Calculate total hours and amount
            double totalHours = selectedTimesheets.Sum(t => t.Hours);
            double totalAmount = totalHours * rate;

            // Create a new invoice
            var invoice = new InvoiceModel
            {
                InvoiceNumber = InvoiceNumberEntry.Text,
                ProjectName = selectedProject,
                DateIssued = InvoiceDatePicker.Date,
                DueDate = DueDatePicker.Date,
                Timesheets = selectedTimesheets,
                TotalHours = totalHours,
                TotalAmount = totalAmount,
                Status = "Pending"
            };

            // Add invoice to data store
            InvoiceDataStore.Invoices.Add(invoice);

            // Confirmation alert
            await DisplayAlert("Success", $"Invoice Generated!\nProject: {selectedProject}\nTotal Hours: {totalHours}\nTotal Amount: £{totalAmount}", "OK");

            // Clear the form
            ProjectPicker.SelectedIndex = -1;
            HourlyRateEntry.Text = string.Empty;
            InvoiceNumberEntry.Text = string.Empty;
            InvoiceDatePicker.Date = DateTime.Today;
            DueDatePicker.Date = DateTime.Today;

            await Navigation.PopAsync();
        }
    }
}
