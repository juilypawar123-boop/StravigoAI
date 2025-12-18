using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StravigoAI.Services
{
    public class NotificationService
    {
        private static NotificationService _instance;
        public static NotificationService Instance => _instance ??= new NotificationService();

        public ObservableCollection<MeetingRequest> Notifications { get; private set; }

        private NotificationService()
        {
            Notifications = new ObservableCollection<MeetingRequest>();
        }

        public Task AddNotification(MeetingRequest request)
        {
            Notifications.Add(request);
            return Task.CompletedTask;
        }
    }

    public class MeetingRequest
    {
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Duration { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
