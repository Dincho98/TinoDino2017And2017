using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Entities
{
    public class ZapisKnjigePrometa
    { 
        //Svi zapisi nalaze se u stringu kako ne bi trošili procesorsko vrijeme na tzv. "castanje" data typeova.
        public string redniBroj;
        public string brojRacuna;
        public string datum;
        public string iznos;
        public string nacinPlacanja;
    }
}
