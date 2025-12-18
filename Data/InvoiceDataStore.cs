using System.Collections.ObjectModel;
using StravigoAI.Models;

namespace StravigoAI.Data
{
    public static class InvoiceDataStore
    {
        public static ObservableCollection<InvoiceModel> Invoices { get; set; }
            = new ObservableCollection<InvoiceModel>();
    }
}
