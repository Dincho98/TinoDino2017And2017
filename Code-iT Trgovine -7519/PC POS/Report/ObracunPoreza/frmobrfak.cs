using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Report.ObracunPoreza
{
    public partial class frmobrfak : Form
    {
        public frmobrfak()
        {
            InitializeComponent();
        }

        public string documenat { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokumenat { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public string kasa { get; set; }
        public string godina { get; set; }
        public string racunodd { get; set; }
        public string racundoo { get; set; }
        public string racunOD { get; set; }
        public string racunDO { get; set; }
        public Boolean ducbool { get; set; }
        public Boolean kasbool { get; set; }
        public Boolean blabool { get; set; }
        public Boolean racbool { get; set; }
        public Boolean datbool { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }
        private double u = 0;

        public string BrojFakOD { get; set; }
        public string BrojFakDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            obracunporeza_fak();

            if (documenat == "POVRATNICA")
            {
                obracunporeza_fak();
                //this.Text = ImeForme;
            }

            this.reportViewer1.RefreshReport();
        }

        private DataTable DTartikli = new DataTable();
        private DataRow RowPdv;
        private DataRow RowOsnovica;
        private DataRow RowArtikl;

        private void obracunporeza_fak()
        {
            //if (DTpdv.Columns["porez"] == null)
            //{
            //    DTpdv.Columns.Add("porez");
            //    DTpdv.Columns.Add("iznos");
            //    DTpdv.Columns.Add("nacin");
            //    DTpdv.Columns.Add("osnovica");
            //}
            //else
            //{
            //    DTpdv.Clear();
            //}

            string naziv_fakture = " podaci_tvrtka.naziv_fakture,";
            string sql1 = SqlPodaciTvrtke.VratiSql("", naziv_fakture, "");
            classSQL.CeAdatpter(sql1).Fill(dSRpodaciTvrtke, "DTRpodaciTvrtke");

            frmObracunporeza obrporez = new frmObracunporeza();

            string duc = "";
            if (ducbool == true)
            {
                duc = " fakture.id_ducan ='" + ducan + "'  AND ";
            }
            else
            {
                duc = "";
            }

            string kas = "";
            if (kasbool == true)
            {
                kas = "fakture.id_kasa ='" + kasa + "'  AND ";
            }
            else
            {
                kas = "";
            }

            string datOD = "";
            string datODrez = "";
            //if (datbool == true)
            //{
            datOD = " fakture.datedvo >='" + datumOD.AddDays(0).ToString("yyyy-MM-dd 00:00:00") + "'  AND ";
            datODrez = "'" + datumOD.AddDays(0).ToString("yyyy-MM-dd") + "'  As datum2 ,";
            //}

            //else
            //{
            //    datOD = "";
            //}

            string datDO = "";
            string datDOrez = "";
            //if (datbool == true)
            //{
            datDO = "fakture.datedvo <='" + datumDO.AddDays(0).ToString("yyyy-MM-dd 23:59:59") + "'  AND ";
            datDOrez = "'" + datumDO.AddDays(0).ToString("yyyy-MM-dd") + "'  As datum3 ,";
            //}
            //else
            //{
            //    datDO = "";
            //}

            string racOD = "";
            string racodd = "";
            if (racbool == true)
            {
                racOD = " CAST(fakture.broj_fakture AS numeric) >='" + racunOD + "'  AND ";
                racodd = " '" + racunodd + "' As string3 ,";
            }
            else
            {
                racOD = "";
            }

            string racDO = "";
            string racdoo = "";
            if (racbool == true)
            {
                racDO = " CAST(fakture.broj_fakture AS numeric) <='" + racunDO + "'  AND ";
                racdoo = " '" + racundoo + "' As string4 ,";
            }
            else
            {
                racDO = "";
            }

            string bla = "";
            string blag = "";
            if (blabool == true)
            {
                bla = "fakture.id_zaposlenik_izradio ='" + blagajnik + "'  AND ";
                blag = " '" + blagajnik + "' As string5 ,";
            }
            else
            {
                bla = "";
            }

            string filter = datOD + datDO + racOD + racDO + kas + bla + duc;

            if (filter.Length != 0)
            {
                filter = " WHERE " + filter;
                filter = filter.Remove(filter.Length - 4, 4);
            }

            string sql_liste = "SELECT " +
                " faktura_stavke.porez As porezroba," +
                " faktura_stavke.rabat As rabat ," +
                " faktura_stavke.kolicina ," +
                " faktura_stavke.vpc ," +
                " faktura_stavke.vpc * (1 zbroj (CAST(replace(faktura_stavke.porez,',','.') as numeric)/100)) as mpc, " + blag + racodd + racdoo +
                " fakture.date As datum1," + datODrez + datDOrez +
                " roba.oduzmi," +
                " fakture.ukupno," +
                " faktura_stavke.porez_potrosnja," +
                " faktura_stavke.povratna_naknada" +
                " FROM faktura_stavke" +
                " LEFT JOIN roba ON roba.sifra=faktura_stavke.sifra " +
                " LEFT JOIN fakture ON fakture.broj_fakture=faktura_stavke.broj_fakture " +
                " AND fakture.id_ducan=faktura_stavke.id_ducan AND fakture.id_kasa=faktura_stavke.id_kasa" +
                " " + filter;

            classSQL.NpgAdatpter(sql_liste).Fill(dSRlisteTekst, "DTlisteTekst");
            if (dSRlisteTekst.Tables[0].Rows.Count < 1)
            {
                this.Hide();
                MessageBox.Show("Za taj upit ne postoji ni jedan zapis !");
                this.Close();
            }

            //string sqltxt1 = " SELECT MAX(CAST(broj_racuna AS INTEGER)) AS string3," +
            //" MIN(CAST(broj_racuna AS INTEGER)) As string4 " +
            //" FROM racuni";

            //classSQL.NpgAdatpter(sqltxt1).Fill(dSRlisteTekst, "DTlisteTekst");

            //MessageBox.Show(dSRlisteTekst.Tables[0].Rows.Count.ToString());

            DataTable DTstavke;
            DTstavke = classSQL.select(sql_liste, "faktura_stavke").Tables[0];
            DataTable DTstavke1;
            DTstavke1 = classSQL.select(sql_liste, "faktura_stavke").Tables[0];
            DataTable DTstavke2;
            DTstavke2 = classSQL.select(sql_liste, "faktura_stavke").Tables[0];

            decimal Stopa = 0;
            decimal iznos_bez_poreza = 0;
            decimal osnovica_ukupno = 0;
            decimal rabat_iznos = 0;
            decimal pdv_rabat = 0;
            decimal pdv_osnovica = 0;
            decimal pdv_iznos = 0;
            decimal Stopa1 = 0;
            decimal iznos_bez_poreza1 = 0;
            decimal osnovica_ukupno1 = 0;
            decimal pdv_rabat1 = 0;
            decimal pdv_osnovica1 = 0;
            decimal pdv_iznos1 = 0;
            decimal povratna_naknada = 0;
            decimal povratna_naknada1 = 0;
            decimal pnp = 0;
            decimal porez_na_potrosnju = 0;
            decimal sveukupno1 = 0;
            decimal sveukupno2 = 0;
            decimal porez_na_potrosnju1 = 0;
            decimal rabat_iznos1 = 0;

            for (int y = 0; y < DTstavke.Rows.Count; y++)
            {
                //MessageBox.Show(DTstavke.Rows[y]["povratna_naknada"].ToString());
                decimal pdv = Convert.ToDecimal(DTstavke.Rows[y]["porezroba"].ToString());
                decimal vpc = Convert.ToDecimal(DTstavke.Rows[y]["vpc"].ToString());
                decimal rabat = Convert.ToDecimal(DTstavke.Rows[y]["rabat"].ToString());
                decimal kol = Convert.ToDecimal(DTstavke.Rows[y]["kolicina"].ToString());
                decimal uku = Convert.ToDecimal(DTstavke.Rows[y]["ukupno"].ToString());
                decimal mpc = Convert.ToDecimal(DTstavke.Rows[y]["mpc"].ToString());

                if (DTstavke.Rows[y]["povratna_naknada"].ToString() != "")
                {
                    povratna_naknada = Convert.ToDecimal(DTstavke.Rows[y]["povratna_naknada"].ToString());
                }
                else
                {
                    povratna_naknada = 0;
                }

                if (DTstavke.Rows[y]["porez_potrosnja"].ToString() != "")
                {
                    pnp = Convert.ToDecimal(DTstavke.Rows[y]["porez_potrosnja"].ToString());
                }
                else
                {
                    pnp = 0;
                }

                if (DTstavke.Rows[y]["oduzmi"].ToString() == "DA")
                {
                    if (DTstavke.Rows[y]["povratna_naknada"].ToString() != "")
                    {
                        povratna_naknada = Convert.ToDecimal(DTstavke.Rows[y]["povratna_naknada"].ToString());
                    }
                    else
                    {
                        povratna_naknada = 0;
                    }
                    sveukupno1 = mpc * kol;
                    Stopa = (pdv * 100) / (100 + pdv);
                    rabat_iznos = (mpc * (rabat / 100)) * kol;
                    pdv_rabat = (rabat_iznos * (Stopa / 100));
                    osnovica_ukupno = (mpc - (mpc * (rabat / 100))) * kol;
                    porez_na_potrosnju = pnp;
                    pdv_iznos = osnovica_ukupno * (Stopa / 100);
                    iznos_bez_poreza = osnovica_ukupno - pdv_iznos;

                    StopePDVa(pdv, Stopa, sveukupno1, povratna_naknada, rabat_iznos, pdv_rabat, osnovica_ukupno, pdv_iznos, porez_na_potrosnju, iznos_bez_poreza);
                }
                else
                {
                    if (DTstavke.Rows[y]["povratna_naknada"].ToString() != "")
                    {
                        povratna_naknada1 = Convert.ToDecimal(DTstavke.Rows[y]["povratna_naknada"].ToString());
                    }
                    else
                    {
                        povratna_naknada1 = 0;
                    }
                    sveukupno2 = mpc * kol;
                    Stopa1 = (pdv * 100) / (100 + pdv);
                    rabat_iznos1 = (mpc * (rabat / 100)) * kol;
                    pdv_rabat1 = (rabat_iznos1 * (Stopa / 100));
                    osnovica_ukupno1 = ((mpc - (mpc * (rabat / 100))) * kol);
                    porez_na_potrosnju1 = pnp;
                    pdv_iznos1 = osnovica_ukupno1 * (Stopa1 / 100);
                    iznos_bez_poreza1 = osnovica_ukupno1 - pdv_iznos1;

                    //StopePDVa(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                    StopePDVa1(pdv, Stopa1, sveukupno2, povratna_naknada1, rabat_iznos1, pdv_rabat, osnovica_ukupno1, pdv_iznos1, porez_na_potrosnju1, iznos_bez_poreza1);
                }
            }

            rekapitulacija();
        }

        private Dataset.DSobracunpor.DTobr1DataTable DTpdv = new Dataset.DSobracunpor.DTobr1DataTable();

        private void StopePDVa(decimal pdv, decimal stopa, decimal sveukupno, decimal povratna_naknada, decimal rabat, decimal pdv_rabat, decimal mpc, decimal pdv_iznos, decimal pnp, decimal iznosBezPoreza)
        {
            DataRow[] dataROW = dSobracunpor.Tables[0].Select("porezroba = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSobracunpor.Tables[0].NewRow();
                RowPdv["porezroba"] = pdv.ToString();
                RowPdv["stopa"] = stopa.ToString("#0.00");
                RowPdv["sveukup"] = sveukupno.ToString("#0.00");
                RowPdv["povrnakn"] = povratna_naknada.ToString("#0.00");
                RowPdv["rabat"] = rabat.ToString("#0.00");
                RowPdv["pdvrabat"] = pdv_rabat.ToString("#0.00");
                RowPdv["osnovica"] = mpc.ToString("#0.00");
                RowPdv["pdv"] = pdv_iznos.ToString("#0.00");
                RowPdv["pp"] = pnp.ToString("#0.00");
                RowPdv["iznbezpor"] = iznosBezPoreza.ToString("#0.00");
                dSobracunpor.Tables[0].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["sveukup"] = (Convert.ToDecimal(dataROW[0]["sveukup"]) + sveukupno).ToString("#0.00");
                dataROW[0]["povrnakn"] = (Convert.ToDecimal(dataROW[0]["povrnakn"]) + povratna_naknada).ToString("#0.00");
                dataROW[0]["osnovica"] = (Convert.ToDecimal(dataROW[0]["osnovica"]) + mpc).ToString("#0.00");
                dataROW[0]["rabat"] = (Convert.ToDecimal(dataROW[0]["rabat"]) + rabat).ToString("#0.00");
                dataROW[0]["pdvrabat"] = (Convert.ToDecimal(dataROW[0]["pdvrabat"]) + pdv_rabat).ToString("#0.00");
                dataROW[0]["pdv"] = (Convert.ToDecimal(dataROW[0]["pdv"]) + pdv_iznos).ToString("#0.00");
                dataROW[0]["pp"] = (Convert.ToDecimal(dataROW[0]["pp"]) + pnp).ToString("#0.00");
                dataROW[0]["iznbezpor"] = (Convert.ToDecimal(dataROW[0]["iznbezpor"]) + iznosBezPoreza).ToString("#0.00");
            }
        }

        private Dataset.DSobracunpor1.DTobr2DataTable DTpdv1 = new Dataset.DSobracunpor1.DTobr2DataTable();

        private void StopePDVa1(decimal pdv, decimal stopa, decimal sveukupno, decimal povratna_naknada, decimal rabat, decimal pdv_rabat, decimal mpc, decimal pdv_iznos, decimal pnp, decimal iznosBezPoreza)
        {
            DataRow[] dataROW = dSobracunpor1.Tables[0].Select("porez = '" + pdv.ToString() + "'");

            if (dataROW.Count() == 0)
            {
                RowPdv = dSobracunpor1.Tables[0].NewRow();
                RowPdv["porez"] = pdv.ToString();
                RowPdv["stopa"] = stopa.ToString("#0.00");
                RowPdv["sveukupno"] = sveukupno.ToString("#0.00");
                RowPdv["povrnak"] = povratna_naknada.ToString("#0.00");
                RowPdv["rabat"] = rabat.ToString("#0.00");
                RowPdv["pdvrabata"] = pdv_rabat.ToString("#0.00");
                RowPdv["osnovica"] = mpc.ToString("#0.00");
                RowPdv["pdv"] = pdv_iznos.ToString("#0.00");
                RowPdv["pp"] = pnp.ToString("#0.00");
                RowPdv["iznbezporeza"] = iznosBezPoreza.ToString("#0.00");
                dSobracunpor1.Tables[0].Rows.Add(RowPdv);
            }
            else
            {
                dataROW[0]["sveukupno"] = (Convert.ToDecimal(dataROW[0]["sveukupno"]) + sveukupno).ToString("#0.00");
                dataROW[0]["povrnak"] = (Convert.ToDecimal(dataROW[0]["povrnak"]) + povratna_naknada).ToString("#0.00");
                dataROW[0]["osnovica"] = (Convert.ToDecimal(dataROW[0]["osnovica"]) + mpc).ToString("#0.00");
                dataROW[0]["rabat"] = (Convert.ToDecimal(dataROW[0]["rabat"]) + rabat).ToString("#0.00");
                dataROW[0]["pdvrabata"] = (Convert.ToDecimal(dataROW[0]["pdvrabata"]) + pdv_rabat).ToString("#0.00");
                dataROW[0]["pdv"] = (Convert.ToDecimal(dataROW[0]["pdv"]) + pdv_iznos).ToString("#0.00");
                dataROW[0]["pp"] = (Convert.ToDecimal(dataROW[0]["pp"]) + pnp).ToString("#0.00");
                dataROW[0]["iznbezporeza"] = (Convert.ToDecimal(dataROW[0]["iznbezporeza"]) + iznosBezPoreza).ToString("#0.00");
            }
        }

        private Dataset.DSobracunpor2.DTobr3DataTable DTpdv2 = new Dataset.DSobracunpor2.DTobr3DataTable();

        private void rekapitulacija()
        {
            DataTable DTo = dSobracunpor.DTobr1;

            foreach (DataRow row in DTo.Rows)
            {
                DataRow[] dataROW = dSobracunpor1.Tables[0].Select("porez = '" + Convert.ToDecimal(row["porezroba"].ToString()).ToString() + "'");
                if (dataROW.Count() == 0)
                {
                    RowPdv = dSobracunpor2.Tables[0].NewRow();
                    RowPdv["porez"] = row["porezroba"].ToString();
                    RowPdv["stopa"] = row["stopa"].ToString();
                    RowPdv["sveukupno"] = row["sveukup"].ToString();
                    RowPdv["povrnak"] = row["povrnakn"].ToString();
                    RowPdv["rabat"] = row["rabat"].ToString();
                    RowPdv["pdvrabata"] = row["pdvrabat"].ToString();
                    RowPdv["osnovica"] = row["osnovica"].ToString();
                    RowPdv["pdv"] = row["pdv"].ToString();
                    RowPdv["pp"] = row["pp"].ToString();
                    RowPdv["iznbezpor"] = row["iznbezpor"].ToString();
                    dSobracunpor2.Tables[0].Rows.Add(RowPdv);
                }
                else
                {
                    RowPdv = dSobracunpor2.Tables[0].NewRow();
                    RowPdv["sveukupno"] = Convert.ToDecimal(Convert.ToDecimal(row["sveukup"].ToString()) + Convert.ToDecimal(dataROW[0]["sveukupno"].ToString())).ToString("#0.00");
                    RowPdv["povrnak"] = Convert.ToDecimal(Convert.ToDecimal(row["povrnakn"].ToString()) + Convert.ToDecimal(dataROW[0]["povrnak"].ToString())).ToString("#0.00");
                    RowPdv["rabat"] = Convert.ToDecimal(Convert.ToDecimal(row["rabat"].ToString()) + Convert.ToDecimal(dataROW[0]["rabat"].ToString())).ToString("#0.00");
                    RowPdv["pdvrabata"] = Convert.ToDecimal(Convert.ToDecimal(row["pdvrabat"].ToString()) + Convert.ToDecimal(dataROW[0]["pdvrabata"].ToString())).ToString("#0.00");
                    RowPdv["osnovica"] = Convert.ToDecimal(Convert.ToDecimal(row["osnovica"].ToString()) + Convert.ToDecimal(dataROW[0]["osnovica"].ToString())).ToString("#0.00");
                    RowPdv["pdv"] = Convert.ToDecimal(Convert.ToDecimal(row["pdv"].ToString()) + Convert.ToDecimal(dataROW[0]["pdv"].ToString())).ToString("#0.00");
                    RowPdv["pp"] = Convert.ToDecimal(Convert.ToDecimal(row["pp"].ToString()) + Convert.ToDecimal(dataROW[0]["pp"].ToString())).ToString("#0.00");
                    RowPdv["iznbezpor"] = Convert.ToDecimal(Convert.ToDecimal(row["iznbezpor"].ToString()) + Convert.ToDecimal(dataROW[0]["iznbezporeza"].ToString())).ToString("#0.00");
                    dSobracunpor2.Tables[0].Rows.Add(RowPdv);
                }
            }
        }
    }
}