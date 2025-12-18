using Microsoft.Maui.Controls;
using StravigoAI.Services;

namespace StravigoAI.Pages
{
    public partial class NotificationsPage : ContentPage
    {
        public NotificationsPage()
        {
            InitializeComponent();
            NotificationsCollection.ItemsSource = NotificationService.Instance.Notifications;
        }
    }
}
