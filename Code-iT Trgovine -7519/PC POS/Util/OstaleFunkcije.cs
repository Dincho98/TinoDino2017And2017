using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PCPOS
{
    internal class OstaleFunkcije
    {
        public static DataTable DSaktivnosDok;

        public static void SetInLog(string greska, string linija, string forma)
        {
            try
            {
                if (File.Exists("log.txt"))
                {
                    string[] lines = { "\r\n**************************************************************",
                                         "DATUM:"+ DateTime.Now.ToString()+", FORMA: " + forma,
                                         "LINIJA KODA:"+linija,
                                         "GREŠKA:\r\n"+greska,
                                        "**************************************************************" };

                    System.IO.File.AppendAllLines("log.txt", lines);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void PovecajBrojIspisaRacuna(string broj, string poslovnica, string naplatni, string doc)
        {
            if (doc == "RAC")
                classSQL.update("UPDATE racuni SET broj_ispisa=COALESCE(broj_ispisa,0)+1" + " WHERE broj_racuna='" + broj + "' AND id_kasa='" + naplatni + "' AND id_ducan='" + poslovnica + "'");
            else if (doc == "FAK")
                classSQL.update("UPDATE fakture SET broj_ispisa=COALESCE(broj_ispisa,0)+1 WHERE broj_fakture='" + broj + "' AND id_kasa='" + naplatni + "' AND id_ducan='" + poslovnica + "'");
        }

        public static bool OIB_Validacija(string oib)
        {
            try
            {
                if (oib.Length != 11)
                    return false;

                int znamenka, zbroj, medjuOstatak, ostatak, umnozak;
                ostatak = 10;

                for (int charIndex = 0; charIndex < oib.Length - 1; charIndex++)
                {
                    znamenka = Convert.ToInt32(oib[charIndex].ToString());
                    zbroj = znamenka + ostatak;
                    medjuOstatak = zbroj % 10;
                    if (medjuOstatak == 0)
                        medjuOstatak = 10;
                    umnozak = medjuOstatak * 2;
                    ostatak = umnozak % 11;
                }

                int kontrolniBroj = 11 - ostatak;

                if (Convert.ToInt32(oib[oib.Length - 1].ToString()) == kontrolniBroj)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static decimal getUkupnoFromTable(DataTable dt, string columnName, string filter)
        {
            try
            {
                decimal ukp = 0, d = 0;
                DataView dv = new DataView(dt);
                dv.RowFilter = filter;
                dt = dv.Table;
                foreach (DataRow r in dt.Rows)
                {
                    Decimal.TryParse(r[columnName].ToString().Replace('.', ','), out d);
                    ukp += d;
                    d = 0;
                }

                return ukp;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}