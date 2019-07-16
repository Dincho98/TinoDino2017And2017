using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCPOS.Entities;
using PCPOS.Report.KnjigaPrometa;

namespace PCPOS.Resort
{
    public partial class FrmKnjigaPrometa : Form
    {
        public FrmKnjigaPrometa()
        {
            InitializeComponent();
        }

        //Globals
        string treciDioRacunaSlucajMaloprodaje;
        string treciDioRacunaSlucajFakture;

        private void FrmKnjigaPrometa_Load(object sender, EventArgs e)
        {
            //Preuzimanje onog što je korisnik postavio u POSTAVKAMA za maloprodaju i fakturu
            DataSet ds = classSQL.select_settings("SELECT default_blagajna, naplatni_uredaj_faktura FROM postavke", "postavke");
            treciDioRacunaSlucajMaloprodaje = ds.Tables[0].Rows[0]["default_blagajna"].ToString();
            treciDioRacunaSlucajFakture = ds.Tables[0].Rows[0]["naplatni_uredaj_faktura"].ToString();
            //Stavljanje SVIH podataka u dataGridView
                //-UKINUTO zbog toga što kada je puno zapisa, korisnik treba čekati da bi se forma uopće otvorila.
                //Zamišljeno je da korisnik pritisne svi zapisi ili traži (prema određenom datumu) da bi se zapisi pokazali.
            //PreuzmiIzMaloprodaje();
            //PreuzmiIzFakture();
        }

        //Funkcija za generiranje rednih brojeva
        private string GenerirajBroj()
        {
            int broj = dataGridView1.Rows.Count + 1;
            return broj.ToString();
        }

        #region Maloprodaja
        //Postoje 2 funkcije PreuzmiIzMaloprodaje i FiltrirajIzMaloprodaje. Razlog je BRZINA.
        private void PreuzmiIzMaloprodaje()
        {
            DataTable DTRacuni = Global.Database.GetRacuni();
            //Potrebne varijable
            string redniBroj;
            string brojRac;
            string imePoslovnice; // potrebno preuzeti ime_ducana iz tablice ducan
            string datum;
            decimal iznos;
            string nacinPlacanja;

            foreach (DataRow row in DTRacuni.Rows)
            {
                redniBroj = GenerirajBroj();
                brojRac = row["broj_racuna"].ToString();
                imePoslovnice = Global.Database.GetImePoslovnice(row["id_ducan"].ToString()); // Preuzima se ime ducana
                string brojRacuna = $@"{brojRac}/{imePoslovnice}/{treciDioRacunaSlucajMaloprodaje}";
                datum = row["datum_racuna"].ToString();
                iznos = (decimal)row["ukupno"];
                nacinPlacanja = row["nacin_placanja"].ToString();
                this.dataGridView1.Rows.Add(redniBroj, brojRacuna, datum, iznos, nacinPlacanja);
            }
        }

        private void FiltrirajIzMaloprodaje()
        {
            DataTable DTRacuni = Global.Database.GetRacuniDate(dateTimePickerDatumPocetka.Value.ToString("yyyy-MM-dd"), dateTimePickerDatumKraja.Value.ToString("yyyy-MM-dd"));
            //Potrebne varijable
            string redniBroj;
            string brojRac;
            string imePoslovnice; // potrebno preuzeti ime_ducana iz tablice ducan
            string datum;
            decimal iznos;
            string nacinPlacanja;

            foreach (DataRow row in DTRacuni.Rows)
            {
                redniBroj = GenerirajBroj();
                brojRac = row["broj_racuna"].ToString();
                imePoslovnice = Global.Database.GetImePoslovnice(row["id_ducan"].ToString()); // Preuzima se ime ducana
                string brojRacuna = $@"{brojRac}/{imePoslovnice}/{treciDioRacunaSlucajMaloprodaje}";
                datum = row["datum_racuna"].ToString();
                iznos = (decimal)row["ukupno"];
                nacinPlacanja = row["nacin_placanja"].ToString();
                this.dataGridView1.Rows.Add(redniBroj, brojRacuna, datum, iznos, nacinPlacanja);
            }
        }
        #endregion

