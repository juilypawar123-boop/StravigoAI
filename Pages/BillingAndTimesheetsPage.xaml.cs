using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using StravigoAI.Data;
using StravigoAI.Models;
using System;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class BillingAndTimesheetsPage : ContentPage
    {
        // Simulated current user role
        private string CurrentUserRole = "Manager"; // Change to "Consultant" for testing

        public BillingAndTimesheetsPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Clear previous UI entries
            TimesheetsList.Children.Clear();
            InvoicesList.Children.Clear();

            // Get relevant timesheets based on user role
            var relevantTimesheets = CurrentUserRole == "Manager"
                ? TimesheetDataStore.Timesheets
                : TimesheetDataStore.Timesheets.Where(t => t.Task.Contains("Your")); // Example: only user's timesheets

            double totalHours = 0;

            // Populate timesheets
            foreach (var timesheet in relevantTimesheets)
            {
                totalHours += timesheet.Hours;

                var frame = new Frame
                {
                    BackgroundColor = Colors.LightGray,
                    CornerRadius = 8,
                    Padding = 10,
                    Content = new Label
                    {
                        Text = $"{timesheet.Date:yyyy-MM-dd} | {timesheet.Project} | {timesheet.Hours}h | {timesheet.Task}",
                        TextColor = Colors.Black
                    }
                };

                TimesheetsList.Children.Add(frame);
            }

            // Update totals
            HoursLabel.Text = totalHours.ToString();
            BillableLabel.Text = $"£{totalHours * GetHourlyRate()}";

            // Update pending invoices count
            PendingInvoicesLabel.Text = InvoiceDataStore.Invoices.Count(i => !i.IsPaid).ToString();

            // Populate invoices
            foreach (var invoice in InvoiceDataStore.Invoices)
            {
                var invoiceFrame = new Frame
                {
                    BackgroundColor = Colors.LightYellow,
                    CornerRadius = 8,
                    Padding = 10,
                    Content = new Label
                    {
                        Text = $"Invoice #{invoice.InvoiceNumber} | {invoice.ProjectName} | £{invoice.TotalAmount} | {(invoice.IsPaid ? "Paid" : "Pending")}",
                        TextColor = Colors.Black
                    }
                };

                InvoicesList.Children.Add(invoiceFrame);
            }
        }

        private double GetHourlyRate()
        {
            // You can customize hourly rate per project if needed
            return 50;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadData(); // Refresh UI when page appears
        }

        private async void AddTimesheets_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTimesheetPage());
        }

        private async void GenerateInvoice_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InvoiceFormPage());
        }
    }
}
