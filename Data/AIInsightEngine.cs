using StravigoAI.Data;
using StravigoAI.Models;
using System;
using System.Globalization;
using System.Linq;

namespace StravigoAI.Data
{
    public static class AIInsightEngine
    {
        // Primary generator: returns detailed InsightResult
        public static InsightResult GenerateProjectInsights(ProjectModel project)
        {
            var result = new InsightResult
            {
                ProjectName = project.ProjectName
            };

            // Gather timesheet info
            var timesheets = TimesheetDataStore.Timesheets
                .Where(t => t.Project == project.ProjectName)
                .ToList();

            double totalHours = timesheets.Sum(t => t.Hours);
            result.TotalHours = totalHours;

            // Parse ProjectBudget if present (string -> double). If fail, budget = 0
            double budget = 0;
            if (!string.IsNullOrWhiteSpace(project.ProjectBudget))
            {
                var cleaned = project.ProjectBudget.Replace("£", "").Replace("$", "").Replace("€", "")
                                                   .Replace(",", "").Trim();
                double.TryParse(cleaned, NumberStyles.Any, CultureInfo.InvariantCulture, out budget);
            }

            // EstimatedHours: crude heuristic — default 100 or from KPI if numeric present
            double estimated = 100; // default
            if (!string.IsNullOrWhiteSpace(project.KPI))
            {
                var digits = new string(project.KPI.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                if (double.TryParse(digits.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsed))
                    estimated = parsed;
            }
            result.EstimatedHours = Math.Max(estimated, 1);

            // Progress %
            result.ProgressPercent = Math.Min(100, result.EstimatedHours > 0 ? (totalHours / result.EstimatedHours) * 100.0 : 0);

            // Budget used percent (treat hourly rate = 50 GBP by default)
            const double defaultRate = 50.0;
            double spent = totalHours * defaultRate;
            result.BillableAmount = spent;
            result.BudgetUsedPercent = budget > 0 ? Math.Min(100, (spent / budget) * 100) : 0;

            // Risk scoring (0..100). Combine multiple heuristics:
            double score = 0;

            // 1) Deadline proximity heuristic (higher risk if deadline near and progress low)
            try
            {
                var daysTotal = (project.EndDate - project.StartDate).TotalDays;
                var daysElapsed = (DateTime.Today - project.StartDate).TotalDays;
                double timePercent = daysTotal > 0 ? (daysElapsed / daysTotal) * 100.0 : 0;
                if (timePercent > 0)
                {
                    if (result.ProgressPercent + 10 < timePercent)
                        score += Math.Min(40, (timePercent - result.ProgressPercent)); // up to 40
                }
            }
            catch { /* ignore */ }

            // 2) Budget pressure
            if (result.BudgetUsedPercent > 80) score += 25;
            else if (result.BudgetUsedPercent > 40) score += 10;

            // 3) Challenges presence
            if (!string.IsNullOrWhiteSpace(project.Challenges))
                score += 15;

            // 4) Low hours overall relative to estimated
            if (result.ProgressPercent < 25) score += 20;
            else if (result.ProgressPercent < 50) score += 10;

            result.RiskScore = Math.Min(100, score);
            result.RiskSummary = BuildRiskSummary(project, result);
            result.Recommendations = BuildRecommendations(project, result, timesheets);

            return result;
        }

        private static string BuildRiskSummary(ProjectModel project, InsightResult r)
        {
            var parts = new System.Text.StringBuilder();
            parts.AppendLine($"Risk Score: {Math.Round(r.RiskScore)} / 100");
            if (r.ProgressPercent < 50)
                parts.AppendLine("⚠ Progress behind expected schedule.");
            if (r.BudgetUsedPercent > 80)
                parts.AppendLine("⚠ High budget consumption.");
            if (!string.IsNullOrWhiteSpace(project.Challenges))
                parts.AppendLine($"Challenges noted: {project.Challenges}");
            if (string.IsNullOrWhiteSpace(parts.ToString()))
                parts.AppendLine("No immediate high-risk signals detected.");

            return parts.ToString().Trim();
        }

        private static string BuildRecommendations(ProjectModel project, InsightResult r, System.Collections.Generic.List<TimesheetModel> timesheets)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Recommended actions:");

            // Deadline-focused suggestions
            try
            {
                var daysLeft = (project.EndDate - DateTime.Today).TotalDays;
                if (daysLeft < 7 && r.ProgressPercent < 60)
                    sb.AppendLine("- Reassign more resources or add overtime to meet the deadline.");
            }
            catch { }

            // Budget recommendations
            if (r.BudgetUsedPercent > 80)
                sb.AppendLine("- Review scope and negotiate a budget increase or reduce scope.");

            // Invoice suggestion
            var billedAmount = InvoiceDataStore.Invoices.Where(i => i.ProjectName == project.ProjectName).Sum(i => i.TotalAmount);
            if (r.BillableAmount > 0 && billedAmount < r.BillableAmount)
                sb.AppendLine("- Consider issuing an interim invoice for logged billable hours.");

            // Allocation suggestion
            if (r.ProgressPercent < 40)
                sb.AppendLine("- Suggested: move 1-2 team members to this project for the next 2 sprints.");

            sb.AppendLine($"- Total logged hours: {r.TotalHours}h. Billable estimate: £{Math.Round(r.BillableAmount, 2)}");

            return sb.ToString().Trim();
        }
    }
}
