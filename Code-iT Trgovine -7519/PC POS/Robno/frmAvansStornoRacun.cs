using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAvansStornoRacun : Form
    {
        public frmAvansStornoRacun()
        {
            InitializeComponent();
        }

        public string broj { get; set; }
        public string poslovnica { get; set; }
        public short godina { get; set; }

        private static DataTable DTtvrtka = classSQL.select_settings("SELECT * FROM podaci_tvrtka", "podaci_tvrtka").Tables[0];
        private static DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private static DataTable DToib = classSQL.select("SELECT oib from zaposlenici where id_zaposlenik='" +
            Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0];

        private static DataTable DTpdv = new DataTable();

        private void button1_Click(object sender, EventArgs e)
        {
            string broj;
            int godina;

            try
            {
                broj = Convert.ToInt64(textBox1.Text).ToString();
                godina = Convert.ToInt16(numGodina.Value);
            }
            catch
            {
                MessageBox.Show("Krivi broj avansa.", "Greška");
                return;
            }

            DataTable DT = classSQL.select("SELECT broj_avansa FROM avans_racun WHERE broj_avansa='" + broj + "' and godina_avansa= '" + godina + "' and poslovnica = '" + poslovnica + "'", "avans_racun").Tables[0];

            if (DT.Rows.Count != 0)
            {
                stornirajAvansHelper(broj, godina);
            }
            else
            {
                MessageBox.Show("U bazi ne postoji ovaj avans.", "Greška");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string br = BrojAvansa(Util.Korisno.GodinaKojaSeKoristiUbazi);
            int god = GodinaZadnjegAvansa();

            if (br != "" && god > 0)
            {
                stornirajAvansHelper(br, god);
            }
            else
            {
                MessageBox.Show("U bazi ne postoji niti jedan avans!");
            }
        }

        private string BrojAvansa(int godina)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_avansa AS integer)) FROM avans_racun where godina_avansa= '" + godina + "' and poslovnica = '" + poslovnica + "'", "avans_racun").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                return DSbr.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        private int GodinaZadnjegAvansa()
        {
            DataTable DSbr = classSQL.select("SELECT godina_avansa, MAX(CAST(broj_avansa AS integer)) FROM avans_racun where poslovnica = '" + poslovnica + "' GROUP BY godina_avansa", "avans_racun").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                return Convert.ToInt16(DSbr.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        private void stornirajAvansHelper(string brojStarogAvansa, int godinaStarogAvansa)
        {
            string sql = string.Format(@"select case when count(storno) > 0 then true else false end as storno
from avans_racun
where storno = (
	select id
	from avans_racun
	where broj_avansa = {0}
	and godina_avansa = {1}
	and poslovnica = {2}
)", brojStarogAvansa, godinaStarogAvansa, poslovnica);
            DataTable DSracun = classSQL.select(sql, "avans_racun").Tables[0];

            if (DSracun.Rows.Count < 1)
            {
                MessageBox.Show("U tablici nema avansa!");
                return;
            }

            if (Convert.ToBoolean(DSracun.Rows[0]["storno"]))
            {
                MessageBox.Show("Avans je već storniran!");
                this.ActiveControl = textBox1;
                textBox1.SelectAll();
            }
            else
            {
                StornirajAvans(brojStarogAvansa, godinaStarogAvansa);
                MessageBox.Show("Avans " + brojStarogAvansa + "-" + godinaStarogAvansa + " je storniran.");
                this.Close();
            }
        }

        private void StornirajAvans(string brojStarogAvansa, int godinaStarogAvansa)
        {
            //ubaci u racuni i vrati IznosGotovina, IznosKartica, ukupno i novi racun
            DataTable DTracun = classSQL.select("SELECT * FROM avans_racun WHERE broj_avansa='" + brojStarogAvansa + "' and godina_avansa = '" + godinaStarogAvansa + "' and poslovnica = '" + poslovnica + "'", "avans_racun").Tables[0];

            decimal _ukupno, _osnovica_var, _osnovica10, _porez_var, _nult_stp, _neoporezivo;

            decimal.TryParse(DTracun.Rows[0]["ukupno"].ToString(), out _ukupno);
            decimal.TryParse(DTracun.Rows[0]["osnovica_var"].ToString(), out _osnovica_var);
            decimal.TryParse(DTracun.Rows[0]["porez_var"].ToString(), out _porez_var);
            decimal.TryParse(DTracun.Rows[0]["osnovica10"].ToString(), out _osnovica10);
            decimal.TryParse(DTracun.Rows[0]["nult_stp"].ToString(), out _nult_stp);
            decimal.TryParse(DTracun.Rows[0]["neoporezivo"].ToString(), out _neoporezivo);

            DTracun.Rows[0]["ukupno"] = _ukupno * (-1);
            DTracun.Rows[0]["osnovica_var"] = _osnovica_var * (-1);
            DTracun.Rows[0]["porez_var"] = _porez_var * (-1);
            DTracun.Rows[0]["osnovica10"] = _osnovica10 * (-1);
            DTracun.Rows[0]["nult_stp"] = _nult_stp * (-1);
            DTracun.Rows[0]["neoporezivo"] = _neoporezivo * (-1);

            string zki = DTracun.Rows[0]["zki"].ToString();

            //priprema za fiskalizaciju
            DataTable DTnaknade = new DataTable();
            DataTable DTOstaliPor = new DataTable();
            bool pdv = DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1" ? true : false;
            string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";
            string iznososlobpdv = "";
            string iznos_marza = "";

            double Porez_potrosnja_sve = 0;

            string[] porezNaPotrosnju = setPorezNaPotrosnju();

            dodajKoloneDTpdv();
            DTpdv.Clear();
            //priprema za fiskalizaciju

            double osnovica_sve = 0;
            double osnovicaVar = Convert.ToDouble(DTracun.Rows[0]["osnovica_var"].ToString());
            double porezVar = Convert.ToDouble(DTracun.Rows[0]["porez_var"].ToString());
            double osnovica10 = Convert.ToDouble(DTracun.Rows[0]["osnovica10"].ToString());
            double porez10 = osnovica10 * .1;

            if (osnovica10 != 0) dodajPDV(10.00, Math.Round(osnovica10, 2));
            if (osnovicaVar != 0) dodajPDV(Math.Round(porezVar / osnovicaVar, 2) * 100, Math.Round(osnovicaVar, 2));

            osnovica_sve += osnovica10 + osnovicaVar;

            porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
            porezNaPotrosnju[1] = Convert.ToString(osnovica_sve);
            porezNaPotrosnju[2] = Convert.ToString(Porez_potrosnja_sve);

            DataTable DTnp = classSQL.select("SELECT naziv_placanja FROM nacin_placanja where id_placanje='" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'", "avans_racun").Tables[0];

            string npId = DTnp.Rows.Count > 0 ? DTnp.Rows[0][0].ToString() : "";
            string np = Util.Korisno.VratiNacinPlacanja(npId.ToLower());

            //datum novog avansa
            DateTime datum = DateTime.Now;

            int godinaNovogAvansa = DateTime.Now.Year;

            string brojNovogAvansa = BrojAvansa(godinaNovogAvansa);

            if (brojNovogAvansa != "")
            {
                brojNovogAvansa = (Convert.ToInt64(brojNovogAvansa) + 1).ToString();
            }
            else
            {
                //MessageBox.Show("U bazi ne postoji niti jedan avans!");
                //return;
                brojNovogAvansa = "1";
            }

            string[] fiskalizacija = new string[3];
            fiskalizacija[0] = "";
            fiskalizacija[1] = "";
            fiskalizacija[2] = "";

            InsertUpdate(DTracun, brojNovogAvansa, brojStarogAvansa, fiskalizacija, datum, godinaStarogAvansa, godinaNovogAvansa);

            //ako ima zki onda fiskaliziraj
            //if (zki != "")
            //{
            //    fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
            //        DTtvrtka.Rows[0]["oib"].ToString(),
            //        oib,
            //        datum,
            //        pdv,
            //        Convert.ToInt32(brojNovogAvansa),
            //        DTpdv,
            //        porezNaPotrosnju,
            //        DTOstaliPor,
            //        iznososlobpdv,
            //        iznos_marza,
            //        DTnaknade,
            //        Convert.ToDecimal(DTracun.Rows[0]["ukupno"].ToString()),//.ToString().Replace(",", ".")
            //        np,
            //        false,
            //        "avans"
            //        );

            //    //ažuriraj avans sa zki i jir
            //    string sql = "UPDATE avans_racun SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
            //        " WHERE broj_avansa='" + brojNovogAvansa + "'" + VratiSqlGodinaAvans(" AND ", godinaNovogAvansa);
            //    provjera_sql(classSQL.update(sql));

            //    provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES" +
            //        "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
            //        "'Storno avansa broj " + brojStarogAvansa + ":" + godinaStarogAvansa + " (fiskaliziran), novi avans broj: " + brojNovogAvansa + ":" + godinaNovogAvansa + " (fiskaliziran)')"));
            //}
            //else
            //{
            provjera_sql(classSQL.insert("INSERT INTO aktivnost_zaposlenici (id_zaposlenik,datum,radnja) VALUES" +
                "('" + Properties.Settings.Default.id_zaposlenik + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                "'Storno avansa broj " + brojStarogAvansa + ":" + godinaStarogAvansa + " (nije fiskaliziran), novi avans broj: " + brojNovogAvansa + ":" + godinaNovogAvansa + " (nije fiskaliziran)')"));
            //}
        }

        private void InsertUpdate(DataTable DTracun, string noviAvans, string stariBroj, string[] fiskalizacija, DateTime datum, int godinaStarogAvansa, int godinaNovogAvansa)
        {
            string sql = "INSERT INTO avans_racun (broj_avansa, poslovnica, dat_dok, dat_knj, datum_valute, id_zaposlenik, id_zaposlenik_izradio, model" +
                ",id_nacin_placanja, id_valuta, opis, id_vd, godina_avansa, ziro, ukupno, nult_stp, neoporezivo, " +
                "osnovica10, osnovica_var, porez_var, id_pdv, id_partner, jir, zki, storno, artikl) VALUES " +
                " (" +
                 " '" + noviAvans + "'," +
                 " '" + poslovnica + "'," +
                " '" + (dtpDatum.Checked ? dtpDatum.Value.ToString("yyyy-MM-dd H:mm:ss") : datum.ToString("yyyy-MM-dd H:mm:ss")) + "'," +
                " '" + (dtpDatumDvo.Checked ? dtpDatumDvo.Value.ToString("yyyy-MM-dd H:mm:ss") : DTracun.Rows[0]["dat_knj"].ToString()) + "'," +
                " '" + (dtpDatumValute.Checked ? dtpDatumValute.Value.ToString("yyyy-MM-dd H:mm:ss") : DTracun.Rows[0]["datum_valute"].ToString()) + "'," +
                " '" + "0" + "'," +
                " '" + Properties.Settings.Default.id_zaposlenik + "'," +
                " '" + DTracun.Rows[0]["model"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_nacin_placanja"].ToString() + "'," +
                " '" + DTracun.Rows[0]["id_valuta"].ToString() + "'," +
                " '" + DTracun.Rows[0]["opis"].ToString() + "'," +
                " '2'," +
                " '" + godinaNovogAvansa + "'," +
                " '" + DTracun.Rows[0]["ziro"].ToString() + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["ukupno"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["nult_stp"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["neoporezivo"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["osnovica10"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["osnovica_var"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + Math.Round((Convert.ToDouble(DTracun.Rows[0]["porez_var"].ToString())), 2).ToString("#0.00").Replace(",", ".") + "'," +
                " '" + DTracun.Rows[0]["id_pdv"].ToString() + "', " +
                " '" + DTracun.Rows[0]["id_partner"].ToString() + "', " +
                " '" + fiskalizacija[0] + "', " +
                " '" + fiskalizacija[1] + "', " +
                " '" + DTracun.Rows[0]["id"].ToString() + "', " +
                " '" + DTracun.Rows[0]["artikl"].ToString() + "' " +
                ")";

            provjera_sql(classSQL.insert(sql));

            //string sqlStorno = "UPDATE avans_racun SET storno='NE' WHERE broj_avansa='" + stariBroj + "'" + VratiSqlGodinaAvans(" AND ", godinaStarogAvansa);
            //classSQL.update(sqlStorno);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        #region fiskalizacija helper

        /// <summary>
        /// Dodaje kolone tablici DTpdv ako još nisu dodane
        /// </summary>
        private static void dodajKoloneDTpdv()
        {
            if (DTpdv.Columns["stopa"] == null)
            {
                DTpdv.Columns.Add("stopa");
                DTpdv.Columns.Add("osnovica");
                DTpdv.Columns.Add("iznos");
            }
        }

        /// <summary>
        /// dodaje stopu PDV-a i iznos u tablicu DTpdv ako ne postoji stopa;
        /// ako postoji zbraja s postojećim iznosom
        /// </summary>
        /// <param name="stopa"></param>
        /// <param name="iznos"></param>
        private static void dodajPDV(double stopa, double iznos)
        {
            DataRow[] dataROW = DTpdv.Select("stopa = '" + stopa.ToString() + "'");
            DataRow RowPdv;

            if (dataROW.Count() == 0)
            {
                RowPdv = DTpdv.NewRow();
                RowPdv["stopa"] = Math.Round(stopa, 2).ToString("#0.00");
                RowPdv["iznos"] = Math.Round(iznos * stopa / 100, 2).ToString("#0.00");
                RowPdv["osnovica"] = iznos.ToString("#0.00");
                DTpdv.Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["iznos"] = (Math.Round(Convert.ToDouble(dataROW[0]["iznos"].ToString()), 2) + Math.Round(iznos * stopa / 100, 2)).ToString("#0.00");
                dataROW[0]["osnovica"] = (Math.Round(Convert.ToDouble(dataROW[0]["osnovica"].ToString()), 2) + Math.Round(iznos, 2)).ToString("#0.00");
            }
        }

        /// <summary>
        /// postavlja porez_na_potrosnju na empty string
        /// </summary>
        /// <returns></returns>
        private static string[] setPorezNaPotrosnju()
        {
            string[] porez_na_potrosnju = new string[3];
            porez_na_potrosnju[0] = "";
            porez_na_potrosnju[1] = "";
            porez_na_potrosnju[2] = "";

            return porez_na_potrosnju;
        }

        #endregion fiskalizacija helper

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmAvansStorno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frmAvansStorno_Load(object sender, EventArgs e)
        {
            numGodina.Minimum = Convert.ToInt16(DateTime.Now.Year - 30);
            numGodina.Maximum = Convert.ToInt16(DateTime.Now.Year + 30);
            numGodina.Value = Convert.ToInt16(DateTime.Now.Year.ToString());
            dtpDatumDvo.Value = DateTime.Now;
            dtpDatum.Value = dtpDatumDvo.Value;
            if (broj != null)
                textBox1.Text = broj;

            if (poslovnica == null)
                poslovnica = Class.Postavke.id_default_ducan.ToString();

            if (godina > 0)
                numGodina.Value = godina;

            if (broj != null && poslovnica != null && godina > 0)
            {
                groupBox2.Enabled = false;
            }
        }
    }
}