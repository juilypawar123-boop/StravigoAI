using System.Collections.ObjectModel;
using StravigoAI.Models;

namespace StravigoAI.Data
{
    public static class TeamMemberDataStore
    {
        public static ObservableCollection<TeamMemberModel> Members { get; set; } = new ObservableCollection<TeamMemberModel>
        {
            new TeamMemberModel { Name="Alice Smith", Role="Manager", Email="alice@example.com", Phone="123-456-7890", AssignedProjects=new string[]{"Project Alpha","Project Delta"} },
            new TeamMemberModel { Name="Bob Johnson", Role="Consultant", Email="bob@example.com", Phone="234-567-8901", AssignedProjects=new string[]{"Project Alpha"} },
            new TeamMemberModel { Name="Carol White", Role="Developer", Email="carol@example.com", Phone="345-678-9012", AssignedProjects=new string[]{"Project Bravo"} }
        };
    }
}
