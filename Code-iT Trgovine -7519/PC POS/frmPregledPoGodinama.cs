using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPregledPoGodinama : Form
    {
        public frmPregledPoGodinama()
        {
            InitializeComponent();
        }

        private void frmPregledPoGodinama_Load(object sender, EventArgs e)
        {
            ProvjeraPostojanjaBaze();
        }

        public void UcitajPrethodneGodine()
        {
            PCPOS.Util.classFukcijeZaUpravljanjeBazom Fbaza = new Util.classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");
            List<string> godine = Fbaza.UzmiSveBazeIzPostgressa();
            Fbaza.UzmiGodinuKojaSeKoristi();

            if (godine.Count > 0)
                classSQL.delete("DELETE FROM pregled_kroz_godine;");

            foreach (string godina in godine)
            {
                if (godina.Length == 7)
                {
                    string StringZaGodinu = classSQL.remoteConnectionString;
                    string[] ArrZaGodinu = StringZaGodinu.Split(';');
                    ArrZaGodinu[4] = ArrZaGodinu[4].Replace("Database=", "");
                    string naziv_baze = ArrZaGodinu[4];
                    if (godina.Remove(3, 4).ToLower() == naziv_baze.Remove(3, 4).ToLower())
                    {
                        UcitajPrethodneGodineIzBaze(godina, Fbaza);
                    }
                }
            }

            MessageBox.Show("Uspješno učitano");
        }

        protected void UcitajPrethodneGodineIzBaze(string baza, PCPOS.Util.classFukcijeZaUpravljanjeBazom Fbaza)
        {
            NpgsqlConnection remoteConnection = new NpgsqlConnection(classSQL.remoteConnectionString.Replace(Fbaza.UzmiBazuKojaSeKoristi(), baza));

            string sql = @"SELECT * FROM
                            (
		                            SELECT
		                            fakture.broj_fakture AS broj,
		                            faktura_stavke.sifra,
		                            fakture.id_odrediste as kupac,
		                            date as datum,'FAKTURA' as dokumenat,
		                            ROUND(CAST(REPLACE(kolicina,',','.') AS NUMERIC),3) as kolicina,
		                            ROUND(vpc*(1+(CAST(REPLACE(porez,',','.') AS NUMERIC)/100)),3) as cijena,
		                            ROUND(CAST(REPLACE(rabat,',','.') AS NUMERIC),3) as rabat,
		                            fakture.id_ducan,
		                            fakture.id_kasa,
                                    fakture.napomena as opis
		                            FROM faktura_stavke
		                            LEFT JOIN fakture
		                            ON fakture.broj_fakture = faktura_stavke.broj_fakture
		                            AND fakture.id_ducan=faktura_stavke.id_ducan
		                            AND fakture.id_kasa=faktura_stavke.id_kasa
	                            UNION ALL
		                            SELECT
		                            CAST(racuni.broj_racuna AS bigint) AS broj,
		                            racun_stavke.sifra_robe as sifra,
		                            id_kupac as kupac,datum_racuna as datum,
		                            'RACUNI' as dokumenat,
		                            ROUND(CAST(REPLACE(kolicina,',','.') AS NUMERIC),3) as kolicina,
		                            ROUND(CAST(mpc AS NUMERIC),3) as cijena,
		                            ROUND(CAST(REPLACE(rabat,',','.') AS NUMERIC),3) as rabat,
		                            racuni.id_ducan,
		                            racuni.id_kasa,
                                    '' as opis
		                            FROM racun_stavke
		                            LEFT JOIN racuni
		                            ON racuni.broj_racuna = racun_stavke.broj_racuna
		                            AND racuni.id_ducan=racun_stavke.id_ducan
		                            AND racuni.id_kasa=racun_stavke.id_kasa
                            ) dokumenat;";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, remoteConnection);
            DataSet DS = new DataSet();
            da.Fill(DS);
            DataTable DTdokumenti = DS.Tables[0];
            remoteConnection.Close();

            StringBuilder query = new StringBuilder();
            string temp_query = "";
            DateTime datum;
            foreach (DataRow r in DTdokumenti.Rows)
            {
                DateTime.TryParse(r["datum"].ToString(), out datum);

                int kupac;
                int.TryParse(r["kupac"].ToString(), out kupac);

                temp_query = @"INSERT INTO pregled_kroz_godine " +
                    "(broj,sifra,kupac,datum,dokumenat,kolicina,cijena,rabat,id_ducan,id_kasa,opis)" +
                    " VALUES (" +
                    "'" + r["broj"].ToString() + "'," +
                    "'" + r["sifra"].ToString() + "'," +
                    "'" + kupac.ToString() + "'," +
                    "'" + datum.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + r["dokumenat"].ToString() + "'," +
                    "'" + r["kolicina"].ToString().Replace(",", ".") + "'," +
                    "'" + r["cijena"].ToString().Replace(",", ".") + "'," +
                    "'" + r["rabat"].ToString().Replace(",", ".") + "'," +
                    "'" + r["id_ducan"].ToString() + "'," +
                    "'" + r["id_kasa"].ToString() + "','" + r["opis"].ToString() + "'); ";
                query.Append(temp_query);
            }
            classSQL.insert(query.ToString());
        }

        protected void ProvjeraPostojanjaBaze()
        {
            DataTable DTremote_cols = classSQL.select("select table_name, column_name, data_type, character_maximum_length from information_schema.columns", "coltypes").Tables[0];
            DataRow[] r = DTremote_cols.Select("table_name='pregled_kroz_godine'");
            if (r.Length == 0)
            {
                string sql = @"CREATE TABLE pregled_kroz_godine(
	                        id serial NOT NULL,
	                        broj bigint DEFAULT 0,
	                        sifra character varying(30),
	                        kupac bigint DEFAULT 0,
	                        datum timestamp without time zone,
	                        dokumenat character varying(30),
	                        kolicina numeric DEFAULT 0,
	                        cijena numeric DEFAULT 0,
	                        rabat numeric DEFAULT 0,
	                        id_ducan integer DEFAULT 0,
	                        id_kasa integer DEFAULT 0,
                            opis text,
	                        CONSTRAINT pregled_kroz_godine_primary_key PRIMARY KEY (id)
                        );";
                classSQL.insert(sql);
            }
        }

        private void btnTrazi_Click(object sender, EventArgs e)
        {
            string filter = "";
            if (txtPartnerIme.Text.Length > 0)
            {
                int id_partner; int.TryParse(txtPartnerIme.Text, out id_partner);
                string dodatak = "";
                if (id_partner != 0) { dodatak = "OR pregled_kroz_godine.kupac='" + id_partner + "'"; }

                filter += " AND (partners.ime_tvrtke  ~* '" + txtPartnerIme.Text + "'" + dodatak + ") ";
            }
            if (txtNazivArtikli.Text.Length > 0)
            {
                filter += " AND roba.naziv='" + txtNazivArtikli.Text + "' ";
            }
            if (txtSifraArtikla.Text.Length > 0)
            {
                filter += " AND (pregled_kroz_godine.sifra='" + txtSifraArtikla.Text + "') ";
            }

            string sql = @"SELECT pregled_kroz_godine.*,partners.ime_tvrtke,roba.naziv as naziv_robe FROM pregled_kroz_godine
                            LEFT JOIN partners ON partners.id_partner=pregled_kroz_godine.kupac
                            LEFT JOIN roba ON roba.sifra=pregled_kroz_godine.sifra
                            WHERE id IS NOT NULL " + filter + " ORDER BY datum DESC LIMIT 1000;";

            DataTable DT = classSQL.select(sql, "sql").Tables[0];
            dataGridView1.Rows.Clear();
            foreach (DataRow r in DT.Rows)
            {
                DateTime dat; DateTime.TryParse(r["datum"].ToString(), out dat);
                int broj; int.TryParse(r["broj"].ToString(), out broj);
                decimal kolicina, rabat, cijena;
                decimal.TryParse(r["kolicina"].ToString(), out kolicina);
                decimal.TryParse(r["rabat"].ToString(), out rabat);
                decimal.TryParse(r["cijena"].ToString(), out cijena);

                dataGridView1.Rows.Add(r["dokumenat"].ToString(),
                    r["ime_tvrtke"].ToString(),
                    dat,
                    broj,
                    r["naziv_robe"].ToString(),
                    kolicina,
                    rabat,
                    cijena,
                    r["opis"].ToString());
            }
        }

        private void btnProsleGosine_Click(object sender, EventArgs e)
        {
            classSQL.delete("DROP TABLE IF EXISTS pregled_kroz_godine;");
            ProvjeraPostojanjaBaze();
            UcitajPrethodneGodine();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                richTextBox1.Text = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["opis"].FormattedValue.ToString();
            }
        }
    }
}