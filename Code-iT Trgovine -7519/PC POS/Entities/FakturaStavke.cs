using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Entities
{
    public class FakturaStavke
    {
        public int BrojFakture { get; set; }
        public string SifraRobe { get; set; }
        public string Naziv { get; set; }
        public decimal Kolicina { get; set; }
        public decimal Nbc { get; set; }
        public decimal Mpc { get; set; }
        public decimal Vpc { get; set; }
        public decimal Porez { get; set; }
        public decimal Rabat { get; set; }
    }
}
