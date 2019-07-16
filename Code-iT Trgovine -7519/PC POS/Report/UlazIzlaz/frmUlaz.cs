using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.UlazIzlaz
{
    public partial class frmUlaz : Form
    {
        public frmUlaz()
        {
            InitializeComponent();
        }

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string skladiste_odabir { get; set; }
        public string artikl { get; set; }
        public string ducan { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            //Sintetika_po_skladisnim_cijenama_sva_skladista();

            if (documenat == "USKLA")
            {
                Ulaz();
                //this.Text = ImeForme;
            }

            if (documenat == "ISKLA")
            {
                Izlaz();
                //this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private DataTable ds = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private DataTable dsskl = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracunskladnisne(DataTable MSu, DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista, DataTable DTradnal, DataTable DTpocsta, DataTable DTprimka)
        {
            decimal mpc_uk = 0;
            decimal id_skladiste_fak1 = 0;
            string ime_dokumenta1 = "";

            ++brojac;

            if (brojac > skladiste_brojac)
            {
                brojac = 0;
            }

            decimal msklad_ulaz1 = 0;
            string uslo_iz1 = "";
            string sifra = "";
            string broj = "";
            string dokumenit_medu_izl = "";

            for (int z = 0; z < MSu.Rows.Count; z++)
            {
                decimal vpc2 = Convert.ToDecimal(MSu.Rows[z]["vpc"].ToString().Replace(".", ","));
                decimal kol2 = Convert.ToDecimal(MSu.Rows[z]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_do2 = Convert.ToDecimal(MSu.Rows[z]["id_skladiste_do"].ToString());
                sifra = MSu.Rows[z]["sifra"].ToString();
                broj = MSu.Rows[z]["broj"].ToString();

                msklad_ulaz1 = kol2 + msklad_ulaz1;
                uslo_iz1 = "MEĐUSKLADIŠNICA ulaz ";
                dokumenit_medu_izl += broj + " ,";
            }
            if (dokumenit_medu_izl.Length > 2)
            {
                dokumenit_medu_izl = dokumenit_medu_izl.Remove(dokumenit_medu_izl.Length - 2);
            }

            decimal mpc1 = 0;
            decimal mpc_ra1 = 0;
            decimal kalk_mpc_uk = 0;
            string ime_dokumetna2 = "";
            string sifra1 = "";
            string broj1 = "";
            string dokumenti_kalkulacija = "";

            for (int y = 0; y < Kal.Rows.Count; y++)
            {
                decimal vpc4 = Convert.ToDecimal(Kal.Rows[y]["vpc"].ToString());
                decimal kol4 = Convert.ToDecimal(Kal.Rows[y]["kolicina"].ToString().Replace(".", ","));
                sifra1 = Kal.Rows[y]["sifra"].ToString();
                broj1 = Kal.Rows[y]["broj"].ToString();

                kalk_mpc_uk = kol4 + kalk_mpc_uk;
                ime_dokumetna2 = "KALKULACIJA";
                dokumenti_kalkulacija += broj1 + " ,";
            }
            if (dokumenti_kalkulacija.Length > 2)
            {
                dokumenti_kalkulacija = dokumenti_kalkulacija.Remove(dokumenti_kalkulacija.Length - 2);
            }

            decimal radnal_mpc_uk = 0;
            string ime_dokumenta4 = "";
            string sifra2 = "";
            string broj2 = "";
            string dokumnenti_radninalog = "";

            for (int j = 0; j < DTradnal.Rows.Count; j++)
            {
                decimal vpc6 = Convert.ToDecimal(DTradnal.Rows[j]["vpc"].ToString());
                decimal kol6 = Convert.ToDecimal(DTradnal.Rows[j]["kolicina"].ToString().Replace(".", ","));
                sifra2 = DTradnal.Rows[j]["sifra_robe"].ToString();
                broj2 = DTradnal.Rows[j]["broj"].ToString();

                radnal_mpc_uk = kol6 + radnal_mpc_uk;
                ime_dokumenta4 = "RADNI NALOG";
                dokumnenti_radninalog += broj2 + " ,";
            }
            if (dokumnenti_radninalog.Length > 2)
            {
                dokumnenti_radninalog = dokumnenti_radninalog.Remove(dokumnenti_radninalog.Length - 2);
            }

            decimal pocsta_mpc_uk = 0;
            string ime_dokumenta5 = "";
            string sifra3 = "";
            string broj3 = "";
            string dokumenti_pocst = "";
            for (int v = 0; v < DTpocsta.Rows.Count; v++)
            {
                decimal mpc7 = Convert.ToDecimal(DTpocsta.Rows[v]["mpc"].ToString());
                decimal kol7 = Convert.ToDecimal(DTpocsta.Rows[v]["kolicina"].ToString().Replace(".", ","));
                sifra3 = DTpocsta.Rows[v]["sifra_robe"].ToString();
                broj3 = DTpocsta.Rows[v]["broj"].ToString();

                pocsta_mpc_uk = kol7 + pocsta_mpc_uk;
                ime_dokumenta5 = "POCETNO STANJE";
                dokumenti_pocst += broj3 + " ,";
            }
            if (dokumenti_pocst.Length > 2)
            {
                dokumenti_pocst = dokumenti_pocst.Remove(dokumenti_pocst.Length - 2);
            }

            decimal primka_mpc_uk = 0;
            string ime_dokumenta7 = "";
            string sifra4 = "";
            string broj4 = "";

            //string value = DTprimka
            //List<string> arr1 = new List<string>();
            string dokumenti_primka = "";
            for (int v = 0; v < DTprimka.Rows.Count; v++)
            {
                decimal mpc8 = Convert.ToDecimal(DTprimka.Rows[v]["mpc"].ToString());
                decimal kol8 = Convert.ToDecimal(DTprimka.Rows[v]["kolicina"].ToString().Replace(".", ","));
                sifra4 = DTprimka.Rows[v]["sifra"].ToString();
                broj4 = DTprimka.Rows[v]["broj"].ToString();

                primka_mpc_uk = kol8 + primka_mpc_uk;
                ime_dokumenta7 = "PRIMKA";
                //arr1.Add(broj4);
                dokumenti_primka += broj4 + " ,";
            }
            if (dokumenti_primka.Length > 2)
            {
                dokumenti_primka = dokumenti_primka.Remove(dokumenti_primka.Length - 2);
            }

            DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();

            if (msklad_ulaz1 != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra;
                DTrow["string2"] = dokumenit_medu_izl;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = uslo_iz1.ToString();
                DTrow["string8"] = msklad_ulaz1.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = msklad_ulaz1.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (kalk_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra1;
                DTrow["string2"] = dokumenti_kalkulacija;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumetna2.ToString();
                DTrow["string8"] = kalk_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = kalk_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (radnal_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra2;
                DTrow["string2"] = dokumnenti_radninalog;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta4.ToString();
                DTrow["string8"] = radnal_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = radnal_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (pocsta_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra3;
                DTrow["string2"] = dokumenti_pocst;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta5.ToString();
                DTrow["string8"] = pocsta_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = pocsta_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (primka_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra4;
                DTrow["string2"] = dokumenti_primka;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta7.ToString();
                DTrow["string8"] = primka_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = primka_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            decimal stanje1 = 0;
            decimal stanje2 = 0;
            decimal stanje3 = 0;
            decimal stanje4 = radnal_mpc_uk;
            stanje2 = (0 - mpc_uk); //Fakture izlaz - stanje

            decimal sveukupno = 0;
            decimal zbroj = 0;
            decimal zbroj_ulaz = 0;
            decimal zbroj_izlaz = 0;

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);

            if (brojac == 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 1)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 2)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 3)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 4)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 5)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 6)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 7)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }
            else if (brojac == 8)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (mpc_uk);
            }

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            string ime_artikla = classSQL.select("Select naziv From roba where sifra = '" + artikl + "'", "").Tables[0].Rows[0][0].ToString();
            sveukupno = zbroj;
            string imereporta = "Ulaz robe na skladište";
            string sqln = " SELECT " +
                " '" + sveukupno.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + " Za artikl: " + "(" + artikl + ")" + ime_artikla + "' As naziv ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private DataTable ds1 = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracunnabavne(DataTable IZF, DataTable MSi, string NAZIV_SKLADISTA, string broj_skladista, DataTable DTotpr, DataTable DTpovdob, DataTable DTmpr)
        {
            decimal mpc_uk = 0;
            decimal id_skladiste_fak1 = 0;
            string ime_dokumenta1 = "";

            ++brojac;

            if (brojac > skladiste_brojac)
            {
                brojac = 0;
            }
            string sifra = "";
            string broj = "";
            string dokumenti = "";
            string dokumenti_IFA = "";

            for (int i = 0; i < IZF.Rows.Count; i++)
            {
                decimal vpc1 = Convert.ToDecimal(IZF.Rows[i]["vpc"].ToString());
                decimal kol1 = Convert.ToDecimal(IZF.Rows[i]["kolicina"].ToString().Replace(".", ","));
                id_skladiste_fak1 = Convert.ToDecimal(IZF.Rows[i]["id_skladiste"].ToString());
                sifra = IZF.Rows[i]["sifra"].ToString();
                broj = IZF.Rows[i]["broj_fakture"].ToString();

                mpc_uk = kol1 + mpc_uk;
                ime_dokumenta1 = "IZLAZNA FAKTURA";

                dokumenti_IFA += broj + " ,";
            }
            if (dokumenti_IFA.Length > 2)
            {
                dokumenti_IFA = dokumenti_IFA.Remove(dokumenti_IFA.Length - 2);
            }

            decimal msklad_izlaz = 0;
            string izaslo_iz = "";
            string sifra1 = "";
            string broj1 = "";
            string dokumenti_Msi = "";

            for (int x = 0; x < MSi.Rows.Count; x++)
            {
                decimal vpc3 = Convert.ToDecimal(MSi.Rows[x]["vpc"].ToString());
                decimal kol3 = Convert.ToDecimal(MSi.Rows[x]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_od3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_od"].ToString());
                decimal skladiste_do3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_do"].ToString());
                sifra1 = MSi.Rows[x]["sifra"].ToString();
                broj1 = MSi.Rows[x]["broj"].ToString();

                msklad_izlaz = kol3 + msklad_izlaz;
                izaslo_iz = "MEĐUSKLADIŠNICA izl ";
                dokumenti_Msi += broj1 + " ,";
            }
            if (dokumenti_Msi.Length > 2)
            {
                dokumenti_Msi = dokumenti_Msi.Remove(dokumenti_Msi.Length - 2);
            }

            decimal mpr_uk = 0;
            string ime_dokumenta_mpr = "";
            string sifra_mpr = "";
            string broj_mpr = "";
            string dokumenti_mpr = "";

            for (int x = 0; x < DTmpr.Rows.Count; x++)
            {
                decimal vpc3 = Convert.ToDecimal(DTmpr.Rows[x]["vpc"].ToString());
                decimal kol3 = Convert.ToDecimal(DTmpr.Rows[x]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_od3 = Convert.ToDecimal(DTmpr.Rows[x]["id_skladiste"].ToString());
                sifra_mpr = DTmpr.Rows[x]["sifra_robe"].ToString();
                broj_mpr = DTmpr.Rows[x]["broj_racuna"].ToString();

                mpr_uk = kol3 + mpr_uk;
                ime_dokumenta_mpr = "MP računi ";
                dokumenti_mpr += broj_mpr + " ,";
            }
            if (dokumenti_mpr.Length > 2)
            {
                dokumenti_mpr = dokumenti_mpr.Remove(dokumenti_mpr.Length - 2);
            }

            decimal mpc = 0;
            decimal mpc_ra = 0;
            decimal otpr_mpc_uk = 0;
            string ime_dokumenta3 = "";
            string sifra2 = "";
            string broj2 = "";
            string dokumenti_otpr = "";
            for (int w = 0; w < DTotpr.Rows.Count; w++)
            {
                decimal vpc5 = Convert.ToDecimal(DTotpr.Rows[w]["vpc"].ToString());
                decimal kol5 = Convert.ToDecimal(DTotpr.Rows[w]["kolicina"].ToString().Replace(".", ","));
                sifra2 = DTotpr.Rows[w]["sifra_robe"].ToString();
                broj2 = DTotpr.Rows[w]["broj_otpremnice"].ToString();

                otpr_mpc_uk = kol5 + otpr_mpc_uk;
                ime_dokumenta3 = "OTPREMNICA";
                dokumenti_otpr += broj2 + " ,";
            }
            if (dokumenti_otpr.Length > 2)
            {
                dokumenti_otpr = dokumenti_otpr.Remove(dokumenti_otpr.Length - 2);
            }

            decimal povdob_mpc_uk = 0;
            string ime_dokumenta6 = "";
            string sifra3 = "";
            string broj3 = "";
            string dokumenti_povdob = "";

            for (int l = 0; l < DTpovdob.Rows.Count; l++)
            {
                decimal vpc8 = Convert.ToDecimal(DTpovdob.Rows[l]["vpc"].ToString());
                decimal kol8 = Convert.ToDecimal(DTpovdob.Rows[l]["kolicina"].ToString().Replace(".", ","));
                sifra3 = DTpovdob.Rows[l]["sifra"].ToString();
                broj3 = DTpovdob.Rows[l]["broj"].ToString();

                povdob_mpc_uk = kol8 + povdob_mpc_uk;
                ime_dokumenta6 = "POVRATNICA DOBAVLJAČU";

                dokumenti_povdob += broj3 + " ,";
            }
            if (dokumenti_povdob.Length > 2)
            {
                dokumenti_povdob = dokumenti_povdob.Remove(dokumenti_povdob.Length - 2);
            }

            DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();

            if (mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra;
                DTrow["string2"] = dokumenti_IFA;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta1.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (mpr_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra_mpr;
                DTrow["string2"] = dokumenti_mpr;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta_mpr;
                DTrow["string8"] = "0";
                DTrow["string7"] = mpr_uk.ToString("#0.00");
                DTrow["string6"] = (0 - mpr_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_izlaz != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra1;
                DTrow["string2"] = dokumenti_Msi;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = izaslo_iz.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = msklad_izlaz.ToString("#0.00");
                DTrow["string6"] = (0 - msklad_izlaz).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (otpr_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra2;
                DTrow["string2"] = dokumenti_otpr;
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta3.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = otpr_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - otpr_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (povdob_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string1"] = sifra3;
                DTrow["string2"] = dokumenti_povdob;
                DTrow["string1"] = broj_skladista.ToString();
                DTrow["string2"] = broj_skladista.ToString();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta6.ToString();
                DTrow["string8"] = povdob_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = povdob_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            decimal stanje1 = 0;
            decimal stanje2 = 0;
            decimal stanje3 = 0;
            decimal stanje4 = 0;
            stanje1 = (0 - msklad_izlaz);//skladiste izlaz - stanje
            stanje2 = (0 - mpc_uk); //Fakture izlaz - stanje
            stanje3 = (0 - otpr_mpc_uk); // otpremnice - izlaz
            stanje4 = (0 - mpr_uk); // otpremnice - izlaz

            decimal sveukupno = 0;
            decimal zbroj = 0;
            decimal zbroj_ulaz = 0;
            decimal zbroj_izlaz = 0;

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);

            if (brojac == 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 1)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 2)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 3)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 4)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 5)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 6)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 7)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }
            else if (brojac == 8)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (povdob_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk).ToString("#0.00");
                DTrow["string6"] = (stanje1 + stanje2 + stanje3 + povdob_mpc_uk + stanje4).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje1 + stanje2 + stanje3 + stanje4 + povdob_mpc_uk);

                zbroj_ulaz = povdob_mpc_uk;
                zbroj_izlaz = (mpc_uk + otpr_mpc_uk + msklad_izlaz + mpr_uk);
            }

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            string ime_artikla = classSQL.select("Select naziv From roba where sifra = '" + artikl + "'", "").Tables[0].Rows[0][0].ToString();
            sveukupno = zbroj;
            string imereporta = "Izlaz robe na skladište";
            string sqln = " SELECT " +
                " '" + sveukupno.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + " Za artikl: " + "(" + artikl + ")" + ime_artikla + "' As naziv ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private void Ulaz()
        {
            string sqlpodaci = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            frmObracunporeza obrporez = new frmObracunporeza();
            string sql = "SELECT * FROM skladiste ORDER BY id_skladiste ASC";
            DataTable DTskl = classSQL.select(sql, "SKL").Tables[0];

            DataTable DTizlazna_fak = new DataTable();
            DataTable MS_ulaz = new DataTable();
            DataTable MS_izlaz = new DataTable();
            DataTable DTkalk = new DataTable();
            DataTable DTOtpremnica = new DataTable();
            DataTable DTradni_nalog = new DataTable();
            DataTable DTpocetno_stanje = new DataTable();
            DataTable DTpovratnica_dobavljacu = new DataTable();
            DataTable DTprimka = new DataTable();
            string skla_kalk = "";
            string skla_msklad_iz = "";
            string skla_msklad_ul = "";
            string skla_fak = "";
            string skla_otpr = "";
            string skla_radnal = "";
            string skla_pocsta = "";
            string skla_povdob = "";
            string skla_primka = "";

            if (bool1 == true)
            {
                skla_kalk = "AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ";
                skla_msklad_iz = "AND meduskladisnica.id_skladiste_od = '" + skladiste_odabir + "' ";
                skla_msklad_ul = "AND meduskladisnica.id_skladiste_do = '" + skladiste_odabir + "' ";
                skla_fak = "AND faktura_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_otpr = "AND otpremnice.id_skladiste = '" + skladiste_odabir + "' ";
                skla_radnal = "AND radni_nalog_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocsta = "AND pocetno_stanje.id_skladiste = '" + skladiste_odabir + "' ";
                skla_povdob = "AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ";
                skla_primka = "AND primka.id_skladiste = '" + skladiste_odabir + "' ";
            }
            else
            {
                skla_kalk = "";
                skla_msklad_iz = "";
                skla_msklad_ul = "";
                skla_fak = "";
                skla_otpr = "";
                skla_radnal = "";
                skla_pocsta = "";
                skla_povdob = "";
                skla_primka = "";
            }

            string artikl_kalk = "";
            string artikl_martikld_iz = "";
            string artikl_martikld_ul = "";
            string artikl_fak = "";
            string artikl_otpr = "";
            string artikl_radnal = "";
            string artikl_pocsta = "";
            string artikl_povdob = "";
            string artikl_primka = "";

            if (artikl != "")
            {
                artikl_kalk = "AND kalkulacija_stavke.sifra = '" + artikl + "' ";
                artikl_martikld_iz = "AND meduskladisnica_stavke.sifra = '" + artikl + "' ";
                artikl_martikld_ul = "AND meduskladisnica_stavke.sifra = '" + artikl + "' ";
                artikl_fak = "AND faktura_stavke.sifra = '" + artikl + "' ";
                artikl_otpr = "AND otpremnica_stavke.sifra_robe = '" + artikl + "' ";
                artikl_radnal = "AND radni_nalog_stavke.sifra_robe = '" + artikl + "' ";
                artikl_pocsta = "AND pocetno_stanje_stavke.sifra = '" + artikl + "' ";
                artikl_povdob = "AND povrat_robe_stavke.sifra = '" + artikl + "' ";
                artikl_primka = "AND primka_stavke.sifra = '" + artikl + "' ";
            }
            else
            {
                artikl = "";
            }

            //string filter = skla ;

            //if (filter.Length != 0)
            //{
            //filter = " WHERE " + filter;
            //filter = filter.Remove(filter.Length + 20 , 8);
            //}

            foreach (DataRow row in DTskl.Rows)
            {
                MS_ulaz = classSQL.select("SELECT meduskladisnica_stavke.nbc, meduskladisnica_stavke.sifra, meduskladisnica_stavke.broj, meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND meduskladisnica.id_skladiste_do='" + row["id_skladiste"].ToString() + "' " + skla_msklad_ul + artikl_martikld_ul + " ", "Meduskladisnica_ulaz").Tables[0];
                DTkalk = classSQL.select("SELECT kalkulacija_stavke.fak_cijena,kalkulacija_stavke.sifra, kalkulacija_stavke.broj, kalkulacija_stavke.id_skladiste , kalkulacija_stavke.kolicina, kalkulacija_stavke.vpc FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.id_kalkulacija = kalkulacija_stavke.id_kalkulacija WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + artikl_kalk + " ", "Kalkulacija").Tables[0];
                DTradni_nalog = classSQL.select("SELECT radni_nalog_stavke.nbc,radni_nalog_stavke.sifra_robe, radni_nalog_stavke.broj_naloga, radni_nalog_stavke.vpc, radni_nalog_stavke.id_skladiste, radni_nalog_stavke.kolicina FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga WHERE  radni_nalog.datum_naloga >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND radni_nalog.datum_naloga <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND radni_nalog_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_radnal + artikl_radnal + "  ", "radni_nalog").Tables[0];
                DTpocetno_stanje = classSQL.select("SELECT pocetno_stanje_stavke.nbc, pocetno_stanje_stavke.sifra, pocetno_stanje_stavke.broj, pocetno_stanje.id_skladiste, pocetno_stanje_stavke.kolicina FROM pocetno_stanje_stavke LEFT JOIN pocetno_stanje ON pocetno_stanje.broj = pocetno_stanje_stavke.broj WHERE  pocetno_stanje.date >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND pocetno_stanje.date <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND pocetno_stanje.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + artikl_pocsta + "  ", "pocetno_stanje").Tables[0];
                DTprimka = classSQL.select("SELECT primka_stavke.nbc, primka_stavke.sifra, primka_stavke.broj, primka.id_skladiste, primka_stavke.kolicina, primka_stavke.mpc, primka_stavke.vpc FROM primka_stavke LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka WHERE  primka.datum >= '" + datumOD.ToString("dd-MM-yyyy") + "' AND primka.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND primka.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_primka + artikl_primka + "  ", "primka").Tables[0];

                SETizracunskladnisne(MS_ulaz, DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString(), DTradni_nalog, DTpocetno_stanje, DTprimka);
            }
        }

        private void Izlaz()
        {
            string sqlpodaci = "SELECT " +
                " podaci_tvrtka.ime_tvrtke," +
                " podaci_tvrtka.skraceno_ime," +
                " podaci_tvrtka.oib," +
                " podaci_tvrtka.tel," +
                " podaci_tvrtka.fax," +
                " podaci_tvrtka.mob," +
                " podaci_tvrtka.iban," +
                " podaci_tvrtka.adresa," +
                " podaci_tvrtka.vl," +
                " podaci_tvrtka.poslovnica_adresa," +
                " podaci_tvrtka.poslovnica_grad," +
                " podaci_tvrtka.email," +
                " podaci_tvrtka.zr," +
                " podaci_tvrtka.naziv_fakture," +
                " podaci_tvrtka.text_bottom," +
                " grad.grad + '' + grad.posta AS grad" +
                " FROM podaci_tvrtka" +
                " LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad" +
                "";
            classSQL.CeAdatpter(sqlpodaci).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            frmObracunporeza obrporez = new frmObracunporeza();
            string sql = "SELECT * FROM skladiste ORDER BY id_skladiste ASC";
            DataTable DTskl = classSQL.select(sql, "SKL").Tables[0];

            DataTable DTizlazna_fak = new DataTable();
            DataTable MS_ulaz = new DataTable();
            DataTable MS_izlaz = new DataTable();
            DataTable DTkalk = new DataTable();
            DataTable DTOtpremnica = new DataTable();
            DataTable DTradni_nalog = new DataTable();
            DataTable DTpocetno_stanje = new DataTable();
            DataTable DTpovratnica_dobavljacu = new DataTable();
            DataTable DTprimka = new DataTable();
            DataTable DTmpr = new DataTable();

            string skla_kalk = "";
            string skla_msklad_iz = "";
            string skla_msklad_ul = "";
            string skla_fak = "";
            string skla_otpr = "";
            string skla_radnal = "";
            string skla_pocsta = "";
            string skla_povdob = "";
            string skla_primka = "";
            string skla_mpr = "";

            if (bool1 == true)
            {
                skla_kalk = "AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ";
                skla_msklad_iz = "AND meduskladisnica.id_skladiste_od = '" + skladiste_odabir + "' ";
                skla_msklad_ul = "AND meduskladisnica.id_skladiste_do = '" + skladiste_odabir + "' ";
                skla_fak = "AND faktura_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_otpr = "AND otpremnice.id_skladiste = '" + skladiste_odabir + "' ";
                skla_radnal = "AND radni_nalog_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocsta = "AND pocetno_stanje.id_skladiste = '" + skladiste_odabir + "' ";
                skla_povdob = "AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ";
                skla_primka = "AND primka.id_skladiste = '" + skladiste_odabir + "' ";
                skla_mpr = "AND racun_stavke.id_skladiste = '" + skladiste_odabir + "' ";
            }
            else
            {
                skla_kalk = "";
                skla_msklad_iz = "";
                skla_msklad_ul = "";
                skla_fak = "";
                skla_otpr = "";
                skla_radnal = "";
                skla_pocsta = "";
                skla_povdob = "";
                skla_primka = "";
                skla_mpr = "";
            }

            string artikl_kalk = "";
            string artikl_martikld_iz = "";
            string artikl_martikld_ul = "";
            string artikl_fak = "";
            string artikl_otpr = "";
            string artikl_radnal = "";
            string artikl_pocsta = "";
            string artikl_povdob = "";
            string artikl_primka = "";
            string artikl_mpr = "";

            if (artikl != "")
            {
                artikl_kalk = "AND kalkulacija_stavke.sifra = '" + artikl + "' ";
                artikl_martikld_iz = "AND meduskladisnica_stavke.sifra = '" + artikl + "' ";
                artikl_martikld_ul = "AND meduskladisnica_stavke.sifra = '" + artikl + "' ";
                artikl_fak = "AND faktura_stavke.sifra = '" + artikl + "' ";
                artikl_otpr = "AND otpremnica_stavke.sifra_robe = '" + artikl + "' ";
                artikl_radnal = "AND radni_nalog_stavke.sifra_robe = '" + artikl + "' ";
                artikl_pocsta = "AND pocetno_stanje_stavke.sifra = '" + artikl + "' ";
                artikl_povdob = "AND povrat_robe_stavke.sifra = '" + artikl + "' ";
                artikl_primka = "AND primka_stavke.sifra = '" + artikl + "' ";
                artikl_mpr = "AND racun_stavke.sifra_robe = '" + artikl + "' ";
            }
            else
            {
                artikl = "";
            }
            //string filter = skla ;

            //if (filter.Length != 0)
            //{
            //filter = " WHERE " + filter;
            //filter = filter.Remove(filter.Length + 20 , 8);
            //}

            foreach (DataRow row in DTskl.Rows)
            {
                DTizlazna_fak = classSQL.select("SELECT faktura_stavke.nbc, fakture.broj_fakture, faktura_stavke.sifra, faktura_stavke.vpc, faktura_stavke.id_skladiste, faktura_stavke.kolicina" +
                    " FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture" +
                    " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                    " WHERE  fakture.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND fakture.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND faktura_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_fak + artikl_fak + "  ", "Izlazne_fakture").Tables[0];

                MS_izlaz = classSQL.select("SELECT meduskladisnica_stavke.nbc,meduskladisnica_stavke.sifra, meduskladisnica_stavke.broj, meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND meduskladisnica.id_skladiste_od='" + row["id_skladiste"].ToString() + "' " + skla_msklad_iz + artikl_martikld_iz + " ", "Meduskladisnica_izlaz").Tables[0];
                DTOtpremnica = classSQL.select("SELECT otpremnica_stavke.nbc,otpremnica_stavke.sifra_robe,  otpremnica_stavke.broj_otpremnice, otpremnica_stavke.vpc, otpremnica_stavke.id_skladiste, otpremnica_stavke.kolicina FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.id_otpremnica = otpremnica_stavke.id_otpremnice WHERE  otpremnice.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND otpremnice.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND otpremnice.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_otpr + artikl_otpr + "  ", "Otpremnice").Tables[0];
                DTpovratnica_dobavljacu = classSQL.select("SELECT povrat_robe_stavke.nbc,povrat_robe_stavke.broj, povrat_robe_stavke.sifra, povrat_robe_stavke.vpc, povrat_robe.id_skladiste, povrat_robe_stavke.kolicina FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj WHERE  povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND povrat_robe.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_povdob + artikl_povdob + "  ", "povrat_robe").Tables[0];
                DTmpr = classSQL.select("SELECT racun_stavke.nbc,racun_stavke.broj_racuna, racun_stavke.sifra_robe, racun_stavke.vpc, racun_stavke.id_skladiste, racun_stavke.kolicina FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna WHERE  racuni.datum_racuna >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND racun_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_mpr + artikl_mpr + "  ", "racun_stavke").Tables[0];

                SETizracunnabavne(DTizlazna_fak, MS_izlaz, row["skladiste"].ToString(), row["id_skladiste"].ToString(), DTOtpremnica, DTpovratnica_dobavljacu, DTmpr);
            }
        }
    }
}