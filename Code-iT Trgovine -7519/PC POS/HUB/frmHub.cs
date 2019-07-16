using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PCPOS.HUB
{
    public partial class frmHub : Form
    {
        public frmHub()
        {
            InitializeComponent();
        }

        public List<string> brojevi_fak { get; set; }
        private string HubTxt = "";
        private DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka WHERE id='1'", "podaci_tvrtka").Tables[0];

        //*****************VARIJAVLE ZA HUB 300***********************
        private string Vrsta_naloga_u_datoteci = "1";

        private string Izvor_dokumenta = "300";
        private string Rezerva300 = "";
        private string Tip_sloga300 = "300";
        //*******************************************************

        //*****************VARIJAVLE ZA HUB 301***********************
        private string IBAN_platitelja = "";

        private string Oznaka_valute_placanja = "HRK";
        private string Racun_naknade = ""; //prazno
        private string Oznaka_valute_naknade = ""; //ako je prazno podrazumijeva se da je valuta kn
        private string Ukupan_broj_naloga_u_sljedećoj_grupi_slogovi309 = "0";
        private string Ukupan_iznos_naloga_u_sljedecoj_grupi_slogovi309 = "0";
        private string Datum_izvrsenja_naloga = DateTime.Now.ToString("yyyyMMdd");
        private string Rezerva301 = " ";
        private string Tip_sloga301 = "301";
        //*******************************************************

        //*****************VARIJAVLE ZA HUB 309***********************
        private string IBAN_ili_racun_primatelja;

        private string Naziv_primatelja; //obavezno za vrstu naloga 2 i 3;
        private string Adresa_primatelja; //obavezno za vrstu naloga 2 i 3;
        private string Sjediste_primatelja; //obavezno za vrstu naloga 2 i 3;
        private string Sifra_zemlje_primatelja; //obavezno za vrstu naloga 2 i 3;
        private string Broj_modela_platitelja;
        private string Poziv_na_broj_platitelja;
        private string Sifra_namjene;
        private string Opis_placanja;
        private string Iznos;
        private string Broj_modela_primatelja;
        private string Poziv_na_broj_primatelja;
        private string BIC_SWIFTadresa;
        private string Naziv_banke_primatelja;
        private string Adresa_banke_primatelja;
        private string Sjediste_banke_primatelja;
        private string sifra_zemlje_banke_primatelja;
        private string Vrsta_strane_osobe;
        private string Valuta_pokrica;
        private string Troskovna_opcija;
        private string Oznaka_hitnosti;
        private string Rezerva309;
        private string Tip_sloga309;
        //*******************************************************

        //*****************VARIJAVLE ZA HUB 399***********************
        private string Rezerva399 = "1";

        private string Tip_sloga399 = "399";
        //*******************************************************

        private void frmHub_Load(object sender, EventArgs e)
        {
            PopuniPadatke();
        }

        private void PopuniPadatke()
        {
            try
            {
                string[] podatak = brojevi_fak[0].Split(';');
                string query = "SELECT * FROM ulazna_faktura WHERE ulazna_faktura.broj='" + podatak[0] + "' AND ulazna_faktura.godina='" + podatak[1] + "'";
                DataTable DTufa = classSQL.select(query, "dt").Tables[0];

                //******************************300**********************************
                Vrsta_naloga_u_datoteci = DTufa.Rows[0]["vrsta_naloga"].ToString();
                Izvor_dokumenta = DTufa.Rows[0]["izvor_dokumenta"].ToString();
                Tip_sloga300 = "300";

                //******************************301**********************************
                decimal suma = 0, bb = 0;
                for (int i = 0; i < DTufa.Rows.Count; i++)
                {
                    decimal.TryParse(DTufa.Rows[i]["iznos"].ToString(), out bb);
                    suma = suma + bb;
                }
                IBAN_platitelja = DTufa.Rows[0]["iban_platitelja"].ToString();
                Oznaka_valute_placanja = DTufa.Rows[0]["valuta"].ToString() == "" ? "HRK" : DTufa.Rows[0]["valuta"].ToString();
                Racun_naknade = DTufa.Rows[0]["iban_platitelja"].ToString();
                Oznaka_valute_naknade = DTufa.Rows[0]["valuta"].ToString() == "" ? "HRK" : DTufa.Rows[0]["valuta"].ToString();
                Ukupan_broj_naloga_u_sljedećoj_grupi_slogovi309 = DTufa.Rows.Count.ToString();
                Ukupan_iznos_naloga_u_sljedecoj_grupi_slogovi309 = suma.ToString("#0.00").Replace(",", "").Replace(".", "");
                DateTime dtd;
                DateTime.TryParse(DTufa.Rows[0]["datum_izvrsenja"].ToString(), out dtd);
                Datum_izvrsenja_naloga = dtd.ToString("yyyyMMdd");
                Rezerva301 = " ";
                Tip_sloga301 = "301";

                string HubTxt = Set300() + Set301();

                foreach (string broj in brojevi_fak)
                {
                    podatak = broj.Split(';');
                    query = "SELECT * FROM ulazna_faktura WHERE ulazna_faktura.broj='" + podatak[0] + "' AND ulazna_faktura.godina='" + podatak[1] + "'";
                    DTufa = classSQL.select(query, "dt").Tables[0];
                    //*******************************309**********************************
                    IBAN_ili_racun_primatelja = DTufa.Rows[0]["iban_primatelja"].ToString();
                    Naziv_primatelja = DTufa.Rows[0]["primatelj_naziv"].ToString();
                    Adresa_primatelja = DTufa.Rows[0]["primatelj_adresa"].ToString();
                    Sjediste_primatelja = DTufa.Rows[0]["primatelj_sjediste"].ToString();
                    Sifra_zemlje_primatelja = DTufa.Rows[0]["primatelj_sifra_zemlje"].ToString();
                    sifra_zemlje_banke_primatelja = DTufa.Rows[0]["primatelj_sifra_zemlje_banke"].ToString();
                    Broj_modela_platitelja = DTufa.Rows[0]["model_platitelja"].ToString().Replace(" ", "");
                    Poziv_na_broj_platitelja = DTufa.Rows[0]["poziv_na_broj_platitelja"].ToString();
                    Sifra_namjene = DTufa.Rows[0]["sifra_namjene"].ToString();
                    Opis_placanja = DTufa.Rows[0]["opis_placanja"].ToString();
                    Broj_modela_primatelja = DTufa.Rows[0]["model_primatelja"].ToString().Replace(" ", "");
                    decimal iz;
                    decimal.TryParse(DTufa.Rows[0]["iznos"].ToString(), out iz);
                    Iznos = iz.ToString("#0.00").Replace(",", "").Replace(".", "");
                    Poziv_na_broj_primatelja = DTufa.Rows[0]["poziv_na_broj_primatelja"].ToString();
                    BIC_SWIFTadresa = DTufa.Rows[0]["primatelj_swift"].ToString();
                    Naziv_banke_primatelja = DTufa.Rows[0]["primatelj_naziv_banke"].ToString();
                    Adresa_banke_primatelja = DTufa.Rows[0]["primatelj_adresa_banke"].ToString();
                    Sjediste_banke_primatelja = DTufa.Rows[0]["primatelj_sjediste_banke"].ToString();
                    sifra_zemlje_banke_primatelja = DTufa.Rows[0]["primatelj_sifra_zemlje_banke"].ToString();
                    Vrsta_strane_osobe = DTufa.Rows[0]["primatelj_vrste_strane_osobe"].ToString();
                    Valuta_pokrica = DTufa.Rows[0]["valuta_pokrica"].ToString() == "" ? "HRK" : DTufa.Rows[0]["valuta_pokrica"].ToString();
                    Troskovna_opcija = DTufa.Rows[0]["primatelj_troskovna_opcija"].ToString();//DTufa.Rows[0]["primatelj_sifra_zemlje_banke"].ToString();
                    Oznaka_hitnosti = DTufa.Rows[0]["oznaka_hitnosti"].ToString();
                    Rezerva309 = " ";
                    Tip_sloga309 = "309";

                    HubTxt += Set309();

                    classSQL.update("UPDATE ulazna_faktura SET hub_kreirano='1' WHERE ulazna_faktura.broj='" + podatak[0] + "' AND ulazna_faktura.godina='" + podatak[1] + "'");
                }

                //**********************************399*****************************************
                Rezerva399 = " ";
                Tip_sloga399 = "399";
                HubTxt += Set399();

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "All files (*.*)|*.*|hub3 files (*.hub3)|*.hub3";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    {
                        File.WriteAllText(saveFileDialog1.FileName, HubTxt, Encoding.GetEncoding(1250));
                        lblTekst.Text = "Datoteka je uspješno kreirana.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                lblTekst.Text = "Desila se greška.";
            }
        }

        private string Set300()
        {
            string vrati = "";
            vrati = VratiString(DateTime.Now.ToString("yyyyMMdd"), 8, "N");
            vrati += VratiString(Vrsta_naloga_u_datoteci, 1, "N");
            vrati += VratiString(Izvor_dokumenta, 3, "N");
            vrati += VratiString(Rezerva300, 985, "C");
            vrati += VratiString(Tip_sloga300, 3, "N");
            return vrati + "\r\n";
        }

        private string Set301()
        {
            string vrati = "";
            vrati = VratiString(IBAN_platitelja, 21, "C");
            vrati += VratiString(Oznaka_valute_placanja, 3, "C");
            vrati += VratiString(Racun_naknade, 21, "C");
            vrati += VratiString(Oznaka_valute_naknade, 3, "C");
            vrati += VratiString(Ukupan_broj_naloga_u_sljedećoj_grupi_slogovi309, 5, "N");
            vrati += VratiString(Ukupan_iznos_naloga_u_sljedecoj_grupi_slogovi309, 20, "N");
            vrati += VratiString(Datum_izvrsenja_naloga, 8, "N");
            vrati += VratiString(Rezerva301, 916, "C");
            vrati += VratiString(Tip_sloga301, 3, "N");
            return vrati + "\r\n";
        }

        private string Set309()
        {
            string vrati = "";
            vrati += VratiString(IBAN_ili_racun_primatelja, 34, "C");
            vrati += VratiString(Naziv_primatelja, 70, "C");
            vrati += VratiString(Adresa_primatelja, 35, "C");
            vrati += VratiString(Sjediste_primatelja, 35, "C");
            vrati += VratiString(Sifra_zemlje_primatelja, 3, "N");
            vrati += VratiString(Broj_modela_platitelja, 4, "C");
            vrati += VratiString(Poziv_na_broj_platitelja, 22, "C");
            vrati += VratiString(Sifra_namjene, 4, "C");
            vrati += VratiString(Opis_placanja, 140, "C");
            vrati += VratiString(Iznos, 15, "N");
            vrati += VratiString(Broj_modela_primatelja, 4, "C");
            vrati += VratiString(Poziv_na_broj_primatelja, 22, "C");
            vrati += VratiString(BIC_SWIFTadresa, 11, "N");
            vrati += VratiString(Naziv_banke_primatelja, 70, "C");
            vrati += VratiString(Adresa_banke_primatelja, 35, "C");
            vrati += VratiString(Sjediste_banke_primatelja, 35, "C");
            vrati += VratiString(sifra_zemlje_banke_primatelja, 3, "N");
            vrati += VratiString(Vrsta_strane_osobe, 1, "N");
            vrati += VratiString(Valuta_pokrica, 3, "C");
            vrati += VratiString(Troskovna_opcija, 1, "N");
            vrati += VratiString(Oznaka_hitnosti, 1, "N");
            vrati += VratiString(Rezerva309, 449, "C");
            vrati += VratiString(Tip_sloga309, 3, "N");
            return vrati + "\r\n";
        }

        private string Set399()
        {
            string vrati = "";
            vrati = VratiString(Rezerva399, 997, "C");
            vrati += VratiString(Tip_sloga399, 3, "N");
            return vrati + "\r\n";
        }

        private string VratiString(string val, int mjesta, string tip)
        {
            string vrijednost_za_vratiti = "";
            if (tip == "C")
            {
                for (int i = 0; i < mjesta; i++)
                {
                    if (val.Length > i)
                    {
                        vrijednost_za_vratiti += val[i].ToString();
                    }
                    else
                    {
                        vrijednost_za_vratiti += " ";
                    }
                }
            }
            else
            {
                int b = 0;
                for (int i = 0; i < mjesta; i++)
                {
                    if (i >= (mjesta - val.Length))
                    {
                        vrijednost_za_vratiti += val[b].ToString();
                        b++;
                    }
                    else
                    {
                        vrijednost_za_vratiti += "0";
                        b = 0;
                    }
                }
            }

            return vrijednost_za_vratiti;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}