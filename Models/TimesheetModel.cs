namespace StravigoAI.Models
{
    public class TimesheetModel
    {
        public string Project { get; set; }
        public DateTime Date { get; set; }
        public double Hours { get; set; }
        public string Task { get; set; }
    }
}