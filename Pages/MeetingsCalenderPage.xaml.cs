using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace StravigoAI.Pages
{
    public partial class MeetingsCalenderPage : ContentPage
    {
        private DateTime _currentMonth;

        private ObservableCollection<Meeting> Meetings { get; set; } = new ObservableCollection<Meeting>();
        public ObservableCollection<CalendarDay> CalendarDays { get; set; } = new ObservableCollection<CalendarDay>();

        public MeetingsCalenderPage()
        {
            InitializeComponent();
            CalendarCollectionView.ItemsSource = CalendarDays;
            _currentMonth = DateTime.Today;
            LoadCalendar(_currentMonth);
        }

        private void LoadCalendar(DateTime month)
        {
            CalendarDays.Clear();
            MonthLabel.Text = month.ToString("MMMM yyyy");

            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            for (int i = 1; i <= daysInMonth; i++)
            {
                var date = new DateTime(month.Year, month.Month, i);

                // Get meetings for this day
                var meetingsForDay = Meetings.Where(m => m.Date.Date == date.Date).ToList();
                string summary = meetingsForDay.Count == 0 ? "No meetings" :
                                 string.Join(", ", meetingsForDay.Select(m => m.Subject));

                CalendarDays.Add(new CalendarDay
                {
                    DayNumber = i.ToString(),
                    MeetingSummary = summary
                });
            }
        }

        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(-1);
            LoadCalendar(_currentMonth);
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            _currentMonth = _currentMonth.AddMonths(1);
            LoadCalendar(_currentMonth);
        }
        private async void OnAddMeetingClicked(object sender, EventArgs e)
        {
            string subject = await DisplayPromptAsync("New Meeting", "Enter meeting title:");
            if (string.IsNullOrWhiteSpace(subject))
                return;

            DateTime date = DateTime.Today;
            string dateString = await DisplayPromptAsync("Meeting Date", "Enter date (YYYY-MM-DD):", initialValue: date.ToString("yyyy-MM-dd"));
            if (!DateTime.TryParse(dateString, out date))
                date = DateTime.Today;

            string timeString = await DisplayPromptAsync("Meeting Time", "Enter time (HH:mm):", initialValue: DateTime.Now.ToString("HH:mm"));
            TimeSpan time;
            if (!TimeSpan.TryParse(timeString, out time))
                time = new TimeSpan(9, 0, 0);

            Meetings.Add(new Meeting
            {
                Subject = subject,
                Date = date.Date + time,
            });

            LoadCalendar(_currentMonth);
        }
    }

    public class CalendarDay
    {
        public string DayNumber { get; set; }
        public string MeetingSummary { get; set; }
    }

    public class Meeting
    {
        public string Subject { get; set; }
        public DateTime Date { get; set; }
    }
}
