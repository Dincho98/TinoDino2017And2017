using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Class
{
    internal class ZapisnikPromjeneCijene
    {
        private DataTable _dtStavke;
        private string broj_zapisnika;
        private bool _kreiraniZapisnik = false;

        public DataTable dtStavke { get { return _dtStavke; } }
        public string brojZapisnik { get { return broj_zapisnika; } }
        public bool kreiraniZapisnik { get { return _kreiraniZapisnik; } }
        private bool editiranjeAutomatskogZapisnika = false;

        //sluzi kod storniranja
        public ZapisnikPromjeneCijene(string oldNapomena, string newNapomena)
        {
            try
            {
                string sql = string.Format("select * from promjena_cijene where napomena = '{0}'", oldNapomena);
                DataSet dsPromjenaCijene = classSQL.select(sql, "promjena_cijene");
                if (dsPromjenaCijene != null && dsPromjenaCijene.Tables.Count > 0 && dsPromjenaCijene.Tables[0] != null && dsPromjenaCijene.Tables[0].Rows.Count > 0)
                {
                    string br = brojPromjene();

                    sql = string.Format("select {0} as br, sifra, stara_cijena, postotak, nova_cijena, pdv, 0 as iznos, replace(kolicina,',','.')::numeric * (-1) as kolicina, '' as id_stavka from promjena_cijene_stavke where broj = '{1}'", br, Convert.ToInt32(dsPromjenaCijene.Tables[0].Rows[0]["broj"]));
                    DataSet dsPcStavke = classSQL.select(sql, "promjena_cijene_stavke");
                    if (dsPcStavke != null && dsPcStavke.Tables.Count > 0 && dsPcStavke.Tables[0] != null && dsPcStavke.Tables[0].Rows.Count > 0)
                    {
                        GenerirajTablicuZaStavke(true);
                        _dtStavke = dsPcStavke.Tables[0];
                        ZapisnikSpremi(br, dsPromjenaCijene.Tables[0].Rows[0]["id_skladiste"].ToString(), DateTime.Now, newNapomena);
                        _kreiraniZapisnik = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ZapisnikPromjeneCijene(DataTable dt, bool b, DateTime datum, string napomena = null, string broj_dokumenta = null, int broj_zapisnika = 0)
        {
            try
            {
                if (b)
                {
                    _dtStavke = dt;
                }
                else
                {
                    GenerirajTablicuZaStavke();

                    DataView dv = dt.DefaultView;
                    dv.Sort = "id_skladiste asc";
                    dt = dv.ToTable();

                    string nap = (napomena != null ? napomena + " broj " + broj_dokumenta.ToString() : "");
                    string id_skladiste = "";

                    if (broj_zapisnika == 0)
                    {
                        this.broj_zapisnika = brojPromjene();
                        editiranjeAutomatskogZapisnika = false;
                    }
                    else
                    {
                        this.broj_zapisnika = broj_zapisnika.ToString();
                        editiranjeAutomatskogZapisnika = true;
                    }

                    foreach (DataRow dRow in dt.Rows)
                    {
                        decimal kolicina = 0;
                        decimal pov_nak = 0;

                        DataSet dsIsUsluga = classSQL.select(string.Format("select case when oduzmi = 'DA' then false else true end as usluga from roba where sifra = '{0}'", dRow["sifra"].ToString()), "roba");
                        if (dsIsUsluga == null || dsIsUsluga.Tables.Count == 0 || dsIsUsluga.Tables[0] == null || dsIsUsluga.Tables[0].Rows.Count == 0 || Convert.ToBoolean(dsIsUsluga.Tables[0].Rows[0]["usluga"].ToString()) || !decimal.TryParse(dRow["kolicina"].ToString(), out kolicina) || kolicina == 0 || !Class.Postavke.automatski_zapisnik)
                            continue;

                        DataSet dsCurrent = null;
                        if (broj_zapisnika > 0)
                        {
                            dsCurrent = classSQL.select("select stara_cijena as mpc, pdv as porez from promjena_cijene_stavke where broj = '" + broj_zapisnika + "' and sifra = '" + dRow["sifra"] + "'", "promjena_cijene_stavke");
                        }

                        if (dsCurrent == null)
                        {
                            dsCurrent = classSQL.select("select sifra, id_skladiste, porez, vpc, ROUND(vpc * (1 zbroj (coalesce(replace(porez, ',','.')::numeric, 0) zbroj coalesce(replace(porez_potrosnja, ',','.')::numeric, 0)) /100), 2) as mpc from roba_prodaja where id_skladiste = '" + dRow["id_skladiste"].ToString() + "' and sifra = '" + dRow["sifra"] + "'", "roba");

                            string sql = string.Format("select iznos from povratna_naknada where sifra = '{0}';", dRow["sifra"]);
                            DataSet dsPovNak = classSQL.select(sql, "povratna_naknada");

                            if (dsPovNak != null && dsPovNak.Tables.Count > 0 && dsPovNak.Tables[0] != null && dsPovNak.Tables[0].Rows.Count > 0)
                            {
                                decimal.TryParse(dsPovNak.Tables[0].Rows[0]["iznos"].ToString(), out pov_nak);
                                if (!Class.Postavke.koristi_povratnu_naknadu)
                                    pov_nak = 0;
                            }
                        }

                        if (dsCurrent != null && dsCurrent.Tables.Count > 0 && dsCurrent.Tables[0] != null && dsCurrent.Tables[0].Rows.Count > 0)
                        {
                            if (Math.Round(Convert.ToDecimal(dRow["mpc"]), 2, MidpointRounding.AwayFromZero) != (Math.Round(Convert.ToDecimal(dsCurrent.Tables[0].Rows[0]["mpc"]), 2, MidpointRounding.AwayFromZero) + pov_nak))
                            {
                                if (id_skladiste != "" && id_skladiste != dRow["id_skladiste"].ToString() && this._dtStavke != null && this._dtStavke.Rows.Count > 0)
                                {
                                    ZapisnikSpremi(this.broj_zapisnika, id_skladiste, datum, nap);
                                    this.broj_zapisnika = brojPromjene();
                                    GenerirajTablicuZaStavke(true);

                                    _kreiraniZapisnik = true;
                                }

                                DataRow drRow = this._dtStavke.NewRow();

                                drRow["br"] = broj_zapisnika;
                                drRow["sifra"] = dRow["sifra"];
                                drRow["stara_cijena"] = dsCurrent.Tables[0].Rows[0]["mpc"];
                                drRow["postotak"] = ((Convert.ToDecimal(dRow["mpc"]) / Convert.ToDecimal(dsCurrent.Tables[0].Rows[0]["mpc"]) - 1) * 100);
                                drRow["nova_cijena"] = dRow["mpc"];
                                drRow["pdv"] = dsCurrent.Tables[0].Rows[0]["porez"];
                                drRow["iznos"] = Convert.ToDecimal(dRow["mpc"]) / Convert.ToDecimal(dsCurrent.Tables[0].Rows[0]["mpc"]);
                                drRow["kolicina"] = dRow["kolicina"];
                                drRow["id_stavka"] = (dRow["id_stavka"] != null ? dRow["id_stavka"].ToString() : "");

                                this._dtStavke.Rows.Add(drRow);
                            }
                        }

                        id_skladiste = dRow["id_skladiste"].ToString();
                    }

                    if (_dtStavke != null && _dtStavke.Rows.Count > 0)
                    {
                        ZapisnikSpremi(this.broj_zapisnika, id_skladiste, datum, nap);
                        _kreiraniZapisnik = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ZapisnikPromjeneCijene(DataGridView dgv)
        {
            try
            {
                GenerirajTablicuZaStavke();

                foreach (DataGridViewRow dRow in dgv.Rows)
                {
                    DataRow dtRow = dtStavke.NewRow();
                    dtRow["br"] = dRow.Cells["br"].Value.ToString();
                    dtRow["sifra"] = dRow.Cells["sifra"].Value.ToString();
                    dtRow["stara_cijena"] = dRow.Cells["stara_cijena"].Value.ToString();
                    dtRow["postotak"] = dRow.Cells["postotak"].Value.ToString();
                    dtRow["nova_cijena"] = dRow.Cells["nova_cijena"].Value.ToString();
                    dtRow["pdv"] = dRow.Cells["pdv"].Value.ToString();
                    dtRow["iznos"] = dRow.Cells["iznos"].Value.ToString();
                    dtRow["kolicina"] = dRow.Cells["kolicina"].Value.ToString();
                    dtRow["id_stavka"] = (dRow.Cells["id_stavka"].Value != null ? dRow.Cells["id_stavka"].Value.ToString() : "");

                    this._dtStavke.Rows.Add(dtRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GenerirajStavkeZaZapisnik(string sifra, decimal nova_cijena)
        {
            try
            {
                GenerirajTablicuZaStavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerirajTablicuZaStavke(bool b = false)
        {
            try
            {
                if (this._dtStavke == null || b)
                {
                    this._dtStavke = null;
                    this._dtStavke = new DataTable();

                    this._dtStavke = new DataTable();
                    _dtStavke.Columns.Add("br");
                    _dtStavke.Columns.Add("sifra");
                    _dtStavke.Columns.Add("stara_cijena");
                    _dtStavke.Columns.Add("postotak");
                    _dtStavke.Columns.Add("nova_cijena");
                    _dtStavke.Columns.Add("pdv");
                    _dtStavke.Columns.Add("iznos");
                    _dtStavke.Columns.Add("kolicina");
                    _dtStavke.Columns.Add("id_stavka");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// metoda sprema zapisnik u bazu podataka
        /// </summary>
        public void ZapisnikSpremi(string broj_zapisnika, string id_skladiste, DateTime datum, string napomena)
        {
            try
            {
                DataTable DTboll = classSQL.select("SELECT broj FROM promjena_cijene WHERE broj='" + broj_zapisnika + "'", "promjena_cijene").Tables[0];

                if (DTboll.Rows.Count == 0)
                {
                    broj_zapisnika = brojPromjene();

                    string s = "INSERT INTO promjena_cijene (broj,id_skladiste,date,id_izradio,napomena) VALUES " +
                    "(" +
                    "'" + broj_zapisnika + "'," +
                    "'" + id_skladiste + "'," +
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + napomena + "'" +
                    ")";

                    classSQL.insert(s);

                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Novi zapisnik o promjeni cijene br." + broj_zapisnika + "')");
                }
                else
                {
                    string s = "UPDATE promjena_cijene SET broj='" + broj_zapisnika + "'" +
                    ",id_skladiste='" + id_skladiste + "'" +
                    ",date='" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'" +
                    ",napomena='" + napomena + "' WHERE broj='" + broj_zapisnika + "'";

                    classSQL.update(s);

                    if (editiranjeAutomatskogZapisnika)
                    {
                        s = "delete from promjena_cijene_stavke where broj = '" + broj_zapisnika + "';";
                        classSQL.select(s, "promjena_cijena_stavke");
                    }
                    classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES " +
                    "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','Ažuriranje zapisnika o promjeni cijene br." + broj_zapisnika + "')");
                }

                foreach (DataRow dRow in this.dtStavke.Rows)
                {
                    double pdv = double.Parse(dRow["pdv"].ToString());
                    double stara = double.Parse(dRow["stara_cijena"].ToString());
                    double nova = double.Parse(dRow["nova_cijena"].ToString());

                    string StrStara = stara.ToString();
                    string StrNova = nova.ToString();

                    if (classSQL.remoteConnectionString == "")
                    {
                        StrStara = StrStara.Replace(",", ".");
                        StrNova = StrNova.Replace(",", ".");
                    }
                    else
                    {
                        StrStara = StrStara.Replace(".", ",");
                        StrNova = StrNova.Replace(".", ",");
                    }

                    if (editiranjeAutomatskogZapisnika)
                    {
                        string s1 = "INSERT INTO promjena_cijene_stavke (stara_cijena, nova_cijena, pdv, sifra, postotak, broj, kolicina) VALUES " +
                        "(" +
                        "'" + StrStara + "'," +
                        "'" + StrNova + "'," +
                        "'" + pdv.ToString() + "'," +
                        "'" + dRow["sifra"].ToString() + "'," +
                        "'" + dRow["postotak"].ToString().Substring(0, (dRow["postotak"].ToString().Length >= 20 ? 20 : dRow["postotak"].ToString().Length)) + "'," +
                        "'" + broj_zapisnika + "'," +
                        "'" + dRow["kolicina"].ToString().Substring(0, (dRow["kolicina"].ToString().Length >= 10 ? 10 : dRow["kolicina"].ToString().Length)) + "'" +
                        ")";

                        classSQL.insert(s1);
                    }
                    else
                    {
                        if (dRow["id_stavka"].ToString() == "")
                        {
                            string s1 = "INSERT INTO promjena_cijene_stavke (stara_cijena, nova_cijena, pdv, sifra, postotak, broj, kolicina) VALUES " +
                            "(" +
                            "'" + StrStara + "'," +
                            "'" + StrNova + "'," +
                            "'" + pdv.ToString() + "'," +
                            "'" + dRow["sifra"].ToString() + "'," +
                            "'" + dRow["postotak"].ToString().Substring(0, (dRow["postotak"].ToString().Length >= 20 ? 20 : dRow["postotak"].ToString().Length)) + "'," +
                            "'" + broj_zapisnika + "'," +
                            "'" + dRow["kolicina"].ToString().Substring(0, (dRow["kolicina"].ToString().Length >= 10 ? 10 : dRow["kolicina"].ToString().Length)) + "'" +
                            ")";

                            classSQL.insert(s1);
                        }
                        else
                        {
                            string s2 = "UPDATE promjena_cijene_stavke SET stara_cijena=" +
                            "'" + StrStara + "'," +
                            " nova_cijena='" + StrNova + "'," +
                            " pdv='" + pdv.ToString() + "'," +
                            " sifra='" + dRow["sifra"].ToString() + "'," +
                            " postotak='" + dRow["postotak"].ToString().Substring(0, (dRow["postotak"].ToString().Length >= 20 ? 20 : dRow["postotak"].ToString().Length)) + "'," +
                            " kolicina='" + dRow["kolicina"].ToString().Substring(0, (dRow["kolicina"].ToString().Length >= 10 ? 10 : dRow["kolicina"].ToString().Length)) + "' WHERE id_stavka = '" + dRow["id_stavka"].ToString() + "'";

                            classSQL.update(s2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// izracunava novi broj zapisnika
        /// </summary>
        /// <returns>vraca broj zapisnika</returns>
        public string brojPromjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM promjena_cijene", "promjena_cijene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// Metoda kreira tablicu za stavke zapisnika u koju se upisuju stavke
        /// </summary>
        /// <returns>Vraca tablicu</returns>
        public static DataTable createTableForSale()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("sifra");
                dt.Columns.Add("mpc");
                dt.Columns.Add("kolicina");
                dt.Columns.Add("id_skladiste");
                dt.Columns.Add("id_stavka");

                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}