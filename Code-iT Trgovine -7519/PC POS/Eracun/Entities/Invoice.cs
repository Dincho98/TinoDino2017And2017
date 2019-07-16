using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Eracun.Entities
{
    public class Invoice : EntityBase
    {
        public int TakeOverPartnerId { get; set; }
        public int InvoiceForPartnerId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateDVO { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateCurrency { get; set; }
        public string Currency { get; set; }
        public decimal CurrencyRate { get; set; }
        public int Days { get; set; }
        public string Remark { get; set; }
        public Employee Employee { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
