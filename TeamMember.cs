namespace StravigoAI.Models
{
    public class TeamMemberModel
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string[] AssignedProjects { get; set; }
    }
}
