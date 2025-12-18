namespace StravigoAI.Models
{
    public class ProjectModel
    {
        public string ProjectName { get; set; }
        public string Objectives { get; set; }
        public string Goals { get; set; }
        public string Challenges { get; set; }
        public string TeamMemberAssigned { get; set; }
        public string ReportingManager { get; set; }
        public string SoftwareSupport { get; set; }
        public string ProjectBudget { get; set; }
        public string ClientExpectations { get; set; }
        public string PointOfContactName { get; set; }
        public string PointOfContactEmail { get; set; }
        public string ClientConfidentiality { get; set; }
        public string ReportingFrequency { get; set; }
        public string EscalationProtocol { get; set; }
        public string KPI { get; set; }
        public string QualityStandards { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Deadline { get; set; }
    }
}
