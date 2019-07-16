using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    /// Country details.
    public class Country
    {
        // Country id.
        // Mandatory field. Ignored on create request. 
        public long? CountryId { get; set; }
        // Country code.
        // Max length: 30 
        public string Code { get; set; }
        // Country name.
        // Max length: 250 
        public string Name { get; set; }
        // Country currency.
        public Currency Currency { get; set; }
    }
}
