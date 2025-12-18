using System.Collections.ObjectModel;
using StravigoAI.Models;

namespace StravigoAI.Data
{
    public static class ProjectDataStore
    {
        public static ObservableCollection<ProjectModel> Projects { get; set; }
            = new ObservableCollection<ProjectModel>();
    }
}