        #region Maloprodaja
        //Postoje 2 funkcije PreuzmiIzFakture i FiltrirajIzFakture. Razlog je BRZINA.
        private void PreuzmiIzFakture()
        {
            DataTable DTFakture = Global.Database.GetFakture();
            //Potrebne varijable
            string redniBroj;
            string brojRac;
            string imePoslovnice; // preuzima se iz tablice ducan, atribut 
            string datum;
            decimal iznos;
            string nacinPlacanja; // preuzima se iz tablice nacin_placanja prema idu 

            foreach (DataRow row in DTFakture.Rows)
            {
                redniBroj = GenerirajBroj();
                brojRac = row["broj_fakture"].ToString();
                imePoslovnice = Global.Database.GetImePoslovnice(row["id_ducan"].ToString());
                string brojRacuna = $@"{brojRac}/{imePoslovnice}/{treciDioRacunaSlucajFakture}";
                datum = row["date"].ToString();
                iznos = (decimal)row["ukupno"];
                nacinPlacanja = Global.Database.GetNacinPlacanja(Int32.Parse(row["id_nacin_placanja"].ToString()));
                this.dataGridView1.Rows.Add(redniBroj, brojRacuna, datum, iznos, nacinPlacanja);
            }
        }

        private void FiltrirajIzFakture()
        {
            DataTable DTRacuni = Global.Database.GetFaktureDate(dateTimePickerDatumPocetka.Value.ToString("yyyy-MM-dd"), dateTimePickerDatumKraja.Value.ToString("yyyy-MM-dd"));
            //Potrebne varijable
            string redniBroj;
            string brojRac;
            string imePoslovnice; // potrebno preuzeti ime_ducana iz tablice ducan
            string datum;
            decimal iznos;
            string nacinPlacanja;

            foreach (DataRow row in DTRacuni.Rows)
            {
                redniBroj = GenerirajBroj();
                brojRac = row["broj_fakture"].ToString();
                imePoslovnice = Global.Database.GetImePoslovnice(row["id_ducan"].ToString()); // Preuzima se ime ducana
                string brojRacuna = $@"{brojRac}/{imePoslovnice}/{treciDioRacunaSlucajFakture}";
                datum = row["date"].ToString();
                iznos = (decimal)row["ukupno"];
                nacinPlacanja = Global.Database.GetNacinPlacanja(Int32.Parse(row["id_nacin_placanja"].ToString()));
                this.dataGridView1.Rows.Add(redniBroj, brojRacuna, datum, iznos, nacinPlacanja);
            }
        }
        #endregion


        private void buttonTrazi_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            FiltrirajIzMaloprodaje();
            FiltrirajIzFakture();
        }

        private void buttonSve_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            PreuzmiIzMaloprodaje();
            PreuzmiIzFakture();
        }

        private void buttonIspisi_Click(object sender, EventArgs e)
        {
            //Brojimo trenutni prikaz redova u tablici
            int brojRedova = dataGridView1.Rows.Count;
            //Stvaramo polje objekata
            ZapisKnjigePrometa[] zapisi = new ZapisKnjigePrometa[brojRedova];
            
            //Punjenje polja podacima iz dataGridViewa
            for(int i = 0; i < brojRedova; i++)
            {
                zapisi[i] = new ZapisKnjigePrometa(); // Zauzimamo prostor u memoriji za object odabranZapis[i]
                zapisi[i].redniBroj = dataGridView1["broj", i].Value.ToString();
                zapisi[i].brojRacuna = dataGridView1["brojRacuna", i].Value.ToString();
                zapisi[i].datum = dataGridView1["datum", i].Value.ToString();
                zapisi[i].iznos= dataGridView1["iznos", i].Value.ToString();
                zapisi[i].nacinPlacanja= dataGridView1["nacinPlacanja", i].Value.ToString();
            }

            //Stvaranje nove forme i proslijeđivanje popunjenog polja kao argument
            FrmKnjigaPrometaReport form = new FrmKnjigaPrometaReport(zapisi);
            form.Show();
        }

    }
}

