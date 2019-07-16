using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Rekapitulacija
{
    public partial class frmListe6 : Form
    {
        public frmListe6()
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
        public string ducan { get; set; }
        public int skladiste_brojac { get; set; }
        public Boolean bool1 { get; set; }
        public Boolean bool2 { get; set; } // pomoćni bit
        public int brojac { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        public string boolBrojKalk { get; set; }

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }
        public bool sa_rabatom { get; set; }
        public bool samo_robno { get; set; }

        private string query_samo_robno = "";
        private string query_samo_robno_dodatak_za_fakturu = "";

        private void frmListe_Load(object sender, EventArgs e)
        {
            if (samo_robno)
            {
                query_samo_robno = " roba.oduzmi='DA'  AND ";
                query_samo_robno_dodatak_za_fakturu = "fakture.oduzmi_iz_skladista='1' AND ";
            }
            else
            {
                query_samo_robno = "";
            }

            //Sintetika_po_skladisnim_cijenama_sva_skladista();

            if (documenat == "SINT_NAB_CJE")
            {
                Sintetika_po_nabavnim_cijenama_sva_skladista();
                //this.Text = ImeForme;
            }

            if (documenat == "SINT_PRO_CJE")
            {
                Sintetika_po_prodajnim_cijenama_sva_skladista();
                //this.Text = ImeForme;
            }

            if (documenat == "SINT_SKL_CJE")
            {
                Sintetika_po_skladisnim_cijenama_sva_skladista();
                //this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private DataTable ds = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracun(DataTable IZF, DataTable MSu, DataTable MSi, DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista, DataTable DTotpr, DataTable DTradnal, DataTable DTpocsta, DataTable DTpovdob, DataTable DTprimka, DataTable DTmaloprodaja)
        {
            decimal mpc_uk = 0;
            decimal mpc2 = 0;
            decimal mpc_ra2 = 0;
            decimal id_skladiste_fak1 = 0;
            string ime_dokumenta1 = "";

            ++brojac;

            if (brojac > skladiste_brojac)
            {
                brojac = 0;
            }

            for (int i = 0; i < IZF.Rows.Count; i++)
            {
                decimal vpc1 = Convert.ToDecimal(IZF.Rows[i]["vpc"].ToString());
                decimal rabat1 = Convert.ToDecimal(IZF.Rows[i]["rabat"].ToString());
                decimal kol1 = Convert.ToDecimal(IZF.Rows[i]["kolicina"].ToString().Replace(".", ","));
                decimal pdv1 = Convert.ToDecimal(IZF.Rows[i]["porez"].ToString());
                id_skladiste_fak1 = Convert.ToDecimal(IZF.Rows[i]["id_skladiste"].ToString());

                mpc2 = vpc1 * (1 + (pdv1 / 100));
                mpc_ra2 = mpc2 - (mpc2 * (rabat1 / 100));

                if (sa_rabatom)
                {
                    mpc_ra2 = mpc2 - (mpc2 * (rabat1 / 100));
                }
                else
                {
                    mpc_ra2 = mpc2;
                }

                mpc_uk = (mpc_ra2 * kol1) + mpc_uk;
                ime_dokumenta1 = "IZLAZNA FAKTURA";
            }

            decimal pdv_stv3 = 0;
            decimal msklad_ulaz1 = 0;
            string uslo_iz1 = "";

            for (int z = 0; z < MSu.Rows.Count; z++)
            {
                decimal vpc2 = Convert.ToDecimal(MSu.Rows[z]["vpc"].ToString());
                decimal kol2 = Convert.ToDecimal(MSu.Rows[z]["kolicina"].ToString().Replace(".", ","));
                decimal pdv2 = Convert.ToDecimal(MSu.Rows[z]["pdv"].ToString());
                decimal skladiste_do2 = Convert.ToDecimal(MSu.Rows[z]["id_skladiste_do"].ToString());

                pdv_stv3 = vpc2 * (1 + (pdv2 / 100));
                msklad_ulaz1 = (pdv_stv3 * kol2) + msklad_ulaz1;
                uslo_iz1 = "MEĐUSKLADIŠNICA ulaz ";
            }

            decimal pdv_stv2 = 0;
            decimal msklad_izlaz = 0;
            string izaslo_iz = "";

            for (int x = 0; x < MSi.Rows.Count; x++)
            {
                decimal vpc3 = Convert.ToDecimal(MSi.Rows[x]["vpc"].ToString());
                decimal kol3 = Convert.ToDecimal(MSi.Rows[x]["kolicina"].ToString().Replace(".", ","));
                decimal pdv3 = Convert.ToDecimal(MSi.Rows[x]["pdv"].ToString());
                decimal skladiste_od3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_od"].ToString());
                decimal skladiste_do3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_do"].ToString());

                pdv_stv2 = vpc3 * (1 + (pdv3 / 100));
                msklad_izlaz = (pdv_stv2 * kol3) + msklad_izlaz;
                izaslo_iz = "MEĐUSKLADIŠNICA izl ";
            }

            decimal mpc1 = 0;
            decimal mpc_ra1 = 0;
            decimal kalk_mpc_uk = 0;
            string ime_dokumetna2 = "";

            for (int y = 0; y < Kal.Rows.Count; y++)
            {
                decimal vpc4 = Convert.ToDecimal(Kal.Rows[y]["vpc"].ToString());
                decimal rabat4 = Convert.ToDecimal(Kal.Rows[y]["rabat"].ToString());
                decimal kol4 = Convert.ToDecimal(Kal.Rows[y]["kolicina"].ToString().Replace(".", ","));
                decimal pdv4 = Convert.ToDecimal(Kal.Rows[y]["porez"].ToString());

                mpc1 = vpc4 * (1 + (pdv4 / 100));
                //mpc_ra1 = mpc1 - (mpc1* (rabat4 / 100));

                kalk_mpc_uk = (mpc1 * kol4) + kalk_mpc_uk;
                ime_dokumetna2 = "KALKULACIJA";
            }

            decimal mpc = 0;
            decimal mpc_ra = 0;
            decimal otpr_mpc_uk = 0;
            string ime_dokumenta3 = "";

            for (int w = 0; w < DTotpr.Rows.Count; w++)
            {
                decimal vpc5 = Convert.ToDecimal(DTotpr.Rows[w]["vpc"].ToString());
                decimal rabat5 = Convert.ToDecimal(DTotpr.Rows[w]["rabat"].ToString());
                decimal kol5 = Convert.ToDecimal(DTotpr.Rows[w]["kolicina"].ToString().Replace(".", ","));
                decimal pdv5 = Convert.ToDecimal(DTotpr.Rows[w]["porez"].ToString());

                mpc = vpc5 * (1 + (pdv5 / 100));

                if (sa_rabatom)
                {
                    mpc_ra = mpc - (mpc * (rabat5 / 100));
                }
                else
                {
                    mpc_ra = mpc;
                }

                otpr_mpc_uk = (mpc_ra * kol5) + otpr_mpc_uk;
                ime_dokumenta3 = "OTPREMNICA";
            }

            decimal pdv_stv5 = 0;
            decimal radnal_mpc_uk = 0;
            string ime_dokumenta4 = "";

            for (int j = 0; j < DTradnal.Rows.Count; j++)
            {
                decimal vpc6 = Convert.ToDecimal(DTradnal.Rows[j]["vpc"].ToString());
                decimal kol6 = Convert.ToDecimal(DTradnal.Rows[j]["kolicina"].ToString().Replace(".", ","));
                decimal pdv6 = Convert.ToDecimal(DTradnal.Rows[j]["porez"].ToString());

                pdv_stv5 = vpc6 * (1 + (pdv6 / 100));
                radnal_mpc_uk = ((pdv_stv5 * kol6) * (-1)) + radnal_mpc_uk;
                ime_dokumenta4 = "RADNI NALOG";
            }

            decimal pocsta_mpc_uk = 0;
            string ime_dokumenta5 = "";

            for (int v = 0; v < DTpocsta.Rows.Count; v++)
            {
                decimal _mpc, _kol;
                decimal.TryParse(DTpocsta.Rows[v]["mpc"].ToString(), out _mpc);
                decimal.TryParse(DTpocsta.Rows[v]["kolicina"].ToString().Replace(".", ","), out _kol);

                decimal mpc7 = _mpc;
                decimal kol7 = _kol;

                pocsta_mpc_uk = (mpc7 * kol7) + pocsta_mpc_uk;
                ime_dokumenta5 = "POCETNO STANJE";
            }

            decimal rab_stv6 = 0;
            decimal pdv_stv6 = 0;
            decimal povdob_mpc_uk = 0;
            string ime_dokumenta6 = "";
            decimal mpc_1 = 0;

            for (int l = 0; l < DTpovdob.Rows.Count; l++)
            {
                decimal vpc8 = Convert.ToDecimal(DTpovdob.Rows[l]["vpc"].ToString());
                decimal rabat8 = Convert.ToDecimal(DTpovdob.Rows[l]["rabat"].ToString());
                decimal kol8 = Convert.ToDecimal(DTpovdob.Rows[l]["kolicina"].ToString().Replace(".", ","));
                decimal pdv8 = Convert.ToDecimal(DTpovdob.Rows[l]["pdv"].ToString());

                mpc_1 = vpc8 + (vpc8 * (pdv8 / 100));
                rab_stv6 = mpc_1 - (mpc_1 * (rabat8 / 100));
                pdv_stv6 = rab_stv6 * (((pdv8 * 100) / (pdv8 + 100)) / 100);
                povdob_mpc_uk = ((rab_stv6 * kol8) * (-1)) + povdob_mpc_uk;
                ime_dokumenta6 = "POVRATNICA DOBAVLJAČU";
            }

            decimal rab_stv7 = 0;
            decimal pdv_stv7 = 0;
            decimal povdob_mpc_uk1 = 0;
            string ime_dokumenta7 = "";
            decimal mpc_2 = 0;
            decimal primka_mpc_uk = 0;

            for (int m = 0; m < DTprimka.Rows.Count; m++)
            {
                decimal vpc9 = Convert.ToDecimal(DTprimka.Rows[m]["vpc"].ToString());
                decimal rabat9 = Convert.ToDecimal(DTprimka.Rows[m]["rabat"].ToString());
                decimal kol9 = Convert.ToDecimal(DTprimka.Rows[m]["kolicina"].ToString().Replace(".", ","));
                decimal pdv9 = Convert.ToDecimal(DTprimka.Rows[m]["pdv"].ToString());

                ime_dokumenta7 = "PRIMKA";
                mpc_1 = vpc9 + (vpc9 * (pdv9 / 100));
                rab_stv7 = mpc_1 - (mpc_1 * (rabat9 / 100));
                pdv_stv7 = rab_stv7 * (((pdv9 * 100) / (pdv9 + 100)) / 100);
                primka_mpc_uk = (rab_stv7 * kol9) + primka_mpc_uk;
            }

            decimal rab_stv8 = 0;
            decimal pdv_stv8 = 0;
            string ime_dokumenta8 = "";
            decimal maloprodaja_mpc_uk = 0;

            for (int m = 0; m < DTmaloprodaja.Rows.Count; m++)
            {
                decimal vpc9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["vpc"].ToString());
                decimal rabat9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["rabat"].ToString());
                decimal kol9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["kolicina"].ToString().Replace(".", ","));
                decimal pdv9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["porez"].ToString());

                ime_dokumenta8 = "MALOPRODAJA";
                mpc_1 = vpc9 + (vpc9 * (pdv9 / 100));
                if (sa_rabatom)
                {
                    rab_stv8 = mpc_1 - (mpc_1 * (rabat9 / 100));
                }
                else
                {
                    rab_stv8 = mpc_1;
                }
                pdv_stv8 = rab_stv8 * (((pdv9 * 100) / (pdv9 + 100)) / 100);
                maloprodaja_mpc_uk = (rab_stv8 * kol9) + maloprodaja_mpc_uk;
            }

            DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();

            if (mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta1.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_ulaz1 != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = uslo_iz1.ToString();
                DTrow["string8"] = msklad_ulaz1.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = msklad_ulaz1.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_izlaz != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = izaslo_iz.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = msklad_izlaz.ToString("#0.00");
                DTrow["string6"] = (0 - msklad_izlaz).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (kalk_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumetna2.ToString();
                DTrow["string8"] = kalk_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = kalk_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (otpr_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta3.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = otpr_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - otpr_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (radnal_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
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
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta5.ToString();
                DTrow["string8"] = pocsta_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = pocsta_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (povdob_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta6.ToString();
                DTrow["string8"] = povdob_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = povdob_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (primka_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta7.ToString();
                DTrow["string8"] = primka_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = primka_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (maloprodaja_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta8.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = maloprodaja_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - maloprodaja_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            decimal stanje1 = 0;
            decimal stanje2 = 0;
            decimal stanje3 = 0;
            decimal stanje4 = radnal_mpc_uk;
            decimal stanje5 = 0;
            stanje1 = (0 - msklad_izlaz);//skladiste izlaz - stanje
            stanje2 = (0 - mpc_uk); //Fakture izlaz - stanje
            stanje3 = (0 - otpr_mpc_uk); // otpremnice - izlaz
            stanje5 = (0 - maloprodaja_mpc_uk);

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
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 1)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 2)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 3)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 4)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 5)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 6)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 7)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 8)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);

            sveukupno = zbroj;
            string imereporta = "za prodajne cijene";
            string sqln = " SELECT " +
                " '" + sveukupno.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private DataTable dsskl = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracunskladnisne(DataTable IZF, DataTable MSu, DataTable MSi, DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista, DataTable DTotpr, DataTable DTradnal, DataTable DTpocsta, DataTable DTpovdob, DataTable DTprimka, DataTable DTmaloprodaja)
        {
            decimal mpc_uk = 0;
            decimal id_skladiste_fak1 = 0;
            string ime_dokumenta1 = "";

            ++brojac;

            if (brojac > skladiste_brojac)
            {
                brojac = 0;
            }

            for (int i = 0; i < IZF.Rows.Count; i++)
            {
                decimal vpc1 = Convert.ToDecimal(IZF.Rows[i]["vpc"].ToString());
                decimal kol1 = Convert.ToDecimal(IZF.Rows[i]["kolicina"].ToString().Replace(".", ","));
                id_skladiste_fak1 = Convert.ToDecimal(IZF.Rows[i]["id_skladiste"].ToString());

                mpc_uk = (vpc1 * kol1) + mpc_uk;
                ime_dokumenta1 = "IZLAZNA FAKTURA";
            }

            decimal msklad_ulaz1 = 0;
            string uslo_iz1 = "";

            for (int z = 0; z < MSu.Rows.Count; z++)
            {
                decimal vpc2 = Convert.ToDecimal(MSu.Rows[z]["vpc"].ToString());
                decimal kol2 = Convert.ToDecimal(MSu.Rows[z]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_do2 = Convert.ToDecimal(MSu.Rows[z]["id_skladiste_do"].ToString());

                msklad_ulaz1 = (vpc2 * kol2) + msklad_ulaz1;
                uslo_iz1 = "MEĐUSKLADIŠNICA ulaz ";
            }

            decimal msklad_izlaz = 0;
            string izaslo_iz = "";

            for (int x = 0; x < MSi.Rows.Count; x++)
            {
                decimal vpc3 = Convert.ToDecimal(MSi.Rows[x]["vpc"].ToString());
                decimal kol3 = Convert.ToDecimal(MSi.Rows[x]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_od3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_od"].ToString());
                decimal skladiste_do3 = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_do"].ToString());

                msklad_izlaz = (vpc3 * kol3) + msklad_izlaz;
                izaslo_iz = "MEĐUSKLADIŠNICA izl ";
            }

            decimal mpc1 = 0;
            decimal mpc_ra1 = 0;
            decimal kalk_mpc_uk = 0;
            string ime_dokumetna2 = "";

            for (int y = 0; y < Kal.Rows.Count; y++)
            {
                decimal vpc4 = Convert.ToDecimal(Kal.Rows[y]["vpc"].ToString());
                decimal kol4 = Convert.ToDecimal(Kal.Rows[y]["kolicina"].ToString().Replace(".", ","));

                kalk_mpc_uk = (vpc4 * kol4) + kalk_mpc_uk;
                ime_dokumetna2 = "KALKULACIJA";
            }

            decimal mpc = 0;
            decimal mpc_ra = 0;
            decimal otpr_mpc_uk = 0;
            string ime_dokumenta3 = "";

            for (int w = 0; w < DTotpr.Rows.Count; w++)
            {
                decimal vpc5 = Convert.ToDecimal(DTotpr.Rows[w]["vpc"].ToString());
                decimal kol5 = Convert.ToDecimal(DTotpr.Rows[w]["kolicina"].ToString().Replace(".", ","));

                otpr_mpc_uk = (vpc5 * kol5) + otpr_mpc_uk;
                ime_dokumenta3 = "OTPREMNICA";
            }

            decimal radnal_mpc_uk = 0;
            string ime_dokumenta4 = "";

            for (int j = 0; j < DTradnal.Rows.Count; j++)
            {
                decimal vpc6 = Convert.ToDecimal(DTradnal.Rows[j]["vpc"].ToString());
                decimal kol6 = Convert.ToDecimal(DTradnal.Rows[j]["kolicina"].ToString().Replace(".", ","));

                radnal_mpc_uk = ((vpc6 * kol6) * (-1)) + radnal_mpc_uk;
                ime_dokumenta4 = "RADNI NALOG";
            }

            decimal pocsta_mpc_uk = 0;
            string ime_dokumenta5 = "";

            for (int v = 0; v < DTpocsta.Rows.Count; v++)
            {
                decimal mpc7 = Convert.ToDecimal(DTpocsta.Rows[v]["mpc"].ToString());
                decimal kol7 = Convert.ToDecimal(DTpocsta.Rows[v]["kolicina"].ToString().Replace(".", ","));

                pocsta_mpc_uk = (mpc7 * kol7) + pocsta_mpc_uk;
                ime_dokumenta5 = "POCETNO STANJE";
            }

            decimal povdob_mpc_uk = 0;
            string ime_dokumenta6 = "";

            for (int l = 0; l < DTpovdob.Rows.Count; l++)
            {
                decimal vpc8 = Convert.ToDecimal(DTpovdob.Rows[l]["vpc"].ToString());
                decimal kol8 = Convert.ToDecimal(DTpovdob.Rows[l]["kolicina"].ToString().Replace(".", ","));

                povdob_mpc_uk = ((vpc8 * kol8) * (-1)) + povdob_mpc_uk;
                ime_dokumenta6 = "POVRATNICA DOBAVLJAČU";
            }

            string ime_dokumenta7 = "";
            decimal primka_mpc_uk = 0;

            for (int m = 0; m < DTprimka.Rows.Count; m++)
            {
                decimal vpc9 = Convert.ToDecimal(DTprimka.Rows[m]["vpc"].ToString());
                decimal kol9 = Convert.ToDecimal(DTprimka.Rows[m]["kolicina"].ToString().Replace(".", ","));

                ime_dokumenta7 = "PRIMKA";
                primka_mpc_uk = (vpc9 * kol9) + primka_mpc_uk;
            }

            string ime_dokumenta8 = "";
            decimal maloprodaja_mpc_uk = 0;

            for (int m = 0; m < DTmaloprodaja.Rows.Count; m++)
            {
                decimal vpc9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["vpc"].ToString());
                decimal kol9 = Convert.ToDecimal(DTmaloprodaja.Rows[m]["kolicina"].ToString().Replace(".", ","));

                ime_dokumenta8 = "MALOPRODAJA";
                maloprodaja_mpc_uk = (vpc9 * kol9) + maloprodaja_mpc_uk;
            }

            DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();

            if (mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta1.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_ulaz1 != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = uslo_iz1.ToString();
                DTrow["string8"] = msklad_ulaz1.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = msklad_ulaz1.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_izlaz != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = izaslo_iz.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = msklad_izlaz.ToString("#0.00");
                DTrow["string6"] = (0 - msklad_izlaz).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (kalk_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumetna2.ToString();
                DTrow["string8"] = kalk_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = kalk_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (otpr_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta3.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = otpr_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - otpr_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (radnal_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
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
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta5.ToString();
                DTrow["string8"] = pocsta_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = pocsta_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (povdob_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta6.ToString();
                DTrow["string8"] = povdob_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = povdob_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (primka_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta7.ToString();
                DTrow["string8"] = primka_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = primka_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (maloprodaja_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta8.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = maloprodaja_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - maloprodaja_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            decimal stanje1 = 0;
            decimal stanje2 = 0;
            decimal stanje3 = 0;
            decimal stanje4 = radnal_mpc_uk;
            decimal stanje5 = 0;
            stanje1 = (0 - msklad_izlaz);//skladiste izlaz - stanje
            stanje2 = (0 - mpc_uk); //Fakture izlaz - stanje
            stanje3 = (0 - otpr_mpc_uk); // otpremnice - izlaz
            stanje5 = (0 - maloprodaja_mpc_uk);

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
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 1)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 2)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 3)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 4)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 5)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 6)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 7)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 8)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + mpc_uk + otpr_mpc_uk + msklad_izlaz);
            }

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);

            sveukupno = zbroj;
            string imereporta = "za skladišne cijene";
            string sqln = " SELECT " +
                " '" + sveukupno.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private DataTable ds1 = new Dataset.DSRlisteTekst.DTlisteTekstDataTable();

        private void SETizracunnabavne(DataTable IZF, DataTable MSu, DataTable MSi, DataTable Kal, string NAZIV_SKLADISTA, string broj_skladista, DataTable DTotpr, DataTable DTradnal, DataTable DTpocsta, DataTable DTpovdob, DataTable DTprimka, DataTable DTmaloprodaja)
        {
            decimal nbc_uk = 0;

            decimal id_skladiste_fak = 0;
            string ime_dokumenta1 = "";

            ++brojac;

            if (brojac > skladiste_brojac)
            {
                brojac = 0;
            }

            for (int i = 0; i < IZF.Rows.Count; i++)
            {
                decimal nbc = Convert.ToDecimal(IZF.Rows[i]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(IZF.Rows[i]["kolicina"].ToString().Replace(".", ","));
                id_skladiste_fak = Convert.ToDecimal(IZF.Rows[i]["id_skladiste"].ToString());

                nbc_uk = (nbc * kol) + nbc_uk;
                ime_dokumenta1 = "IZLAZNA FAKTURA";
            }

            decimal msklad_ulaz1 = 0;
            string uslo_iz1 = "";

            for (int z = 0; z < MSu.Rows.Count; z++)
            {
                decimal vpc = Convert.ToDecimal(MSu.Rows[z]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(MSu.Rows[z]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_do = Convert.ToDecimal(MSu.Rows[z]["id_skladiste_do"].ToString());

                msklad_ulaz1 = (vpc * kol) + msklad_ulaz1;
                uslo_iz1 = "MEĐUSKLADIŠNICA ulaz ";
            }

            decimal msklad_izlaz = 0;
            string izaslo_iz = "";

            for (int x = 0; x < MSi.Rows.Count; x++)
            {
                decimal nbc = Convert.ToDecimal(MSi.Rows[x]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(MSi.Rows[x]["kolicina"].ToString().Replace(".", ","));
                decimal skladiste_od = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_od"].ToString());
                decimal skladiste_do = Convert.ToDecimal(MSi.Rows[x]["id_skladiste_do"].ToString());

                msklad_izlaz = (nbc * kol) + msklad_izlaz;
                izaslo_iz = "MEĐUSKLADIŠNICA izl ";
            }

            decimal kalk_mpc_uk = 0;
            string ime_dokumetna2 = "";
            decimal rabat;
            for (int y = 0; y < Kal.Rows.Count; y++)
            {
                decimal fak_cijena = Convert.ToDecimal(Kal.Rows[y]["fak_cijena"].ToString());
                decimal kol = Convert.ToDecimal(Kal.Rows[y]["kolicina"].ToString().Replace(".", ","));
                decimal.TryParse(Kal.Rows[y]["rabat"].ToString().Replace(".", ","), out rabat);

                fak_cijena = fak_cijena - (fak_cijena * rabat / 100);

                kalk_mpc_uk = (fak_cijena * kol) + kalk_mpc_uk;
                ime_dokumetna2 = "KALKULACIJA";
            }

            decimal otpr_mpc_uk = 0;
            string ime_dokumenta3 = "";

            for (int w = 0; w < DTotpr.Rows.Count; w++)
            {
                decimal nbc = Convert.ToDecimal(DTotpr.Rows[w]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTotpr.Rows[w]["kolicina"].ToString().Replace(".", ","));

                otpr_mpc_uk = (nbc * kol) + otpr_mpc_uk;
                ime_dokumenta3 = "OTPREMNICA";
            }

            decimal radnal_mpc_uk = 0;
            string ime_dokumenta4 = "";

            for (int j = 0; j < DTradnal.Rows.Count; j++)
            {
                decimal nbc = Convert.ToDecimal(DTradnal.Rows[j]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTradnal.Rows[j]["kolicina"].ToString().Replace(".", ","));

                radnal_mpc_uk = ((nbc * kol) * (-1)) + radnal_mpc_uk;
                ime_dokumenta4 = "RADNI NALOG";
            }

            decimal pocsta_mpc_uk = 0;
            string ime_dokumenta5 = "";

            for (int v = 0; v < DTpocsta.Rows.Count; v++)
            {
                decimal nbc = Convert.ToDecimal(DTpocsta.Rows[v]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTpocsta.Rows[v]["kolicina"].ToString().Replace(".", ","));

                pocsta_mpc_uk = (nbc * kol) + pocsta_mpc_uk;
                ime_dokumenta5 = "POCETNO STANJE";
            }

            decimal povdob_mpc_uk = 0;
            string ime_dokumenta6 = "";

            for (int l = 0; l < DTpovdob.Rows.Count; l++)
            {
                decimal nbc = Convert.ToDecimal(DTpovdob.Rows[l]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTpovdob.Rows[l]["kolicina"].ToString().Replace(".", ","));

                povdob_mpc_uk = ((nbc * kol) * (-1)) + povdob_mpc_uk;
                ime_dokumenta6 = "POVRATNICA DOBAVLJAČU";
            }

            decimal primka_mpc_uk = 0;
            string ime_dokumenta7 = "";

            for (int m = 0; m < DTprimka.Rows.Count; m++)
            {
                decimal nbc = Convert.ToDecimal(DTprimka.Rows[m]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTprimka.Rows[m]["kolicina"].ToString().Replace(".", ","));

                primka_mpc_uk = (nbc * kol) + primka_mpc_uk;
                ime_dokumenta7 = "PRIMKA";
            }

            decimal maloprodaja_mpc_uk = 0;
            string ime_dokumenta8 = "";

            for (int m = 0; m < DTmaloprodaja.Rows.Count; m++)
            {
                decimal nbc = Convert.ToDecimal(DTmaloprodaja.Rows[m]["nbc"].ToString());
                decimal kol = Convert.ToDecimal(DTmaloprodaja.Rows[m]["kolicina"].ToString().Replace(".", ","));

                maloprodaja_mpc_uk = (nbc * kol) + maloprodaja_mpc_uk;
                ime_dokumenta8 = "MALOPRODAJA";
            }

            DataRow DTrow = dSRlisteTekst.Tables[0].NewRow();

            if (nbc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta1.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = nbc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - nbc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_ulaz1 != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = uslo_iz1.ToString();
                DTrow["string8"] = msklad_ulaz1.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = msklad_ulaz1.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (msklad_izlaz != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = izaslo_iz.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = msklad_izlaz.ToString("#0.00");
                DTrow["string6"] = (0 - msklad_izlaz).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (kalk_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumetna2.ToString();
                DTrow["string8"] = kalk_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = kalk_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (otpr_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta3.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = otpr_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - otpr_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (radnal_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
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
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta5.ToString();
                DTrow["string8"] = pocsta_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = pocsta_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (povdob_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta6.ToString();
                DTrow["string8"] = povdob_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = povdob_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (primka_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta7.ToString();
                DTrow["string8"] = primka_mpc_uk.ToString("#0.00");
                DTrow["string7"] = "0";
                DTrow["string6"] = primka_mpc_uk.ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            if (maloprodaja_mpc_uk != 0)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = broj_skladista.ToString();
                DTrow["string4"] = NAZIV_SKLADISTA.ToString();
                DTrow["string9"] = ime_dokumenta8.ToString();
                DTrow["string8"] = "0";
                DTrow["string7"] = maloprodaja_mpc_uk.ToString("#0.00");
                DTrow["string6"] = (0 - maloprodaja_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
            }

            decimal stanje1 = 0;
            decimal stanje2 = 0;
            decimal stanje3 = 0;
            decimal stanje4 = radnal_mpc_uk;
            decimal stanje5 = 0;
            stanje1 = (0 - msklad_izlaz);//skladiste izlaz - stanje
            stanje2 = (0 - nbc_uk); //Fakture izlaz - stanje
            stanje3 = (0 - otpr_mpc_uk); // otpremnice - izlaz
            stanje5 = (0 - maloprodaja_mpc_uk);

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
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 1)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 2)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 3)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 4)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 5)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 6)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 7)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }
            else if (brojac == 8)
            {
                DTrow = dSRlisteTekst.Tables[0].NewRow();
                DTrow["string5"] = "UKUPNO :";
                DTrow["string4"] = "Skladiste " + " " + broj_skladista.ToString();
                DTrow["string9"] = NAZIV_SKLADISTA.ToString();
                DTrow["string8"] = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                DTrow["string7"] = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz).ToString("#0.00");
                DTrow["string6"] = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk).ToString("#0.00");
                dSRlisteTekst.Tables[0].Rows.Add(DTrow);
                zbroj = (stanje5 + stanje1 + stanje2 + stanje3 + msklad_ulaz1 + kalk_mpc_uk + stanje4 + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);

                zbroj_ulaz = (msklad_ulaz1 + kalk_mpc_uk + radnal_mpc_uk + pocsta_mpc_uk + povdob_mpc_uk + primka_mpc_uk);
                zbroj_izlaz = (maloprodaja_mpc_uk + nbc_uk + otpr_mpc_uk + msklad_izlaz);
            }

            DTrow = dSRlisteTekst.Tables[0].NewRow();
            DTrow["string5"] = "";
            DTrow["string4"] = "";
            DTrow["string9"] = "";
            DTrow["string8"] = "";
            DTrow["string7"] = "";
            DTrow["string6"] = "";
            dSRlisteTekst.Tables[0].Rows.Add(DTrow);

            sveukupno = zbroj;
            string imereporta = "za nabavne cijene";
            string sqln = " SELECT " +
                " '" + sveukupno.ToString("0.00") + "' As cijena9 ," +
                " '" + zbroj_ulaz.ToString("0.00") + "' As cijena8 ," +
                " '" + zbroj_izlaz.ToString("0.00") + "' As cijena7 ," +
                " '" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "' As datum1 ," +
                " '" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "' As datum2 ," +
                " '" + imereporta + "' As jmj " +
                "";
            classSQL.CeAdatpter(sqln).Fill(dSRliste, "DTliste");
        }

        private void Sintetika_po_nabavnim_cijenama_sva_skladista()
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
            DataTable DTmaloprodaja = new DataTable();

            string skla_kalk = "";
            string skla_msklad_iz = "";
            string skla_msklad_ul = "";
            string skla_fak = "";
            string skla_otpr = "";
            string skla_radnal = "";
            string skla_pocsta = "";
            string skla_povdob = "";
            string skla_primka = "";
            string skla_maloprodaja = "";

            if (bool1 == true)
            {
                skla_kalk = "AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ";
                skla_msklad_iz = "AND meduskladisnica.id_skladiste_od = '" + skladiste_odabir + "' ";
                skla_msklad_ul = "AND meduskladisnica.id_skladiste_do = '" + skladiste_odabir + "' ";
                skla_fak = "AND faktura_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_otpr = "AND otpremnice.id_skladiste = '" + skladiste_odabir + "' ";
                skla_radnal = "AND radni_nalog_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocsta = "AND pocetno.id_skladiste = '" + skladiste_odabir + "' ";
                skla_povdob = "AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ";
                skla_primka = "AND primka.id_skladiste = '" + skladiste_odabir + "' ";
                skla_maloprodaja = "AND racun_stavke.id_skladiste = '" + skladiste_odabir + "' ";
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
                skla_maloprodaja = "";
            }

            //string filter = skla ;

            //if (filter.Length != 0)
            //{
            //filter = " WHERE " + filter;
            //filter = filter.Remove(filter.Length + 20 , 8);
            //}

            foreach (DataRow row in DTskl.Rows)
            {
                DTizlazna_fak = classSQL.select("SELECT faktura_stavke.nbc, faktura_stavke.id_skladiste, faktura_stavke.kolicina FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa  LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra WHERE " + query_samo_robno + query_samo_robno_dodatak_za_fakturu + " fakture.oduzmi_iz_skladista='1' AND fakture.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND fakture.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND faktura_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_fak + "  ", "Izlazne_fakture").Tables[0];
                MS_ulaz = classSQL.select("SELECT meduskladisnica_stavke.nbc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND meduskladisnica.id_skladiste_do='" + row["id_skladiste"].ToString() + "' " + skla_msklad_ul + " ", "Meduskladisnica_ulaz").Tables[0];
                MS_izlaz = classSQL.select("SELECT meduskladisnica_stavke.nbc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND meduskladisnica.id_skladiste_od='" + row["id_skladiste"].ToString() + "' " + skla_msklad_iz + " ", "Meduskladisnica_izlaz").Tables[0];
                DTkalk = classSQL.select("SELECT kalkulacija_stavke.fak_cijena,kalkulacija_stavke.rabat, kalkulacija_stavke.id_skladiste , kalkulacija_stavke.kolicina FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ", "Kalkulacija").Tables[0];
                DTOtpremnica = classSQL.select("SELECT otpremnica_stavke.nbc, otpremnica_stavke.id_skladiste, otpremnica_stavke.kolicina FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice AND otpremnice.id_skladiste = otpremnica_stavke.id_skladiste WHERE  otpremnice.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND otpremnice.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND otpremnice.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_otpr + "  ", "Otpremnice").Tables[0];
                DTradni_nalog = classSQL.select("SELECT radni_nalog_stavke.nbc, radni_nalog_stavke.id_skladiste, radni_nalog_stavke.kolicina FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga WHERE  radni_nalog.datum_naloga >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND radni_nalog.datum_naloga <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND radni_nalog_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_radnal + "  ", "radni_nalog").Tables[0];
                //DTpocetno_stanje = classSQL.select("SELECT pocetno_stanje_stavke.nbc, pocetno_stanje.id_skladiste, pocetno_stanje_stavke.kolicina FROM pocetno_stanje_stavke LEFT JOIN pocetno_stanje ON pocetno_stanje.broj = pocetno_stanje_stavke.broj WHERE  pocetno_stanje.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND pocetno_stanje.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND pocetno_stanje.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + "  ", "pocetno_stanje").Tables[0];

                DTpocetno_stanje = classSQL.select("SELECT pocetno.nbc, pocetno.id_skladiste, pocetno.kolicina, pocetno.porez FROM pocetno WHERE pocetno.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND pocetno.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND pocetno.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + "  ", "pocetno_stanje").Tables[0];

                DTpovratnica_dobavljacu = classSQL.select("SELECT povrat_robe_stavke.nbc, povrat_robe.id_skladiste, povrat_robe_stavke.kolicina FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj WHERE  povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND povrat_robe.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_povdob + "  ", "povrat_robe").Tables[0];
                DTprimka = classSQL.select("SELECT primka_stavke.nbc, primka.id_skladiste, primka_stavke.kolicina FROM primka_stavke LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka WHERE  primka.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND primka.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND primka.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_primka + "  ", "primka").Tables[0];
                DTmaloprodaja = classSQL.select("SELECT racun_stavke.nbc, racun_stavke.id_skladiste, racun_stavke.kolicina FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE " + query_samo_robno + " racuni.datum_racuna >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND racun_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_maloprodaja + "  ", "primka").Tables[0];

                SETizracunnabavne(DTizlazna_fak, MS_ulaz, MS_izlaz, DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString(), DTOtpremnica, DTradni_nalog, DTpocetno_stanje, DTpovratnica_dobavljacu, DTprimka, DTmaloprodaja);
            }
        }

        private void Sintetika_po_prodajnim_cijenama_sva_skladista()
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
            DataTable DTmaloprodaja = new DataTable();
            DataTable DTpocetno = new DataTable();

            string skla_kalk = "";
            string skla_msklad_iz = "";
            string skla_msklad_ul = "";
            string skla_fak = "";
            string skla_otpr = "";
            string skla_radnal = "";
            string skla_pocsta = "";
            string skla_povdob = "";
            string skla_primka = "";
            string skla_maloprodaja = "";
            string skla_pocetno = "";

            if (bool1 == true)
            {
                skla_kalk = "AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ";
                skla_msklad_iz = "AND meduskladisnica.id_skladiste_od = '" + skladiste_odabir + "' ";
                skla_msklad_ul = "AND meduskladisnica.id_skladiste_do = '" + skladiste_odabir + "' ";
                skla_fak = "AND faktura_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_otpr = "AND otpremnice.id_skladiste = '" + skladiste_odabir + "' ";
                skla_radnal = "AND radni_nalog_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocsta = "AND pocetno.id_skladiste = '" + skladiste_odabir + "' ";
                skla_povdob = "AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ";
                skla_primka = "AND primka.id_skladiste = '" + skladiste_odabir + "' ";
                skla_maloprodaja = "AND racun_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocetno = "AND pocetno.id_skladiste = '" + skladiste_odabir + "' ";
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
                skla_maloprodaja = "";
                skla_pocetno = "";
            }

            //string filter = skla ;

            //if (filter.Length != 0)
            //{
            //filter = " WHERE " + filter;
            //filter = filter.Remove(filter.Length + 20 , 8);
            //}

            /*
             UPDATE pocetno SET prodajna_cijena=
            (SELECT ROUND((vpc+(vpc*CAST(REPLACE(porez,',','.') AS numeric)/100)),2) FROM roba_prodaja WHERE roba_prodaja.id_skladiste=pocetno.id_skladiste AND roba_prodaja.sifra=pocetno.sifra LIMIT 1)
            */

            foreach (DataRow row in DTskl.Rows)
            {
                DTizlazna_fak = classSQL.select("SELECT faktura_stavke.vpc, faktura_stavke.id_skladiste, faktura_stavke.rabat, faktura_stavke.kolicina, faktura_stavke.porez FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa  LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra WHERE " + query_samo_robno + query_samo_robno_dodatak_za_fakturu + " fakture.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND fakture.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND faktura_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_fak + "  ", "Izlazne_fakture").Tables[0];
                int gg = DTizlazna_fak.Rows.Count;

                MS_ulaz = classSQL.select("SELECT meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica_stavke.pdv , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND meduskladisnica.id_skladiste_do='" + row["id_skladiste"].ToString() + "' " + skla_msklad_ul + " ", "Meduskladisnica_ulaz").Tables[0];
                MS_izlaz = classSQL.select("SELECT meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica_stavke.pdv , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND meduskladisnica.id_skladiste_od='" + row["id_skladiste"].ToString() + "' " + skla_msklad_iz + " ", "Meduskladisnica_izlaz").Tables[0];
                string sql1 = "SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "'  " + skla_kalk + " ";
                DTkalk = classSQL.select(sql1, "Kalkulacija").Tables[0];
                DTOtpremnica = classSQL.select("SELECT otpremnica_stavke.vpc, otpremnica_stavke.id_skladiste, otpremnica_stavke.rabat, otpremnica_stavke.kolicina, otpremnica_stavke.porez FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice AND otpremnice.id_skladiste = otpremnica_stavke.id_skladiste WHERE  otpremnice.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND otpremnice.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND otpremnice.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_otpr + "  ", "Otpremnice").Tables[0];
                DTradni_nalog = classSQL.select("SELECT radni_nalog_stavke.vpc, radni_nalog_stavke.id_skladiste, radni_nalog_stavke.kolicina, radni_nalog_stavke.porez FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga WHERE  radni_nalog.datum_naloga >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND radni_nalog.datum_naloga <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND radni_nalog_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_radnal + "  ", "radni_nalog").Tables[0];
                //DTpocetno_stanje = classSQL.select("SELECT pocetno_stanje_stavke.mpc, pocetno_stanje.id_skladiste, pocetno_stanje_stavke.kolicina, pocetno_stanje_stavke.pdv FROM pocetno_stanje_stavke LEFT JOIN pocetno_stanje ON pocetno_stanje.broj = pocetno_stanje_stavke.broj WHERE  pocetno_stanje.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND pocetno_stanje.date <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND pocetno_stanje.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + "  ", "pocetno_stanje").Tables[0];
                DTpovratnica_dobavljacu = classSQL.select("SELECT povrat_robe_stavke.vpc, povrat_robe.id_skladiste, povrat_robe_stavke.kolicina, povrat_robe_stavke.rabat, povrat_robe_stavke.pdv FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj WHERE  povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND povrat_robe.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_povdob + "  ", "povrat_robe").Tables[0];
                DTprimka = classSQL.select("SELECT primka_stavke.nbc, primka_stavke.vpc,  primka_stavke.rabat,  primka_stavke.pdv, primka.id_skladiste, primka_stavke.kolicina FROM primka_stavke LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka WHERE  primka.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND primka.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND primka.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_primka + "  ", "primka").Tables[0];
                DTmaloprodaja = classSQL.select("SELECT racun_stavke.vpc, racun_stavke.rabat, racun_stavke.porez, racun_stavke.id_skladiste, racun_stavke.kolicina FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna=racun_stavke.broj_racuna AND racuni.id_ducan=racun_stavke.id_ducan AND racuni.id_kasa=racun_stavke.id_kasa LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE " + query_samo_robno + " racuni.datum_racuna >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND racun_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_maloprodaja + "  ", "primka").Tables[0];

                DTpocetno_stanje = classSQL.select("SELECT pocetno.prodajna_cijena as mpc, pocetno.id_skladiste, pocetno.kolicina, pocetno.porez FROM pocetno WHERE pocetno.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND pocetno.datum <= '" + datumDO.ToString("dd-MM-yyyy 23:59:59") + "' AND pocetno.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + "  ", "pocetno_stanje").Tables[0];

                //DTpocetno_stanje = classSQL.select("SELECT pocetno.prodajna_cijena/(1+(pocetno.porez/100)) as vpc, pocetno.id_skladiste, pocetno.kolicina , pocetno.porez FROM pocetno WHERE", "Pocetno

                SETizracun(DTizlazna_fak, MS_ulaz, MS_izlaz, DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString(), DTOtpremnica, DTradni_nalog, DTpocetno_stanje, DTpovratnica_dobavljacu, DTprimka, DTmaloprodaja);
            }
        }

        private void Sintetika_po_skladisnim_cijenama_sva_skladista()
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
            DataTable DTmaloprodaja = new DataTable();

            string skla_kalk = "";
            string skla_msklad_iz = "";
            string skla_msklad_ul = "";
            string skla_fak = "";
            string skla_otpr = "";
            string skla_radnal = "";
            string skla_pocsta = "";
            string skla_povdob = "";
            string skla_primka = "";
            string skla_maloprodaja = "";

            if (bool1 == true)
            {
                skla_kalk = "AND kalkulacija.id_skladiste = '" + skladiste_odabir + "' ";
                skla_msklad_iz = "AND meduskladisnica.id_skladiste_od = '" + skladiste_odabir + "' ";
                skla_msklad_ul = "AND meduskladisnica.id_skladiste_do = '" + skladiste_odabir + "' ";
                skla_fak = "AND faktura_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_otpr = "AND otpremnice.id_skladiste = '" + skladiste_odabir + "' ";
                skla_radnal = "AND radni_nalog_stavke.id_skladiste = '" + skladiste_odabir + "' ";
                skla_pocsta = "AND pocetno.id_skladiste = '" + skladiste_odabir + "' ";
                skla_povdob = "AND povrat_robe.id_skladiste = '" + skladiste_odabir + "' ";
                skla_primka = "AND primka.id_skladiste = '" + skladiste_odabir + "' ";
                skla_maloprodaja = "AND racun_stavka.id_skladiste = '" + skladiste_odabir + "' ";
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

            //string filter = skla ;

            //if (filter.Length != 0)
            //{
            //filter = " WHERE " + filter;
            //filter = filter.Remove(filter.Length + 20 , 8);
            //}

            foreach (DataRow row in DTskl.Rows)
            {
                DTizlazna_fak = classSQL.select("SELECT faktura_stavke.vpc, faktura_stavke.id_skladiste, faktura_stavke.rabat, faktura_stavke.kolicina, faktura_stavke.porez FROM faktura_stavke LEFT JOIN fakture ON fakture.broj_fakture = faktura_stavke.broj_fakture AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa  LEFT JOIN roba ON roba.sifra = faktura_stavke.sifra WHERE " + query_samo_robno + query_samo_robno_dodatak_za_fakturu + "  fakture.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND fakture.date <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND faktura_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_fak + "  ", "Izlazne_fakture").Tables[0];
                MS_ulaz = classSQL.select("SELECT meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica_stavke.pdv , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND meduskladisnica.id_skladiste_do='" + row["id_skladiste"].ToString() + "' " + skla_msklad_ul + " ", "Meduskladisnica_ulaz").Tables[0];
                MS_izlaz = classSQL.select("SELECT meduskladisnica_stavke.vpc, meduskladisnica_stavke.iz_skladista, meduskladisnica_stavke.kolicina , meduskladisnica_stavke.pdv , meduskladisnica.id_skladiste_od , meduskladisnica.id_skladiste_do FROM meduskladisnica_stavke LEFT JOIN meduskladisnica ON meduskladisnica.broj = meduskladisnica_stavke.broj AND meduskladisnica.id_skladiste_od=meduskladisnica_stavke.iz_skladista WHERE meduskladisnica.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND meduskladisnica.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND meduskladisnica.id_skladiste_od='" + row["id_skladiste"].ToString() + "' " + skla_msklad_iz + " ", "Meduskladisnica_izlaz").Tables[0];
                DTkalk = classSQL.select("SELECT kalkulacija_stavke.vpc, kalkulacija_stavke.id_skladiste, kalkulacija_stavke.rabat, kalkulacija_stavke.kolicina, kalkulacija_stavke.porez FROM kalkulacija_stavke LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste WHERE kalkulacija.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND kalkulacija.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND kalkulacija.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_kalk + " ", "Kalkulacija").Tables[0];
                DTOtpremnica = classSQL.select("SELECT otpremnica_stavke.vpc, otpremnica_stavke.id_skladiste, otpremnica_stavke.rabat, otpremnica_stavke.kolicina, otpremnica_stavke.porez FROM otpremnica_stavke LEFT JOIN otpremnice ON otpremnice.broj_otpremnice = otpremnica_stavke.broj_otpremnice AND otpremnice.id_skladiste = otpremnica_stavke.id_skladiste WHERE  otpremnice.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND otpremnice.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND otpremnice.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_otpr + "  ", "Otpremnice").Tables[0];
                DTradni_nalog = classSQL.select("SELECT radni_nalog_stavke.vpc, radni_nalog_stavke.id_skladiste, radni_nalog_stavke.kolicina, radni_nalog_stavke.porez FROM radni_nalog_stavke LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga WHERE  radni_nalog.datum_naloga >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND radni_nalog.datum_naloga <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND radni_nalog_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_radnal + "  ", "radni_nalog").Tables[0];
                DTpocetno_stanje = classSQL.select("SELECT pocetno_stanje_stavke.mpc, pocetno_stanje.id_skladiste, pocetno_stanje_stavke.kolicina, pocetno_stanje_stavke.pdv FROM pocetno_stanje_stavke LEFT JOIN pocetno_stanje ON pocetno_stanje.broj = pocetno_stanje_stavke.broj WHERE  pocetno_stanje.date >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND pocetno_stanje.date <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND pocetno_stanje.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_pocsta + "  ", "pocetno_stanje").Tables[0];
                DTpovratnica_dobavljacu = classSQL.select("SELECT povrat_robe_stavke.vpc, povrat_robe.id_skladiste, povrat_robe_stavke.kolicina, povrat_robe_stavke.rabat, povrat_robe_stavke.pdv FROM povrat_robe_stavke LEFT JOIN povrat_robe ON povrat_robe.broj = povrat_robe_stavke.broj WHERE  povrat_robe.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND povrat_robe.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND povrat_robe.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_povdob + "  ", "povrat_robe").Tables[0];
                DTprimka = classSQL.select("SELECT primka_stavke.nbc, primka_stavke.vpc,  primka_stavke.rabat,  primka_stavke.pdv, primka.id_skladiste, primka_stavke.kolicina FROM primka_stavke LEFT JOIN primka ON primka.id_primka = primka_stavke.id_primka WHERE  primka.datum >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND primka.datum <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND primka.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_primka + "  ", "primka").Tables[0];
                DTmaloprodaja = classSQL.select("SELECT racun_stavke.vpc, racun_stavke.rabat, racun_stavke.porez, racun_stavke.id_skladiste, racun_stavke.kolicina FROM racun_stavke LEFT JOIN racuni ON racuni.broj_racuna = racun_stavke.broj_racuna  LEFT JOIN roba ON roba.sifra = racun_stavke.sifra_robe WHERE " + query_samo_robno + "  racuni.datum_racuna >= '" + datumOD.ToString("dd-MM-yyyy 00:00:00") + "' AND racuni.datum_racuna <= '" + datumDO.ToString("dd-MM-yyyy") + "' AND racun_stavke.id_skladiste='" + row["id_skladiste"].ToString() + "' " + skla_maloprodaja + "  ", "primka").Tables[0];
                SETizracunskladnisne(DTizlazna_fak, MS_ulaz, MS_izlaz, DTkalk, row["skladiste"].ToString(), row["id_skladiste"].ToString(), DTOtpremnica, DTradni_nalog, DTpocetno_stanje, DTpovratnica_dobavljacu, DTprimka, DTmaloprodaja);
            }
        }
    }
}