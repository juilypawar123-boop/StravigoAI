using System.Collections.Generic;

namespace StravigoAI.Models
{
    public class TeamMember
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public List<string> AssignedProjects { get; set; } = new List<string>();
    }
}
