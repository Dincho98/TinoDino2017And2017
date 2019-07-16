using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Entities
{
    public class Faktura
    {
        public int BrojFakture { get; set; }
        public int IdOdrediste { get; set; }
        public int IdFakturirati { get; set; }
        public int IdSkladiste { get; set; }
        public string Datum { get; set; }
        public string DatumDVO { get; set; }
        public string DatumValute { get; set; }
        public int IdZaposlenik { get; set; }
        public string Napomena { get; set; }
        public List<FakturaStavke> Stavke { get; set; }
    }
}
