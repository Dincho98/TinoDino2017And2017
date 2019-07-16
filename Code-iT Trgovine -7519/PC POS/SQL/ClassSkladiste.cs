using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.SQL
{
    internal class ClassSkladiste
    {
        public static string GetAmount(string sifra, string skladiste, string oduzeti, string mnozeno_kolicinom, string funk)
        {
            double kol_skladiste = 0;
            double kol = 0;
            DataSet DSkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

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
            DataSet DSkol = classSQL.select("SELECT kolicina FROM roba_prodaja WHERE sifra='" + sifra + "' AND id_skladiste='" + skladiste + "'", "roba_prodaja");

            if (DSkol.Tables[0].Rows.Count == 0)
            {
                kol_skladiste = 0;
                DataTable STrob = classSQL.select("SELECT nc,vpc,porez,porez_potrosnja FROM roba WHERE sifra ='" + sifra + "'", "roba").Tables[0];
                if (classSQL.remoteConnectionString == "")
                {
                    try
                    {
                        if (STrob != null && STrob.Rows.Count > 0)
                        {
                            classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra,porez_potrosnja) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "','" + STrob.Rows[0]["porez_potrosnja"].ToString() + ")");
                        }
                        else {
                            MessageBox.Show("Artikl " + sifra + " ne postoji !");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Artikl " + sifra + " ne postoji !");
                    }
                }
                else
                {
                    try
                    {
                        if (STrob != null && STrob.Rows.Count > 0)
                        {
                            classSQL.insert("INSERT INTO roba_prodaja (id_skladiste,kolicina,nc,vpc,porez,sifra) VALUES ('" + skladiste + "','0','" + STrob.Rows[0]["nc"].ToString() + "','" + STrob.Rows[0]["vpc"].ToString().Replace(",", ".") + "','" + STrob.Rows[0]["porez"].ToString() + "','" + sifra + "')");
                        }
                        else
                        {
                            MessageBox.Show("Artikl " + sifra + " ne postoji !");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Artikl " + sifra + " ne postoji !");
                    }
                }
            }
        }
    }
}