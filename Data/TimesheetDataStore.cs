using System.Collections.ObjectModel;
using StravigoAI.Models;

namespace StravigoAI.Data
{
    public static class TimesheetDataStore
    {
        // ObservableCollection allows UI to automatically update when items are added
        public static ObservableCollection<TimesheetModel> Timesheets { get; set; }
            = new ObservableCollection<TimesheetModel>
        {
            // Sample initial data (optional)
            new TimesheetModel { Project = "Project Alpha", Date = DateTime.Today.AddDays(-2), Hours = 8, Task = "Design architecture" },
            new TimesheetModel { Project = "Project Alpha", Date = DateTime.Today.AddDays(-1), Hours = 6, Task = "Development" },
            new TimesheetModel { Project = "Project Bravo", Date = DateTime.Today, Hours = 7.5, Task = "Testing" }
        };
    }
}
