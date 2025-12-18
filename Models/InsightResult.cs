namespace StravigoAI.Models
{
    public class InsightResult
    {
        public string ProjectName { get; set; }
        public double TotalHours { get; set; }
        public double EstimatedHours { get; set; }     // optional estimated effort
        public double ProgressPercent { get; set; }    // 0..100
        public double BudgetUsedPercent { get; set; }  // 0..100
        public double RiskScore { get; set; }          // 0..100 (higher = more risk)
        public string RiskSummary { get; set; }
        public string Recommendations { get; set; }    // advisory action steps
        public double BillableAmount { get; set; }     // in GBP
    }
}
