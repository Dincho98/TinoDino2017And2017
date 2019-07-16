using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    public class VatRate
    {
        // Vat rate id.
        // Mandatory field. Ignored on create request. 
        public long VatRateId { get; set; }
        // VAT rate codes:
        // <ul>
        //     <li>S - Standard rate</li>
        //     <li>Z - Reduced rate</li>
        //     <li>0 - Lower rate</li>
        //     <li>O - Exempted</li>
        //     <li>N - Non-taxable</li>
        //  <ul>
        // Mandatory field. Max length: 30 
        public string Code { get; set; }
        // Interest percent.
        public Decimal Percent { get; set; }

    }
}
