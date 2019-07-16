using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Entities
{
    public class Gost
    {       
        //Variables
        public int broj { get; set; }
        public string imePrezime { get; set; }
        // Ako je osobna: 3314325432
        // Ako je broj putovnice: 32141453424
        // Ako je osobna+putovnica: 3314325432,
        public string brojOsobne { get; set; } 
        public string brojPutovnice { get; set; }
        public string vrstaPruzeneUsluge { get; set; }
        public string datumPocetka { get; set; }
        public string datumKraja { get; set; }
        public string primjedba;
    }
}
