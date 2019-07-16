using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmKasaOpcije : Form
    {
        public frmKasaOpcije()
        {
            InitializeComponent();
        }

        private DataTable DTsetting;
        private DataTable DTsend;
        private DataTable DTpostavkePrinter;
        private DataSet DSpostavke;
        private DataTable DTpromocije1;
        private DataTable DSPartner;
        public string id_ducan { get; set; }
        public string id_kasa { get; set; }
        public string sifra_skladiste { get; set; }
        public string sifraPartnera { get; set; }
        public int rowCount { get; set; }

        private double ukupno = 0;
        private string brRac;
        private string blagajnik_ime;

        private void frmKasaOpcije_Load(object sender, EventArgs e)
        {
            DSpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke");

            if (DSpostavke.Tables[0].Rows.Count > 0)
            {
                id_kasa = DSpostavke.Tables[0].Rows[0]["default_blagajna"].ToString();
                id_ducan = DSpostavke.Tables[0].Rows[0]["default_ducan"].ToString();
                ////ovo je totalno krivo jer postavlja skladiste na default, tj. kod storna se vraća roba na
                ////default skladište!!!
                //sifra_skladiste = DSpostavke.Tables[0].Rows[0]["default_skladiste"].ToString();
            }

            DTpromocije1 = classSQL.select("SELECT * FROM promocija1", "promocija1").Tables[0];
            string sql = "SELECT ime + ' ' + prezime FROM zaposlenici WHERE id_zaposlenik = '" +
                Properties.Settings.Default.id_zaposlenik + "'";
            blagajnik_ime = classSQL.select(sql, "zaposlenici").Tables[0].Rows[0][0].ToString();
            DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

            ////zakomentirano, jer inače uzima partnera id default.id_partner. To znači da ako nije uopće odabran partner,
            ////na računu bude svejedno ispisan!
            //if (Properties.Settings.Default.id_partner != "")
            //{
            //    sql = "SELECT id_partner,ime_tvrtke FROM partners WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'";
            //    DSPartner = classSQL.select(sql, "partners").Tables[0];

            //    if (DSPartner.Rows.Count > 0)
            //    {
            //        sifraPartnera = DSPartner.Rows[0]["id_partner"].ToString();
            //    }
            //    else
            //    {
            //        sifraPartnera = "0";
            //    }
            //}
            //else
            //{
            //    sifraPartnera = "1";
            //}

            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w + 3, h + 3);
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            Button PB = ((Button)sender);
            int w = PB.Size.Width;
            int h = PB.Size.Height;
            PB.Size = new System.Drawing.Size(w - 3, h - 3);
        }

        private string barcode = "";
        private string sifra = "";

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni WHERE id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];

            if (DSbr.Rows.Count > 0)
            {
                IspisRacuna(DSbr.Rows[0][0].ToString());
            }
            else
            {
                MessageBox.Show("U bazi ne postoje računi!", "Upozorenje!");
            }
        }

        private void IspisRacuna(string broj)
        {
            DTsend = new DataTable();
            DTsend.Columns.Add("broj_racuna");
            DTsend.Columns.Add("sifra_robe");
            DTsend.Columns.Add("id_skladiste");
            DTsend.Columns.Add("mpc");
            DTsend.Columns.Add("porez");
            DTsend.Columns.Add("kolicina");
            DTsend.Columns.Add("rabat");
            DTsend.Columns.Add("cijena");
            DTsend.Columns.Add("porez_potrosnja");
            DTsend.Columns.Add("ime");
            DTsend.Columns.Add("vpc");
            DTsend.Columns.Add("povratna_naknada");

            DTsend.Columns.Add("porez_na_dohodak");
            DTsend.Columns.Add("prirez");
            DTsend.Columns.Add("porez_na_dohodak_iznos");
            DTsend.Columns.Add("prirez_iznos");
            DataRow row;

            DataTable DSkupac = classSQL.select("SELECT id_kupac FROM racuni WHERE broj_racuna='" + broj + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];

            string sql = "SELECT racun_stavke.sifra_robe,roba.naziv,racun_stavke.vpc,racun_stavke.porez_potrosnja," +
                "racun_stavke.porez_na_dohodak,racun_stavke.prirez,racun_stavke.porez_na_dohodak_iznos,racun_stavke.prirez_iznos," +
                "racun_stavke.id_skladiste,racun_stavke.mpc,racun_stavke.porez,racun_stavke.kolicina,racun_stavke.rabat," +
                "racun_stavke.povratna_naknada,roba.oduzmi,roba.mpc AS cijena FROM racun_stavke " +
                " LEFT JOIN roba ON roba.sifra=racun_stavke.sifra_robe" +
                " WHERE racun_stavke.broj_racuna='" + broj + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan;
            DataTable DTrac = classSQL.select(sql, "racun_stavke").Tables[0];

            for (int i = 0; i < DTrac.Rows.Count; i++)
            {
                sifra = DTrac.Rows[i]["sifra_robe"].ToString();
                row = DTsend.NewRow();
                //Dodano za apsolutni popust
                row["sifra_robe"] = DTrac.Rows[i]["sifra_robe"].ToString();
                row["id_skladiste"] = DTrac.Rows[i]["id_skladiste"].ToString();
                //Dodano za apsolutni popust
                row["ime"] = DTrac.Rows[i]["naziv"].ToString();
                row["porez"] = DTrac.Rows[i]["porez"].ToString();
                row["mpc"] = DTrac.Rows[i]["mpc"].ToString();
                row["kolicina"] = DTrac.Rows[i]["kolicina"].ToString();
                row["rabat"] = DTrac.Rows[i]["rabat"].ToString();
                row["cijena"] = DTrac.Rows[i]["mpc"].ToString();
                row["vpc"] = DTrac.Rows[i]["vpc"].ToString();
                row["porez_potrosnja"] = DTrac.Rows[i]["porez_potrosnja"].ToString();
                row["povratna_naknada"] = DTrac.Rows[i]["povratna_naknada"].ToString();

                row["porez_na_dohodak"] = DTrac.Rows[i]["porez_na_dohodak"].ToString();
                row["prirez"] = DTrac.Rows[i]["prirez"].ToString();
                row["porez_na_dohodak_iznos"] = DTrac.Rows[i]["porez_na_dohodak_iznos"].ToString();
                row["prirez_iznos"] = DTrac.Rows[i]["prirez_iznos"].ToString();
                DTsend.Rows.Add(row);
                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" +
                            sifra.Substring(3, sifra.Length - 3) + "'";
                        classSQL.update(sqlnext);
                    }
                }
            }

            DTsetting = classSQL.select("SELECT * FROM pos_print", "pos_print").Tables[0];

            DataTable DSblagajnik = classSQL.select("SELECT id_blagajnik FROM racuni WHERE broj_racuna='" + broj + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];
            string blagajnik = "";
            if (DSblagajnik.Rows.Count > 0)
            {
                blagajnik = classSQL.select("SELECT ime + ' ' + prezime AS name FROM zaposlenici WHERE id_zaposlenik='" + DSblagajnik.Rows[0][0].ToString() + "'", "zaposlenici").Tables[0].Rows[0]["name"].ToString();
            }

            //if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
            //{
            //    try
            //    {
            //        barcode = "000" + broj;
            //        DateTime[] datumi = new DateTime[2];
            //        datumi[0] = DateTime.Now;
            //        datumi[1] = datumi[0];
            //        PosPrint.classPosPrintMaloprodaja1.PrintReceipt(DTsend, blagajnik, broj + "/" +
            //            datumi[0].Year.ToString(), DSkupac.Rows[0][0].ToString(), barcode, broj, "G", datumi, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
            //            "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
            //            + ex.Message, "Printer") == DialogResult.Yes)
            //        {
            //            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //            rfak.dokumenat = "RAC";
            //            rfak.ImeForme = "Račun";
            //            rfak.broj_dokumenta = broj;
            //            rfak.ShowDialog();
            //        }
            //    }
            //}
            //else
            //{
            //    Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //    rfak.dokumenat = "RAC";
            //    rfak.ImeForme = "Račun";
            //    rfak.broj_dokumenta = broj;
            //    rfak.ShowDialog();
            //}

            DataTable DTRacunDatum = classSQL.select("SELECT datum_racuna FROM racuni where broj_racuna='" + broj + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "pos_print").Tables[0];
            DateTime[] datumi = new DateTime[2];
            datumi[0] = Convert.ToDateTime(DTRacunDatum.Rows[0][0].ToString());
            datumi[1] = datumi[0];

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();
            string a6 = classSQL.select_settings("SELECT a6print FROM postavke", "postavke").Tables[0].Rows[0]["a6print"].ToString();

            barcode = "000" + broj + id_ducan + id_kasa + datumi[0].Year.ToString().Remove(0, 2);

            try
            {
                PosPrint.classPosPrintMaloprodaja2.BoolPreview = false;
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(DTsend, blagajnik, broj + "/" +
                    datumi[0].Year.ToString(), DSkupac.Rows[0][0].ToString(), barcode, broj, "G", datumi, true, mali, false, true, id_ducan, id_kasa);

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(broj);
                }
                else if (a6 == "1")
                {
                    printajA6(broj);
                }
                else if (mali != "1")
                {
                    printaj(broj);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(broj);
                }
            }
        }

        private void printaj(string brRac)
        {
            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA5(string brRac)
        {
            Report.A5racun.frmA5racun rfak = new Report.A5racun.frmA5racun();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void printajA6(string brRac)
        {
            Report.A6racun.frmA6racun rfak = new Report.A6racun.frmA6racun();
            rfak.dokumenat = "RAC";
            rfak.ImeForme = "Račun";
            rfak.naplatni = id_kasa;
            rfak.poslovnica = id_ducan;
            rfak.broj_dokumenta = brRac;
            rfak.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kasa.frmIspisOdredenogRacuna odd = new Kasa.frmIspisOdredenogRacuna();
            odd.id_kasa = id_kasa;
            odd.id_ducan = id_ducan;
            odd.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Kasa.frmStornoRacuna sr = new Kasa.frmStornoRacuna();
            sr.ShowDialog();
            if (sr.brojRacuna != "-1") StornirajRacun(sr.brojRacuna);
        }

        private void btnStornoZadnjegR_Click(object sender, EventArgs e)
        {
            //DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS integer)) FROM racuni", "racuni").Tables[0];
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];

            //DataTable DTstornoprovjera = classSQL.select("SELECT * FROM racuni where broj_racuna = '" + DSbr.Rows[0]["broj_racuna"].ToString() + "' AND storno = 'NE'", "storno provjera").Tables[0];

            if (DSbr.Rows.Count != 0 && DSbr.Rows[0][0].ToString() != "")
            {
                StornirajRacun(DSbr.Rows[0][0].ToString());
            }
            else
            {
                MessageBox.Show("U bazi ne postoji niti jedan račun!");
            }
        }

        #region storniranje računa

        private void StornirajRacun(string racun)
        {
            DataTable DSracun = classSQL.select("SELECT storno,id_kupac FROM racuni where broj_racuna='" + racun + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];
            sifraPartnera = DSracun.Rows[0]["id_kupac"].ToString();
            if (DSracun.Rows[0]["storno"].ToString() == "DA")
            {
                MessageBox.Show("Račun je već storniran!");
            }
            else
            {
                StornirajRacunHelper(racun);
                MessageBox.Show("Izvršeno.");
            }
        }

        private void StornirajRacunHelper(string stariRacun)
        {
            classSQL.transaction("BEGIN;");
            //ubaci u racuni i vrati IznosGotovina, IznosKartica, ukupno i novi racun
            string[] iznosi = InsertRacun(stariRacun, id_kasa, id_ducan, sifraPartnera);

            string IznosGotovina = iznosi[0].ToString();
            string IznosKartica = iznosi[1].ToString();
            string IznosVirman = iznosi[2].ToString();
            string uk = iznosi[3].ToString();
            string noviRacun = iznosi[4].ToString();

            DataTable DSracunStavke = classSQL.select("SELECT * FROM racun_stavke where broj_racuna='" + stariRacun + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racun_stavke").Tables[0];

            srediStavkeRobuSkladiste(ref DSracunStavke, stariRacun, noviRacun);

            bool paragonProvjera = false;
            foreach (DataRow r in DSracunStavke.Rows)
            {
                if (r["sifra_robe"].ToString()[0] != '!') { paragonProvjera = true; break; }
            }
            if (paragonProvjera)
            {
                Util.AktivnostZaposlenika.SpremiAktivnost(new DataGridView(), null, "Maloprodaja", stariRacun, true);
            }

            srediPopuste(stariRacun, noviRacun, uk);
            classSQL.transaction("COMMIT;");

            if (!Class.Dokumenti.isKasica && Class.Postavke.automatski_zapisnik)
            {
                string oldNapomena = "Kreiranje zapisnika zbog promjene cijena na maloprodajnom računu broj " + stariRacun + "/" + Util.Korisno.nazivPoslovnica + "/" + Util.Korisno.nazivNaplatnogUredaja;
                string newNapomena = "Kreiranje zapisnika zbog promjene cijena na maloprodajnom računu broj " + noviRacun + "/" + Util.Korisno.nazivPoslovnica + "/" + Util.Korisno.nazivNaplatnogUredaja;

                Class.ZapisnikPromjeneCijene _zapisnik = new Class.ZapisnikPromjeneCijene(oldNapomena, newNapomena);
                if (_zapisnik.kreiraniZapisnik)
                {
                    MessageBox.Show("Kreiranje je zapisnik o promjeni cijene zbog storniranog računa.");
                }
                _zapisnik = null;
            }

            printaj(DSracunStavke, IznosGotovina, IznosKartica, IznosVirman, noviRacun, iznosi[5], iznosi[6]);
        }

        /// <summary>
        /// Ubacuje novi račun i stavlja storno=DA, apdejta stari račun storno=DA;
        /// vraća IznosGotovina, IznosKartica, ukupno, noviRacun
        /// </summary>
        /// <param name="stariRacun"></param>
        /// <param name="kasa"></param>
        /// <param name="ducan"></param>
        /// <param name="partner"></param>
        /// <returns></returns>
        private string[] InsertRacun(string stariRacun, string kasa, string ducan, string partner)
        {
            DataTable DTracun = classSQL.select("SELECT * FROM racuni where broj_racuna='" + stariRacun + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];
            string iznosGotovina = (Convert.ToDouble(DTracun.Rows[0]["ukupno_gotovina"].ToString()) * -1).ToString();
            string iznosKartica = (Convert.ToDouble(DTracun.Rows[0]["ukupno_kartice"].ToString()) * -1).ToString();
            string iznosBon = (Convert.ToDouble(DTracun.Rows[0]["ukupno_bon"].ToString()) * -1).ToString();
            double vir;
            //ovo je zato jer prije nije bilo virmana u bazi, tj. bil je empty string
            try
            {
                vir = Convert.ToDouble(DTracun.Rows[0]["ukupno_virman"].ToString());
            }
            catch
            {
                vir = 0;
            }
            string iznosVirman = (vir * -1).ToString();
            string ukupno = (Convert.ToDouble(DTracun.Rows[0]["ukupno"].ToString()) * -1).ToString();
            string dobivenoGotovina = (Convert.ToDouble(DTracun.Rows[0]["dobiveno_gotovina"].ToString()) * -1).ToString();
            string ukupno_ostalo = (Convert.ToDouble(DTracun.Rows[0]["ukupno_ostalo"].ToString()) * -1).ToString();
            string ukupno_rabat = (Convert.ToDouble(DTracun.Rows[0]["ukupno_rabat"].ToString()) * -1).ToString();
            string ukupno_porez = (Convert.ToDouble(DTracun.Rows[0]["ukupno_porez"].ToString()) * -1).ToString();
            string ukupno_mpc = (Convert.ToDouble(DTracun.Rows[0]["ukupno_mpc"].ToString()) * -1).ToString();
            string ukupno_vpc = (Convert.ToDouble(DTracun.Rows[0]["ukupno_vpc"].ToString()) * -1).ToString();
            string ukupno_mpc_rabat = (Convert.ToDouble(DTracun.Rows[0]["ukupno_mpc_rabat"].ToString()) * -1).ToString();
            string ukupno_povratna_naknada = (Convert.ToDouble(DTracun.Rows[0]["ukupno_povratna_naknada"].ToString()) * -1).ToString();
            string ukupno_osnovica = (Convert.ToDouble(DTracun.Rows[0]["ukupno_osnovica"].ToString()) * -1).ToString();

            DateTime dRac = Convert.ToDateTime(DateTime.Now);
            string dt = dRac.ToString("yyyy-MM-dd H:mm:ss");

            string noviRacun = brojRacuna();
            string sql = "INSERT INTO racuni (broj_racuna,id_kupac,datum_racuna,id_ducan,id_kasa,id_blagajnik," +
                "gotovina,kartice,bon,ukupno_gotovina,ukupno_kartice,ukupno_bon,ukupno_virman,broj_kartice_cashback,broj_kartice_bodovi," +
                "br_sa_prethodnog_racuna,dobiveno_gotovina,storno,ukupno,napomena,nacin_placanja,ukupno_ostalo," +
                "ukupno_povratna_naknada,ukupno_mpc,ukupno_vpc,ukupno_mpc_rabat,ukupno_rabat,ukupno_osnovica,ukupno_porez, avio_registracija, avio_tip_zrakoplova, avio_maks_tezina_polijetanja) " +
                "VALUES (" +
                "'" + noviRacun + "'," +
                "'" + partner + "'," +
                "'" + dt + "'," +
                "'" + ducan + "'," +
                "'" + kasa + "'," +
                "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                "'" + DTracun.Rows[0]["gotovina"].ToString() + "'," +
                "'" + DTracun.Rows[0]["kartice"].ToString() + "'," +
                "'" + DTracun.Rows[0]["bon"].ToString() + "'," +
                "'" + iznosGotovina + "'," +
                "'" + iznosKartica + "'," +
                "'" + iznosBon + "'," +
                "'" + iznosVirman + "'," +
                "'" + DTracun.Rows[0]["broj_kartice_cashback"].ToString() + "'," +
                "'" + DTracun.Rows[0]["broj_kartice_bodovi"].ToString() + "'," +
                "'" + DTracun.Rows[0]["br_sa_prethodnog_racuna"].ToString() + "'," +
                "'" + dobivenoGotovina + "'," +
                "'NE'," +
                "'" + ukupno + "'," +
                "'" + DTracun.Rows[0]["napomena"].ToString() + "'," +
                "'" + DTracun.Rows[0]["nacin_placanja"].ToString() + "'," +
                "'" + ukupno_ostalo.Replace(",", ".") + "'," +
                "'" + ukupno_povratna_naknada.Replace(",", ".") + "'," +
                "'" + ukupno_mpc.Replace(",", ".") + "'," +
                "'" + ukupno_vpc.Replace(",", ".") + "'," +
                "'" + ukupno_mpc_rabat.Replace(",", ".") + "'," +
                "'" + ukupno_rabat.Replace(",", ".") + "'," +
                "'" + ukupno_osnovica.Replace(",", ".") + "'," +
                "'" + ukupno_porez.Replace(",", ".") + "'," +
                 "'" + DTracun.Rows[0]["avio_registracija"].ToString() + "'," +
                "'" + DTracun.Rows[0]["avio_tip_zrakoplova"].ToString() + "'," +
                "'" + DTracun.Rows[0]["avio_maks_tezina_polijetanja"].ToString().Replace(",", ".") + "'" +
                ")";

            provjera_sql(classSQL.transaction(sql));

            string sqlStorno = "UPDATE racuni SET storno='DA' WHERE broj_racuna='" + stariRacun + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan;
            classSQL.transaction(sqlStorno);

            string[] a = new string[7];
            a[0] = iznosGotovina;
            a[1] = iznosKartica;
            a[2] = iznosVirman;
            a[3] = ukupno;
            a[4] = noviRacun;
            a[5] = iznosBon;
            a[6] = DTracun.Rows[0]["nacin_placanja"].ToString();

            return a;
        }

        /// <summary>
        /// Oduzima robu sa skladišta za svaku stavku
        /// </summary>
        /// <param name="DSracunStavke">Reference DSracunStavke</param>
        /// <param name="stari_racun"></param>
        /// <param name="novi_racun"></param>
        private void srediStavkeRobuSkladiste(ref DataTable DSracunStavke, string stari_racun, string novi_racun)
        {
            string kol;
            string povrNaknad;

            //za svaku stavku vrati robu na skladište
            for (int i = 0; i < DSracunStavke.Rows.Count; i++)
            {
                sifra = DSracunStavke.Rows[i]["sifra_robe"].ToString();

                //povrNaknad = DSracunStavke.Rows[i]["povratna_naknada"].ToString();
                //povrNaknad = (Convert.ToDouble(povrNaknad) * -1).ToString();
                //DSracunStavke.Rows[i].SetField("povratna_naknada", povrNaknad);

                try
                {
                    povrNaknad = DSracunStavke.Rows[i]["povratna_naknada"].ToString();
                    povrNaknad = (Convert.ToDouble(povrNaknad)).ToString();
                }
                catch
                {
                    povrNaknad = "0";
                }

                DSracunStavke.Rows[i].SetField("povratna_naknada", povrNaknad);

                kol = DSracunStavke.Rows[i]["kolicina"].ToString();
                kol = (Convert.ToDouble(kol) * -1).ToString();
                DSracunStavke.Rows[i].SetField("kolicina", kol);

                sifra_skladiste = DSracunStavke.Rows[i]["id_skladiste"].ToString();

                //postavi broj_racuna na novi račun (kad ne bi stavili, u bazu bi sve stavke išle na stari račun)
                DSracunStavke.Rows[i].SetField("broj_racuna", novi_racun);
                //

                DataTable robaOduzmi = classSQL.select(
                    "SELECT oduzmi FROM roba where sifra='" +
                    sifra + "'", "roba")
                    .Tables[0];

                if (robaOduzmi.Rows[0]["oduzmi"].ToString() == "DA")
                {
                    kol = SQL.ClassSkladiste.GetAmount(
                        sifra,
                        sifra_skladiste,
                        kol,
                        "1",
                        "-");
                    /*SQL.SQLroba_prodaja.UpdateRows(
						sifra_skladiste,
						kol,
						sifra);*/
                    string update = "UPDATE roba_prodaja SET kolicina='" + kol.ToString() +
                        "' WHERE id_skladiste='" + sifra_skladiste +
                        "' AND sifra='" + sifra + "';";
                    classSQL.update(update);
                }

                //???
                if (sifra.Length > 4)
                {
                    if (sifra.Substring(0, 3) == "000")
                    {
                        string sqlnext = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + sifra.Substring(3, sifra.Length - 3) + "' AND dokumenat='RN'";
                        classSQL.transaction(sqlnext);
                    }
                }
            }

            provjera_sql(SQL.SQLracun.InsertStavke(DSracunStavke));
        }

        /// <summary>
        /// Apdejta racun_popust_kod_sljedece_kupnje atribut koristeno=DA kod starog računa,
        /// te ubacuje u istu tablicu novi zapis za novi račun koristeno=DA
        /// </summary>
        /// <param name="stariRacun"></param>
        /// <param name="noviRacun"></param>
        /// <param name="ukupno"></param>
        private void srediPopuste(string stariRacun, string noviRacun, string ukupno)
        {
            barcode = "000" + noviRacun + id_ducan + id_kasa + DateTime.Now.Year.ToString().Remove(0, 2);

            if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA")
            {
                if (classSQL.remoteConnectionString == "")
                {
                    ukupno = ukupno.Replace(",", ".");
                }
                else
                {
                    ukupno = ukupno.Replace(".", ",");
                }

                DateTime date = Convert.ToDateTime(classSQL.select("SELECT datum_racuna FROM racuni where broj_racuna='" + stariRacun + "' AND id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "pos_print").Tables[0].Rows[0][0].ToString());

                string sqlStorno = "UPDATE racun_popust_kod_sljedece_kupnje SET koristeno='DA' WHERE broj_racuna='" + stariRacun + id_ducan + id_kasa + date.Year.ToString().Remove(0, 2) + "'";
                classSQL.transaction(sqlStorno);

                provjera_sql(classSQL.transaction("INSERT INTO racun_popust_kod_sljedece_kupnje (broj_racuna,datum,ukupno,popust,koristeno,dokumenat) VALUES (" +
                     "'" + noviRacun + id_ducan + id_kasa + DateTime.Now.Year.ToString().Remove(0, 2) + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                     "'" + ukupno + "'," +
                     "'" + DTpromocije1.Rows[0]["popust"].ToString() + "'," +
                     "'DA'," +
                     "'RN'" +
                     ")"));
            }
        }

        /// <summary>
        /// Printa račun
        /// </summary>
        /// <param name="Stavke"></param>
        /// <param name="IznosGotovina"></param>
        /// <param name="IznosKartica"></param>
        /// <param name="noviRacun"></param>
        private void printaj(DataTable Stavke, string IznosGotovina, string IznosKartica, string IznosVirman, string noviRacun, string IznosBon, string nacinplacanj = null)
        {
            //dodaj kolonu jer datatable za printanje mora imati kolonu "ime"
            Stavke.Columns.Add("ime");
            for (int i = 0; i < Stavke.Rows.Count; i++)
            {
                string sifra = Stavke.Rows[i]["sifra_robe"].ToString();
                DataTable imeRobe = classSQL.select(
                    "SELECT naziv FROM roba where sifra='" +
                    sifra + "'", "roba")
                    .Tables[0];

                Stavke.Rows[i].SetField("ime", imeRobe.Rows[0]["naziv"].ToString());
            }

            string placanje = "O";

            //if (DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString() == "1")
            //{
            //    try
            //    {
            //        if (Convert.ToDecimal(IznosGotovina) == 0 && Convert.ToDecimal(IznosKartica) > 0)
            //        {
            //            placanje = "K";
            //        }
            //        else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) == 0)
            //        {
            //            placanje = "G";
            //        }
            //        else if (Convert.ToDecimal(IznosGotovina) > 0 && Convert.ToDecimal(IznosKartica) > 0)
            //        {
            //            placanje = "O";
            //        }

            //        DateTime[] datumi = new DateTime[2];
            //        datumi[0] = DateTime.Now;
            //        datumi[1] = datumi[0];
            //        PosPrint.classPosPrintMaloprodaja1.PrintReceipt(Stavke, blagajnik_ime,
            //            noviRacun + "/" + datumi[0].Year.ToString(), sifraPartnera, barcode, noviRacun, placanje, datumi, false);
            //    }
            //    catch (Exception ex)
            //    {
            //        if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\nŽelite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
            //            + ex.Message, "Printer") == DialogResult.Yes)
            //        {
            //            Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //            rfak.dokumenat = "RAC";
            //            rfak.ImeForme = "Račun";
            //            rfak.broj_dokumenta = noviRacun;
            //            rfak.ShowDialog();
            //        }
            //    }
            //}
            //else
            //{
            //    Report.Faktura.repFaktura rfak = new Report.Faktura.repFaktura();
            //    rfak.dokumenat = "RAC";
            //    rfak.ImeForme = "Račun";
            //    rfak.broj_dokumenta = noviRacun;
            //    rfak.ShowDialog();
            //}

            if (Convert.ToDecimal(IznosGotovina) == 0 && Convert.ToDecimal(IznosBon) == 0 && Convert.ToDecimal(IznosKartica) != 0)
            {
                placanje = "K";
            }
            else if (Convert.ToDecimal(IznosGotovina) != 0 && Convert.ToDecimal(IznosBon) == 0 && Convert.ToDecimal(IznosKartica) == 0)
            {
                placanje = "G";
            }
            else if (Convert.ToDecimal(IznosGotovina) != 0 && Convert.ToDecimal(IznosKartica) != 0)
            {
                placanje = "O";
            }

            if (nacinplacanj != null)
                placanje = nacinplacanj;

            string mali = DTpostavkePrinter.Rows[0]["posPrinterBool"].ToString();
            string a5 = classSQL.select_settings("SELECT a5print FROM postavke", "postavke").Tables[0].Rows[0]["a5print"].ToString();

            try
            {
                DateTime[] datumi = new DateTime[2];
                datumi[0] = DateTime.Now;
                datumi[1] = datumi[0];
                PosPrint.classPosPrintMaloprodaja2.PrintReceipt(Stavke, blagajnik_ime,
                    noviRacun + "/" + datumi[0].Year.ToString(), sifraPartnera, barcode, noviRacun,
                    placanje, datumi, false, mali, false, true, id_ducan, id_kasa);

                if (mali == "1")
                {
                    //već je isprintan u gornjoj metodi
                }
                else if (a5 == "1")
                {
                    printajA5(noviRacun);
                }
                else if (mali != "1")
                {
                    printaj(noviRacun);
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show("Desila se pogreška kod ispisa na 'mali' pos printer.\r\n" +
                    "Želite li ispisati ovaj dokument na A4 format?" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "Printer") == DialogResult.Yes)
                {
                    printaj(noviRacun);
                }
            }
        }

        private string ReturnSifra(string sifra)
        {
            try
            {
                if (sifra.Length > 3)
                {
                    if (DTpromocije1.Rows[0]["aktivnost"].ToString() == "DA" && sifra.Substring(0, 3) == "000")
                    {
                        return "00000";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return sifra;
        }

        private string brojRacuna()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(CAST(broj_racuna AS bigint)) FROM racuni WHERE id_kasa=" + id_kasa + " AND id_ducan=" + id_ducan, "racuni").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        #endregion storniranje računa

        private void button4_Click(object sender, EventArgs e)
        {
            Kasa.frmDodajPartnera dp = new Kasa.frmDodajPartnera();
            dp.ShowDialog();
        }

        private void btnPromjenaPlacanja_Click(object sender, EventArgs e)
        {
            Kasa.frmPromjenaNacinaPlacanja pnp = new Kasa.frmPromjenaNacinaPlacanja();
            pnp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable DTpostavkePrinter = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];

            try
            {
                if (DTpostavkePrinter.Rows[0]["drawer_bool"].ToString() == "1")
                {
                    //PosPrint.classCashDrawer.OpenCashDrawer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška:\r\nDesila se pogreška kod otvaranje ladice.\r\nOvo je originalna pogreška:\r\n" + Environment.NewLine + Environment.NewLine
                        + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private string Sukupno = "";
        private string datumOD = "";

        private void button4_Click_1(object sender, EventArgs e)
        {
            string ZBS = ZadnjiBrojSmjene();

            if (ZBS != "null")
            {
                string sql = "SELECT * FROM smjene WHERE id='" + ZBS + "'";
                DataTable DT_smjena = classSQL.select(sql, "smjene").Tables[0];

                if (DT_smjena.Rows.Count > 0)
                {
                    if (DT_smjena.Rows[0]["zavrsetak"].ToString() == "")
                    {
                        Kasa.frmZavrsiSmjenu ps = new Kasa.frmZavrsiSmjenu();
                        ps.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Smjena nije započeta.", "Greška");
                    }
                }
                else
                {
                    MessageBox.Show("Smjena nije započeta.", "Greška");
                }
            }
        }

        private string ZadnjiBrojSmjene()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(id) FROM smjene", "smjene").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString())).ToString();
            }
            else
            {
                return "null";
            }
        }

        private void btnOtvoriLadicu_Click(object sender, EventArgs e)
        {
            DTsetting = classSQL.select_settings("SELECT * FROM pos_print", "pos_print").Tables[0];
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();
            PrintDocument printDoc = new PrintDocument();

            printDoc.PrinterSettings.PrinterName = printerName;

            /*string GS = Convert.ToString((char)29);
            string ESC = Convert.ToString((char)27);

            string COMMAND = "";
            COMMAND = ESC + "@";
            COMMAND += GS + "V" + (char)1;*/

            if (DSpostavke.Tables[0].Rows[0]["direct_print"].ToString() == "1")
            {
                if (DSpostavke.Tables[0].Rows[0]["ladicaOn"].ToString() == "1")
                {
                    openCashDrawer1();
                }

                string GS = Convert.ToString((char)29);
                string ESC = Convert.ToString((char)27);

                string COMMAND = "";
                COMMAND = ESC + "@";
                COMMAND += GS + "V" + (char)1;

                PCPOS.PosPrint.RawPrinterHelper.SendStringToPrinter(printDoc.PrinterSettings.PrinterName, "" + COMMAND);
            }
            else
            {
                if (!printDoc.PrinterSettings.IsValid)
                {
                    string msg = string.Format(
                       "Can't find printer \"{0}\".", printerName);
                    MessageBox.Show(msg, "Print Error");
                    return;
                }
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                printDoc.Print();
            }
        }

        private void openCashDrawer1()
        {
            string printerName = DTsetting.Rows[0]["windows_printer_name"].ToString();

            byte[] codeOpenCashDrawer = new byte[] { 27, 112, 48, 55, 121 };
            IntPtr pUnmanagedBytes = new IntPtr(0);
            pUnmanagedBytes = Marshal.AllocCoTaskMem(5);
            Marshal.Copy(codeOpenCashDrawer, 0, pUnmanagedBytes, 5);
            PCPOS.PosPrint.RawPrinterHelper.SendBytesToPrinter(printerName, pUnmanagedBytes, 5);
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
        }

        private void PrintPage(object o, PrintPageEventArgs e)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat = new StringFormat();
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            System.Drawing.Text.PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile("Slike/msgothic.ttc");
            System.Drawing.Font font = new Font(privateFonts.Families[0], 10);
            string drawString = "";
            Font drawFont = font;
            e.Graphics.DrawString(drawString, drawFont, drawBrush, 0, 0, drawFormat);
        }

        private void btnDodajStavkuUMinusIzRacuna_Click(object sender, EventArgs e)
        {
            try
            {
                Kasa.frmDodajStavkuUMinus f = new Kasa.frmDodajStavkuUMinus();
                f.ShowDialog();
                if (f.DialogResult == DialogResult.Yes)
                {
                    DataGridViewRow dRow = f.dgvRez.Rows[f.dgvRez.CurrentCell.RowIndex];
                    decimal kol = 0, mpc = 0, rabat = 0, ukupno = 0;
                    decimal.TryParse(dRow.Cells["Količina"].Value.ToString(), out kol);
                    decimal.TryParse(dRow.Cells["MPC"].Value.ToString(), out mpc);
                    decimal.TryParse(dRow.Cells["Rabat"].Value.ToString(), out rabat);

                    mpc = mpc * kol;
                    rabat = mpc * rabat / 100;

                    ((frmKasa)this.Owner).dgv.Rows.Add(
                    dRow.Cells["Šifra"].Value.ToString(),
                    dRow.Cells["Naziv"].Value.ToString(),
                    dRow.Cells["JM"].Value.ToString(),
                    dRow.Cells["Skladište"].Value.ToString(),
                    dRow.Cells["MPC"].Value.ToString(),
                    dRow.Cells["Količina"].Value.ToString(),
                    dRow.Cells["Rabat"].Value.ToString(),
                    (mpc - rabat).ToString("#0.00"),
                    dRow.Cells["Oduzmi"].ToString(),
                    dRow.Cells["PDV"].Value.ToString(),
                    dRow.Cells["VPC"].Value.ToString(),
                    "",
                    dRow.Cells["NBC"].Value.ToString(),
                    dRow.Cells["PND"].Value.ToString(),
                    dRow.Cells["Prirez"].Value.ToString(),
                    dRow.Cells["PND iznos"].Value.ToString(),
                    dRow.Cells["Prirez iznos"].Value.ToString());

                    ((frmKasa)this.Owner).IzracunUkupno();
                    ((frmKasa)this.Owner).PaintRows(((frmKasa)this.Owner).dgv);
                    ((frmKasa)this.Owner).dgv.Rows[rowCount].ReadOnly = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}