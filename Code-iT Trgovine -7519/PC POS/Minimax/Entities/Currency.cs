using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Minimax.Entities
{
    /// Currency details.
    public class Currency
    {
        // Currency id.
        public long? CurrencyId { get; set; }
        // Currency code.
        // Max length: 30 
        public string Code { get; set; }
        // Currency name.
        // Max length: 250 
        public string Name { get; set; }

    }
}
