using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.SQL
{
    internal class ClassSkladistePosebno
    {
        public static string GetAmount(string sifra, string skladiste, string oduzeti, string mnozeno_kolicinom, string funk)
        {
            double kol_skladiste = 0;
            double kol = 0;
            DataTable sql_kol_skl = classSQL.select("Select max(id_stavka) from promjena_cijene_komadno_stavke where sifra = '" + sifra + "' ", "kolicina na skladistu posebno").Tables[0];
            //DataSet DSkol = classSQL.select("SELECT kolicina_ostatak FROM promjena_cijene_komadno_stavke WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "' AND id_stavka = '" + sql_kol_skl.Rows[0][0].ToString() + "' ", "promjena_cijene_komadno_stavke");

            DataSet DSkol = classSQL.select("SELECT kolicina_ostatak FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                kol_skladiste = 0;
                DataTable STrob = classSQL.select("SELECT nc,vpc,porez,porez_potrosnja FROM roba WHERE sifra ='" + sifra + "'", "roba").Tables[0];
                if (classSQL.remoteConnectionString == "")
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + ")");
                }
                else
                {
                    classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString() + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "')");
                }
            }
            else
            {
                kol_skladiste = Convert.ToDouble(DSkol.Tables[0].Rows[0][0].ToString());
            }

            if (funk == "-")
            {
                kol = Convert.ToDouble(kol_skladiste) - (Convert.ToDouble(oduzeti) * Convert.ToDouble(mnozeno_kolicinom));
            }
            else
            {
                kol = Convert.ToDouble(kol_skladiste) + (Convert.ToDouble(oduzeti) * Convert.ToDouble(mnozeno_kolicinom));
            }

            return kol.ToString();
        }

        public static void provjeri_roba_prodaja(string sifra, string skladiste)
        {
            double kol_skladiste = 0;
            double kol = 0;
            DataSet DSkol = classSQL.select("SELECT kolicina_ostatak FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                kol_skladiste = 0;
                DataTable STrob = classSQL.select("SELECT nc,vpc,porez,porez_potrosnja FROM roba WHERE sifra ='" + sifra + "'", "roba").Tables[0];
                if (classSQL.remoteConnectionString == "")
                {
                    try
                    {
                        classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + ")");
                    }
                    catch (Exception ex)
                    { MessageBox.Show("Artikl ne postoji !"); }
                }
                else
                {
                    try
                    {
                        classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString() + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "')");
                    }
                    catch (Exception ex)
                    { MessageBox.Show("Artikl ne postoji !"); }
                }
            }
        }

        //public static string GetAmountCaffe(string sifra, string skladiste, string kolicina, string funk)
        //{
        //    double kol = 0;

        //    DataTable DTnormativi = classSQL.select("SELECT * FROM caffe_normativ WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "caffe_normativ").Tables[0];
        //    DataTable DTroba_prodaja = classSQL.select("SELECT * FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja").Tables[0];

        //    if (DTnormativi.Rows.Count != 0)
        //    {
        //        if (funk == "-") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString())-(Convert.ToDouble(DTnormativi.Rows[0]["kolicina"].ToString()) * Convert.ToDouble(kolicina)); }
        //        else if (funk == "+") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString())-(Convert.ToDouble(DTnormativi.Rows[0]["kolicina"].ToString()) * Convert.ToDouble(kolicina)); }

        //        for (int i = 0; i < DTnormativi.Rows.Count; i++)
        //        {
        //            classSQL.update("UPDATE roba_prodaja SET kolicina='" + kol + "' WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'");
        //            //classSQL.update("UPDATE caffe_normativ SET kolicina='" + kolicina + "' WHERE sifra_normativ='" + DTnormativi.Rows[0]["sifra_normativ"].ToString() + "' AND id_skladiste='" + skladiste + "'");
        //        }

        //    }
        //    else
        //    {
        //        if (funk == "-") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) - Convert.ToDouble(kolicina); }
        //        else if (funk == "+") { kol = Convert.ToDouble(DTroba_prodaja.Rows[0]["kolicina"].ToString()) - Convert.ToDouble(kolicina); }
        //        classSQL.update("UPDATE roba_prodaja SET kolicina='" + kolicina + "' WHERE sifra='" + sifra + "' AND id_skladiste='"+skladiste+"'");
        //    }

        //    return kol.ToString();
        //}
    }
}