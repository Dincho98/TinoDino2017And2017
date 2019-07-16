using System;
using System.Collections.Generic;
using System.Text;

public class StavkeZaIspisUkupno
{
    public class obj
    {
        public int vrstaIspisa { get; set; }
        public string text { get; set; }
        public string iznos { get; set; }
    }

    public List<obj> listaZaIspis = new List<obj>();

    ////primjeri
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 1, text = "PNP", iznos = "12354,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 1, text = "Kartica", iznos = "12354,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 2, text = "10", iznos = "1454,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 2, text = "25", iznos = "3434,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 1, text = "Gotovina", iznos = "134,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 2, text = "10", iznos = "454,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 2, text = "25", iznos = "94,70" });
    //aaa.listaZaIspis.Add(new StavkeZaUkupno.obj { vrsta = 3, text = "SVE UKUPNO:", iznos = "121294,70" });

    public StringBuilder PrintajListu(List<obj> lista, int duljinaNeIznosa,
        int duljinaIznosa, int razmakPrijeOsnovica, int razmakPrijePdv)
    {
        StringBuilder str = new StringBuilder();

        foreach (obj o in lista)
        {
            switch (o.vrstaIspisa)
            {
                case 1:
                    str.Append(PrintajUkupno(o.text, o.iznos, duljinaNeIznosa, duljinaIznosa));
                    break;

                case 2:
                    str.Append(PrintajOsnovicaPdv(o.text, o.iznos, duljinaNeIznosa, duljinaIznosa,
                        razmakPrijeOsnovica, razmakPrijePdv));
                    break;

                case 3:
                    str.Append(PrintajText(o.text, o.iznos, duljinaNeIznosa, duljinaIznosa));
                    break;

                default:
                    break;
            }
        }

        return str;
    }

    private string PrintajText(string text, string iznos, int duljinaNeIznosa, int duljinaIznosa)
    {
        string levo, desno;

        levo = text.PadRight(duljinaNeIznosa);
        desno = iznos.PadLeft(duljinaIznosa);

        return levo + desno + Environment.NewLine;
    }

    private string PrintajUkupno(string ukupnoText, string iznos, int duljinaNeIznosa, int duljinaIznosa)
    {
        string levo, desno;

        levo = ("UKUPNO " + ukupnoText).PadRight(duljinaNeIznosa);
        desno = iznos.PadLeft(duljinaIznosa);

        return levo + desno + Environment.NewLine;
    }

    private string PrintajOsnovicaPdv(string pdv, string iznos, int duljinaNeIznosa,
        int duljinaIznosa, int razmakPrijeOsnovica, int razmakPrijePdv)
    {
        string str = "";
        string levo, desno;
        string pdvString = "PDV " + pdv + "%:";
        string osnovicapdvString = "OSNOVICA PDV " + pdv + "%:";

        levo = osnovicapdvString.PadLeft(razmakPrijeOsnovica + osnovicapdvString.Length).PadRight(duljinaNeIznosa);
        desno = iznos.PadLeft(duljinaIznosa);

        str = levo + desno + Environment.NewLine;

        levo = (pdvString.PadLeft(razmakPrijePdv + pdvString.Length)).PadRight(duljinaNeIznosa);
        desno = (Convert.ToDecimal(iznos) * Convert.ToDecimal(pdv) / 100).ToString("#0.00").PadLeft(duljinaIznosa);//tu treba pomnožiti iznos s pdv

        str += levo + desno + Environment.NewLine;

        return str;
    }
}