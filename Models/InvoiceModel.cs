using System;
using System.Collections.Generic;

namespace StravigoAI.Models
{
    public class InvoiceModel
    {
        public string InvoiceNumber { get; set; }            
        public string ProjectName { get; set; }              
        public DateTime DateIssued { get; set; }           
        public DateTime DueDate { get; set; }                
        public string ClientName { get; set; }               
        public string ClientEmail { get; set; }              
        public string BillingAddress { get; set; }           
        public string PaymentTerms { get; set; }            
        public string Currency { get; set; }                 
        public double HourlyRate { get; set; }              
        public double Discount { get; set; }                 
        public string Notes { get; set; }                    
        public List<TimesheetModel> Timesheets { get; set; } = new List<TimesheetModel>(); 
        public double TotalHours { get; set; }               
        public double TotalAmount { get; set; }              
        public string Status { get; set; }                   

        public bool IsPaid { get; set; } = false;
    }
}
