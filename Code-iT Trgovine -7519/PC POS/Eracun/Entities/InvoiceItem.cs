using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Eracun.Entities
{
    public class InvoiceItem : EntityBase
    {
        public string MerchandiseId { get; set; }
        public string Name { get; set; }
        public string UnitOfMeasure { get; set; }
        public int Amount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholesalePrice { get; set; }
        public decimal DiscountPercentage { get; set; }
    }
}
