using System;
using System.Data;
using System.Text;

namespace PCPOS.Util
{
    internal class classNabavneCijene
    {
        public void PostaviNBC()
        {
            DataTable DTulazIzlaz = VratiUlazIzlazDateTable();
            DataRow[] RowUlaz = DTulazIzlaz.Select("ui='ulaz'");
            DataRow[] RowIzlaz = DTulazIzlaz.Select("ui='izlaz'");
            DataRow[] RowUlazArtikl = DTulazIzlaz.Select("ui='0'");//varka

            decimal zadnji = 0, izlKolicinaTEMP = 0, izlKolicina = 0, ulazKolicina = 0, nbcUlaz = 0, a1 = 0, ulazKolicinaZbroj = 0, ulazStanjePoSifri = 0;
            string zadnjaSifraUlaz = "", zadnjaSifraIzlaz = "";
            StringBuilder sbSQL = new StringBuilder();
            int brojSQLa = 0;

            foreach (DataRow ROWizlaz in RowIzlaz)
            {
                zadnjaSifraIzlaz = ROWizlaz["sifra"].ToString();
                decimal.TryParse(ROWizlaz["kolicina"].ToString(), out izlKolicina);
                izlKolicinaTEMP = izlKolicina;
                ulazKolicinaZbroj = 0;
                a1 = 0;

                if (zadnjaSifraUlaz != zadnjaSifraIzlaz)
                {
                    RowUlazArtikl = DTulazIzlaz.Select("sifra='" + zadnjaSifraIzlaz + "' AND ui='ulaz'");
                    zadnji = 0;
                }

                foreach (DataRow RowU in RowUlazArtikl)
                {
                    decimal.TryParse(RowU["kolicina"].ToString(), out ulazKolicina);
                    decimal.TryParse(RowU["nbc"].ToString(), out nbcUlaz);
                    zadnjaSifraUlaz = RowU["sifra"].ToString();

                    ulazKolicinaZbroj += ulazKolicina;
                    ulazStanjePoSifri += ulazKolicina;

                    if (ulazKolicina > 0)
                    {
                        if (ulazKolicina >= izlKolicinaTEMP)
                        {
                            a1 += izlKolicinaTEMP * nbcUlaz;
                            RowU["kolicina"] = ulazKolicina - izlKolicinaTEMP;
                            break;
                        }
                        else
                        {
                            a1 += ulazKolicina * nbcUlaz;
                            izlKolicinaTEMP = izlKolicinaTEMP - ulazKolicina;
                            RowU["kolicina"] = 0;
                        }
                    }
                }

                string tablica = ROWizlaz["doc"].ToString();

                if (tablica == "maloprodaja")
                {
                    tablica = "racun_stavke";
                }
                else if (tablica == "fakture")
                {
                    tablica = "faktura_stavke";
                }
                else if (tablica == "iz_skl")
                {
                    tablica = "meduskladisnica_stavke";
                }
                else if (tablica == "izdatnica")
                {
                    tablica = "izdatnica_stavke";
                }
                else if (tablica == "otpremnica")
                {
                    tablica = "otpremnica_stavke";
                }
                else if (tablica == "otpis_robe")
                {
                    tablica = "otpis_robe_stavke";
                }
                else if (tablica == "povrat_robe")
                {
                    tablica = "povrat_robe_stavke";
                }

                sbSQL.Append("UPDATE " + tablica + " SET nbc='" + Math.Round((a1 / izlKolicina), 4).ToString().Replace(".", ",") + "' WHERE id_stavka='" + ROWizlaz["id"].ToString() + "';");

                if (brojSQLa > 1000)
                {
                    classSQL.update(sbSQL.ToString());
                    sbSQL.Clear();
                    brojSQLa = 0;
                }
            }
            classSQL.update(sbSQL.ToString());
        }

        #region UZMI ULAZE ROBNO

        public DataTable VratiUlazIzlazDateTable()
        {
            return classSQL.select("SELECT * FROM ulaz_izlaz_robe_financijski " +
                " WHERE doc<>'promjena_cijene'" +
                " AND doc<>'inventura' ORDER BY sifra,datum;", "ulaz_robe").Tables[0];
        }

        #endregion UZMI ULAZE ROBNO
    }
}

/*if (ulazKolicinaZbroj > zadnji)
{
    if (izlKolicina >= ulazKolicinaZbroj)
    {
        a1 += ulazKolicina * nbcUlaz;
        ulazKolicinaZbroj -= ulazKolicina;
        zadnji += ulazKolicina;
    }
    else if (izlKolicina < ulazKolicinaZbroj && izlKolicina > ulazKolicina)
    {
        a1 += (izlKolicina - (ulazKolicinaZbroj - ulazKolicina)) * nbcUlaz;
        ulazKolicinaZbroj -= (izlKolicina - (ulazKolicinaZbroj - ulazKolicina));
        zadnji += (izlKolicina - (ulazKolicinaZbroj - ulazKolicina));
    }
    else if (izlKolicina < ulazKolicinaZbroj && izlKolicina < ulazKolicina)
    {
        a1 += izlKolicina * nbcUlaz;
        ulazKolicinaZbroj -= izlKolicina;
        zadnji += izlKolicina;
        break;
    }
}*/
