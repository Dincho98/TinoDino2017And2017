using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PCPOS.Robno
{
    public partial class frmUlazneFakture : Form
    {
        public frmUlazneFakture()
        {
            InitializeComponent();
        }

        public string broj_fakture_edit { get; set; }
        public int godina_edit { get; set; }
        private bool edit = false;
        public frmMenu MainForm { get; set; }
        private DataTable DTpostavke = classSQL.select_settings("SELECT * FROM postavke", "postavke").Tables[0];

        private void frmUlazneFakture_Load(object sender, EventArgs e)
        {
            //this.Paint += new PaintEventHandler(Form1_Paint);

            EnableDisable(false, true);
            ControlDisableEnable(1, 0, 0, 1, 0);

            fillComboBox();

            if (godina_edit != 0)
                nmGodinaFakture.Value = godina_edit;

            ttxBrojFakture.Text = brojUFA();

            if (broj_fakture_edit != null) { Fill(); }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 1000);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void provjera_sql(string str)
        {
            if (str != "")
            {
                MessageBox.Show(str);
            }
        }

        private void fillComboBox()
        {
            nmGodinaFakture.Value = DateTime.Now.Year;

            //DS Valuta
            DataSet DSValuta = classSQL.select("SELECT * FROM valute", "valute");
            foreach (DataRow r in DSValuta.Tables[0].Rows)
            {
                if (r["naziv"].ToString() == "")
                {
                    r["naziv"] = "HRK";
                }
            }

            cbValuta.DataSource = DSValuta.Tables[0];
            cbValuta.DisplayMember = "ime_valute";
            cbValuta.ValueMember = "naziv";
            cbValuta.SelectedValue = "HRK";

            //fill ziro_racun
            //DataSet DSzr1 = classSQL.select("SELECT * FROM ziro_racun", "ziro_racun");
            //cbSziroRacuna.DataSource = DSzr1.Tables[0];
            //cbSziroRacuna.DisplayMember = "ziroracun";
            //cbSziroRacuna.ValueMember = "id_ziroracun";

            //fill tko je prijavljen
            txtIzradio.Text = classSQL.select("SELECT ime+' '+prezime as Ime  FROM zaposlenici WHERE id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'", "zaposlenici").Tables[0].Rows[0][0].ToString();

            DataTable DTizvor_dokumenta = new DataTable();
            DTizvor_dokumenta.Columns.Add("id");
            DTizvor_dokumenta.Columns.Add("naziv");
            DTizvor_dokumenta.Rows.Add("300", "Nalozi banaka za svoja plaćanja i za plaćanja na teret računa svojih klijenata");
            DTizvor_dokumenta.Rows.Add("701", "Nalozi klijenata inicirani putem servisa e-plaćanja Fine i na šalteru jedinica Fine");
            DTizvor_dokumenta.Rows.Add("803", "Elektronski nalozi koje inicira banka za plaćanja na teret transakcijskih računa svojih klijenta radi izvršenja osnova za plaćanje");
            DTizvor_dokumenta.Rows.Add("652", "Elektronski medij podnosi HNB i Ministarstvo financija za plaćanja putem HSVP");
            DTizvor_dokumenta.Rows.Add("530", "Nalozi vezani za kolekciju države");
            DTizvor_dokumenta.Rows.Add("502", "Nalozi javnih prihoda s posebnim kontrolama");
            DTizvor_dokumenta.Rows.Add("520", "Posebna obrada naloga za plačanje");
            cbIzvorDokumenata.DataSource = DTizvor_dokumenta;
            cbIzvorDokumenata.DisplayMember = "naziv";
            cbIzvorDokumenata.ValueMember = "id";

            DataTable DTvrstaNaloga = new DataTable();
            DTvrstaNaloga.Columns.Add("id");
            DTvrstaNaloga.Columns.Add("naziv");
            DTvrstaNaloga.Rows.Add("1", "Nacionalna plaćanja HRK");
            DTvrstaNaloga.Rows.Add("2", "Međunarodna plaćanja u HRK i stranoj valuti");
            DTvrstaNaloga.Rows.Add("3", "Nacionalna plaćanja u stranoj valuti");
            cbVrstaNaloga.DataSource = DTvrstaNaloga;
            cbVrstaNaloga.DisplayMember = "naziv";
            cbVrstaNaloga.ValueMember = "id";

            DataTable DTvrsta_strane_osobe = new DataTable();
            DTvrsta_strane_osobe.Columns.Add("id");
            DTvrsta_strane_osobe.Columns.Add("naziv");
            DTvrsta_strane_osobe.Rows.Add("1", "Pravna osoba");
            DTvrsta_strane_osobe.Rows.Add("2", "Fizička osoba");
            cbVrstaStraneOsobe.DataSource = DTvrsta_strane_osobe;
            cbVrstaStraneOsobe.DisplayMember = "naziv";
            cbVrstaStraneOsobe.ValueMember = "id";

            DataTable DTtroskovna_opcija = new DataTable();
            DTtroskovna_opcija.Columns.Add("id");
            DTtroskovna_opcija.Columns.Add("naziv");
            DTtroskovna_opcija.Rows.Add("1", "Na teret nalogodavatelja (OUR)");
            DTtroskovna_opcija.Rows.Add("2", "Na teret korisnika (BEN)");
            DTtroskovna_opcija.Rows.Add("3", "Podijeljeni troškovi SHA");
            cbTroskovnaOpcija.DataSource = DTvrsta_strane_osobe;
            cbTroskovnaOpcija.DisplayMember = "naziv";
            cbTroskovnaOpcija.ValueMember = "id";

            DataTable DTsifraNamjene = new DataTable();
            DTsifraNamjene.Columns.Add("id");
            DTsifraNamjene.Columns.Add("naziv");
            DTsifraNamjene.Rows.Add("CDCD ", "Gotovinska isplata Isplata gotovine na bankomatu ili na šalteru banke ");
            DTsifraNamjene.Rows.Add("CDCB ", "Kartično plaćanje uz gotovinski povrat (Cashback) Kupovina roba i usluga uz dodatnu isplatu gotovine na prodajnom mjestu ");
            DTsifraNamjene.Rows.Add("CDQC ", "Zamjenska gotovina Kupovina roba koje su jednake gotovini poput kupona u kockarnicama.");
            DTsifraNamjene.Rows.Add("CDOC ", "Originalno odobrenje Transakcija koja omogućuje primatelju kartice izvršenje odobrenja u korist računa vlasnika kartice. Za razliku od trgovačkog povrata, originalnom odobrenju nije prethodilo 0plaćanje karticom. Koristi se kod odobrenja temeljem dobitaka u igrama na sreću.");
            DTsifraNamjene.Rows.Add(" ACCT ", "Upravljanje sredstvima - unutar banke Transakcija prenošenja sredstava između dva računa istog vlasnika računa kod iste banke.");
            DTsifraNamjene.Rows.Add("CASH ", "Upravljanje sredstvima – izvan banke Transakcija predstavlja opću instrukciju za upravljanje sredstvima.");
            DTsifraNamjene.Rows.Add("COLL ", "Naplata Transakcija predstavlja prikupljanje sredstava temeljem priljeva na račun ili izravnog terećenja ");
            DTsifraNamjene.Rows.Add("CSDB ", "Gotovinska isplata Transakcija se odnosi na gotovinsku isplatu.");
            DTsifraNamjene.Rows.Add("DEPT ", "Depozit Transakcija se odnosi na uplatu depozita.");
            DTsifraNamjene.Rows.Add("INTC ", "Plaćanje unutar Grupe Transakcija se odnosi na plaćanje unutar Grupe Odnosno na plaćanje između dva društava koja pripadaju istoj Grupi.");
            DTsifraNamjene.Rows.Add("LIMA ", "Upravljanje likvidnošću Prijenos s računa iniciran radi pražnjenja računa ili svođenja stanja na nulu, cashpoolinga ili sweepinga ");
            DTsifraNamjene.Rows.Add("NETT ", "Saldiranje (netiranje) Transakcija se odnosi na izvršavanje saldiranja (netiranja).");
            DTsifraNamjene.Rows.Add("AGRT ", "Poljoprivredni transfer Transakcija se odnosi na plaćanje u poljoprivredi.");
            DTsifraNamjene.Rows.Add("AREN ", "Knjiženje potraživanja Transakcija se odnosi na knjiženje potraživanja");
            DTsifraNamjene.Rows.Add("BEXP ", "Poslovni troškovi Transakcija se odnosi na plaćanje poslovnih troškova.");
            DTsifraNamjene.Rows.Add("BOCE ", "Knjiženje konverzije u Back Office-u Transakcija se odnosi na plaćanje koje je povezano sa knjiženjem konverzije u Back Office-u");
            DTsifraNamjene.Rows.Add("COMC ", "Komercijalno plaćanje Transakcija se odnosi na plaćanje komercijalnog kredita ili duga po kreditu");
            DTsifraNamjene.Rows.Add("CPYR ", "Autorsko pravo Transakcija se odnosi na plaćanje autorskog prava.");
            DTsifraNamjene.Rows.Add("GDDS ", "Kupoprodaja roba Transakcija se odnosi na kupovinu ili prodaju roba.");
            DTsifraNamjene.Rows.Add("GDSV ", "Kupoprodaja roba i usluga Transakcija se odnosi na kupovinu i prodaju roba i usluga.");
            DTsifraNamjene.Rows.Add("GSCB ", "Kupoprodaja roba i usluga uz gotovinski povrat Transakcija se odnosi na kupovinu i prodaju roba i usluga uz gotovinski povrat.");
            DTsifraNamjene.Rows.Add("LICF ", "Naknada za licencu Transakcija predstavlja plaćanje naknade za licencu.");
            DTsifraNamjene.Rows.Add("POPE", "Knjiženje prodajnog mjesta Transakcija se odnosi na plaćanje vezano prodajno mjesto24 Komercijalna plaćanja ROYA Tantijemi / Prihodi od autorskog prava Transakcija predstavlja plaćanje tantijema /prihoda s osnova autorskog prava.");
            DTsifraNamjene.Rows.Add("ROYA ", "Tantijemi / Prihodi od autorskog prava. Transakcija predstavlja plaćanje tantijema /prihoda s osnova autorskog prava.");
            DTsifraNamjene.Rows.Add("SCVE ", "Kupoprodaja usluga Transakcija se odnosi na kupovinu i prodaju usluga.");
            DTsifraNamjene.Rows.Add("SUBS ", "Pretplata Transakcija se odnosi na plaćanje informacije ili pretplate ");
            DTsifraNamjene.Rows.Add("SUPP ", "Plaćanje dobavljaču Transakcija se odnosi na plaćanje dobavljaču.");
            DTsifraNamjene.Rows.Add("TRAD ", "Trgovačke usluge Transakcija se odnosi na plaćanje trgovačkih usluga.");
            DTsifraNamjene.Rows.Add("CHAR ", "Plaćanje u dobrotvorne svrhe Transakcija predstavlja plaćanje u dobrotvorne svrhe. ");
            DTsifraNamjene.Rows.Add("COMT ", "Konsolidirano plaćanje treće strane za račun potrošača Transakcija predstavlja plaćanje koje obavlja treća strana ovlaštena za prikupljanje sredstava radi plaćanja u ime i za račun potrošača. ");
            DTsifraNamjene.Rows.Add("CLPR ", "Otplata glavnice kredita za automobil Transakcija predstavlja plaćanje otplate glavnice kredita za automobil. ");
            DTsifraNamjene.Rows.Add("DBTC ", "Plaćanje putem terećenja Plaćanje temeljem naloga za terećenje");
            DTsifraNamjene.Rows.Add("GOVI ", "Državno osiguranje Transakcija se odnosi na plaćanje državnog osiguranja.");
            DTsifraNamjene.Rows.Add("HLRP ", "Otplata stambenog kredita Transakcija se odnosi na otplatu stambenog kredita.");
            DTsifraNamjene.Rows.Add("INPC ", "Premija osiguranja za vozilo Transakcija predstavlja plaćanje premije osiguranja za vozilo. ");
            DTsifraNamjene.Rows.Add("INSU ", "Premija osiguranja Transakcija predstavlja plaćanje premije osiguranja.");
            DTsifraNamjene.Rows.Add("INTE ", "Kamata Transakcija predstavlja plaćanje kamate.");
            DTsifraNamjene.Rows.Add("LBRI ", "Osiguranje od ozljede na radu Transakcija predstavlja plaćanje osiguranja od ozljede na radu. ");
            DTsifraNamjene.Rows.Add("LIFI ", "Životno osiguranje Transakcija predstavlja plaćanje životnog osiguranja.");
            DTsifraNamjene.Rows.Add("LOAN ", "Zajam Transakcija se odnosi na odobrenje zajma zajmoprimcu.");
            DTsifraNamjene.Rows.Add("LOAR ", "Otplata zajma Transakcija se odnosi na otplatu zajma zajmodavcu.");
            DTsifraNamjene.Rows.Add("PPTI ", "Osiguranje imovine Transakcija predstavlja plaćanje osiguranja imovine.");
            DTsifraNamjene.Rows.Add("RINP ", "Obročno plaćanje Transakcija se odnosi na plaćanje ponavljajućih rata u redovitim intervalima.");
            DTsifraNamjene.Rows.Add("TRFD ", "Zaklada Transakcija koja se odnosi na plaćanje zaklade.");
            DTsifraNamjene.Rows.Add("ADVA ", "Predujam Transakcija predstavlja plaćanje predujma/ avansa.");
            DTsifraNamjene.Rows.Add("CBFF ", "Kapitalna štednja Transakcija se odnosi na kapitalnu štednju, odnosno štednju za umirovljenje.");
            DTsifraNamjene.Rows.Add("CCRD ", "Plaćanje kreditnom karticom Transakcija se odnosi na plaćanje kreditnom karticom.");
            DTsifraNamjene.Rows.Add("CDBL ", "Plaćanje troškova učinjenih kreditnom karticom Transakcija se odnosi na plaćanje računa za troškove učinjene kreditnom karticom. ");
            DTsifraNamjene.Rows.Add("CFEE ", "Naknada za opoziv/storno Transakcija se odnosi na plaćanje naknade za opoziv/storno. ");
            DTsifraNamjene.Rows.Add("COST ", "Troškovi Transakcija se odnosi na plaćanje troškova. ");
            DTsifraNamjene.Rows.Add("DCRD ", "Plaćanje troškova učinjenih debitnom karticom Transakcija se odnosi na plaćanje troškova učinjenih debitnom karticom.");
            DTsifraNamjene.Rows.Add("GOVT ", "Plaćanje državi/države Transakcija predstavlja plaćanje državnom tijelu ili plaćanje od strane državnog tijela.");
            DTsifraNamjene.Rows.Add("ICCP ", "Neopozivo plaćanje kreditnom karticom Transakcija predstavlja povrat plaćanja s izvršenog kreditnom karticom. ");
            DTsifraNamjene.Rows.Add("IDCP ", "Neopozivo plaćanje debitnom karticom Transakcija predstavlja povrat plaćanja Izvršenog debitnom karticom. ");
            DTsifraNamjene.Rows.Add("IHRP ", "Plaćanje rate pri kupnji na otplatu Transakcija predstavlja otplatu rate kod kupnje na otplatu.");
            DTsifraNamjene.Rows.Add("INSM ", "Rata Transakcija se odnosi na plaćanje rate/obroka. ");
            DTsifraNamjene.Rows.Add("MSVC ", "Višenamjenske usluge Transakcija se odnosi na plaćanje višenamjenskih usluga. ");
            DTsifraNamjene.Rows.Add("NOWS ", "Nenavedeno Transakcija se odnosi na plaćanje za usluge koje nisu drugdje navedene.");
            DTsifraNamjene.Rows.Add("OFEE ", "Početna naknada (Opening Fee) Transakcija se odnosi na plaćanje početne naknade. ");
            DTsifraNamjene.Rows.Add("OTHR ", "Ostalo Druga vrsta plaćanja.");
            DTsifraNamjene.Rows.Add("PADD ", "Unaprijed odobreno terećenje Transakcija se odnosi na unaprijed odobreni nalog za terećenje.");
            DTsifraNamjene.Rows.Add("PTSP ", "Uvjeti plaćanja Transakcija se odnosi na specifikaciju uvjeta plaćanja.");
            DTsifraNamjene.Rows.Add("RCKE ", "Ponovna prezentacija čeka Transakcija se odnosi na plaćanje vezano za ponovnu prezentaciju čeka. ");
            DTsifraNamjene.Rows.Add("RCPT ", "Plaćanje potvrde Transakcija se odnosi na izdavanje potvrde o provedenom plaćanju. ");
            DTsifraNamjene.Rows.Add("REFU ", "Povrat Transakcija predstavlja povrat sredstava. ");
            DTsifraNamjene.Rows.Add("RENT ", "Najam Transakcija predstavlja plaćanje najma. ");
            DTsifraNamjene.Rows.Add("STDY ", "Studiranje Transakcija se odnosi na plaćanje troškova studiranja/školarine.");
            DTsifraNamjene.Rows.Add("TELI ", "Plaćanje putem telefona Transakcija se odnosi na plaćanje koje je inicirano telefonom. ");
            DTsifraNamjene.Rows.Add("WEBI ", "Plaćanje putem interneta Transakcija se odnosi na plaćanje koje je inicirano internetom.");
            DTsifraNamjene.Rows.Add("ANNI ", "Anuitet Transakcija se odnosi na plaćanje auniteta kredita, osiguranja, ulaganja i dr. ");
            DTsifraNamjene.Rows.Add("CMDT ", "Plaćanje roba Transakcija predstavlja plaćanje roba.");
            DTsifraNamjene.Rows.Add("DERI ", "Derivativi (izvedenice) Transakcija se odnosi na poslove sderivativima. ");
            DTsifraNamjene.Rows.Add("DIVD ", "Dividenda Transakcija predstavlja plaćanje dividendi.");
            DTsifraNamjene.Rows.Add("FREX ", "Kupoprodaja deviza Transakcija se odnosi na poslove deviznog tržišta.");
            DTsifraNamjene.Rows.Add("HEDG ", "Hedging Transakcija se odnosi na operaciju hedginga.");
            DTsifraNamjene.Rows.Add("PRME ", "Plemeniti metali Transakcija se odnosi na poslovanje s plemenitim metalima.");
            DTsifraNamjene.Rows.Add("SAVG ", "Štednja Prijenos na račun štednje/mirovine.");
            DTsifraNamjene.Rows.Add("SECU ", "Vrijednosni papiri Transakcija predstavlja plaćanje vrijednosnih papira.");
            DTsifraNamjene.Rows.Add("TREA ", "Riznični transferi Transakcija se odnosi na riznično poslovanje.");
            DTsifraNamjene.Rows.Add("ANTS ", "Usluge anestezije Transakcija predstavlja plaćanje za usluge anestezije.");
            DTsifraNamjene.Rows.Add("CVCF ", "Usluge skrbi za rekonvalescente Transakcija predstavlja plaćanje za usluge skrbi za rekonvalescente. ");
            DTsifraNamjene.Rows.Add("DMEQ ", "Medicinska oprema Transakcija predstavlja plaćanje za nabavu trajne medicinske opreme");
            DTsifraNamjene.Rows.Add("DNTS ", "Zubarske usluge Transakcija predstavlja plaćanje za zubarske usluge.");
            DTsifraNamjene.Rows.Add("HLTC ", "Kućna njega bolesnika Transakcija predstavlja plaćanje za usluge kućne njege bolesnika.");
            DTsifraNamjene.Rows.Add("HLTI ", "Zdravstveno osiguranje Transakcija predstavlja plaćanje zdravstvenog osiguranja. ");
            DTsifraNamjene.Rows.Add("HSPC ", "Bolnička njega Transakcija predstavlja plaćanje za usluge bolničke njege.");
            DTsifraNamjene.Rows.Add("ICRF ", "Ustanova socijalne skrbi Transakcija predstavlja plaćanje usluga ustanove socijalne skrbi.");
            DTsifraNamjene.Rows.Add("LTCF ", "Ustanova dugoročne zdravstvene skrbi Transakcija predstavlja plaćanje usluga ustanove dugoročne zdravstvene skrbi.");
            DTsifraNamjene.Rows.Add("MDCS ", "Zdravstvene usluge Transakcija predstavlja plaćanje za zdravstvene usluge. ");
            DTsifraNamjene.Rows.Add("VIEW ", "Oftalmološke/okulističke usluge Transakcija predstavlja plaćanje za oftamološke/okulističke usluge. ");
            DTsifraNamjene.Rows.Add("ALMY ", "Plaćanje alimentacije Transakcija predstavlja plaćanje alimentacije.");
            DTsifraNamjene.Rows.Add("BECH ", "Dječji doplatak Transakcija se odnosi na plaćanje kojim se pomaže roditelju/staratelju u uzdržavanju djeteta.");
            DTsifraNamjene.Rows.Add("BENE ", "Naknada za nezaposlenost/invaliditet Transakcija se odnosi na plaćanje osobi koja je nezaposlena/invalid. ");
            DTsifraNamjene.Rows.Add("BONU ", "Novčana nagrada (bonus) Transakcija se odnosi na plaćanje novčane nagrade (bonusa).");
            DTsifraNamjene.Rows.Add("COMM ", "Provizija Transakcija predstavlja plaćanje provizije. ");
            DTsifraNamjene.Rows.Add("CSLP ", "Isplata socijalnih zajmova društava banci Transakcija predstavlja plaćanje društva banci u svrhu financiranja socijalnih zajmova zaposlenicima.");
            DTsifraNamjene.Rows.Add("GVEA ", "Austrijski državni službenici, Kategorija A Transakcija predstavlja plaćanje kategoriji A austrijskih državnih službenika.");
            DTsifraNamjene.Rows.Add("GVEB ", "Austrijski državni službenici, Kategorija B Transakcija predstavlja plaćanje kategoriji B austrijskih državnih službenika ");
            DTsifraNamjene.Rows.Add("GVEC ", "Austrijski državni službenici, Kategorija C Transakcija predstavlja plaćanje kategoriji C austrijskih državnih službenika");
            DTsifraNamjene.Rows.Add("GVED ", "Austrijski državni službenici, Kategorija D Transakcija predstavlja plaćanje kategoriji D austrijskih državnih službenika");
            DTsifraNamjene.Rows.Add("PAYR ", "Platni spisak Transakcija se odnosi na isplatu plaća prema platnom spisku. ");
            DTsifraNamjene.Rows.Add("PENS ", "Mirovine Transakcija predstavlja isplatu mirovine.");
            DTsifraNamjene.Rows.Add("PRCP ", "Plaćanje troškova Transakcija se odnosi na plaćanje troškova.");
            DTsifraNamjene.Rows.Add("SALA ", "Plaće Transakcija predstavlja isplatu plaće. ");
            DTsifraNamjene.Rows.Add("SSBE ", "Socijalna pomoć Transakcija predstavlja naknadu za socijalnu pomić odnosno plaćanje kojeg izvršava država kao socijalnu potporu pojedincima. ");
            DTsifraNamjene.Rows.Add("ESTX ", "Porez na nasljedstvo Transakcija se odnosi na plaćanje poreza na nasljedstvo.");
            DTsifraNamjene.Rows.Add("HSTX ", "Porez na stambeni prostor Transakcija se odnosi na plaćanje poreza na stambeni prostor. ");
            DTsifraNamjene.Rows.Add("INTX ", "Porez na dohodak Transakcija se odnosi na plaćanje poreza na dohodak.");
            DTsifraNamjene.Rows.Add("NITX ", "Porez na neto dohodak Transakcija se odnosi na plaćanje poreza na neto dohodak");
            DTsifraNamjene.Rows.Add("TAXS ", "Plaćanje poreza Transakcija predstavlja plaćanje poreza.");
            DTsifraNamjene.Rows.Add("VATX ", "Plaćanje poreza na dodanu vrijednost Transakcija predstavlja plaćanje poreza na dodanu vrijednost.");
            DTsifraNamjene.Rows.Add("WHLD ", "Porez po odbitku Transakcija se odnosi na plaćanje poreza po odbitku.");
            DTsifraNamjene.Rows.Add("AIRB ", "Zračni Transakcija predstavlja plaćanje za poslove vezane uz zračni prijevoz. ");
            DTsifraNamjene.Rows.Add("BUSB ", "Autobusni Transakcija predstavlja plaćanje za poslove vezane uz autobusni prijevoz.");
            DTsifraNamjene.Rows.Add("FERB ", "Trajektni Transakcija predstavlja plaćanje za poslove vezane uz trajektni prijevoz.");
            DTsifraNamjene.Rows.Add("RLWY ", "Željeznički Transakcija predstavlja plaćanje za poslove vezane uz željeznički prijevoz.");
            DTsifraNamjene.Rows.Add("CBTV ", "Račun za kabelsku TV Transakcija se odnosi na plaćanje računa za kabelsku TV.");
            DTsifraNamjene.Rows.Add("ELEC ", "Račun za električnu energiju Transakcija se odnosi na plaćanje računa za električnu energiju. ");
            DTsifraNamjene.Rows.Add("ENRG ", "Energija Transakcija se odnosi na plaćanje energije.");
            DTsifraNamjene.Rows.Add("GASB ", "Račun za plin Transakcija se odnosi na plaćanje računa za plin.");
            DTsifraNamjene.Rows.Add("NWCH ", "Troškovi za mrežu Transakcija se odnosi na plaćanje troškova za korištenje mreže.");
            DTsifraNamjene.Rows.Add("NWCM ", "Mrežna komunikacija Transakcija se odnosi na plaćanje mrežne komunikacije.");
            DTsifraNamjene.Rows.Add("OTLC ", "Račun za ostale telekomunikacijske usluge Transakcija se odnosi na plaćanje računa za ostale telekomunikacijske usluge.");
            DTsifraNamjene.Rows.Add("PHON ", "Račun za telefon Transakcija se odnosi na plaćanje računa za telefon.");
            DTsifraNamjene.Rows.Add("WTER ", "Račun za vodu Transakcija se odnosi na plaćanje računa za vodu.");

            DataView dv = DTsifraNamjene.DefaultView;
            dv.Sort = "naziv asc";
            DataTable sortedDT = dv.ToTable();

            cbSifraNamjene.DataSource = sortedDT;
            cbSifraNamjene.DisplayMember = "naziv";
            cbSifraNamjene.ValueMember = "id";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TRENUTNI_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.Khaki;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.Khaki;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.Khaki;
            }
        }

        private void NAPUSTENI_Leave(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                TextBox txt = ((TextBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is ComboBox)
            {
                ComboBox txt = ((ComboBox)sender);
                txt.BackColor = Color.White;
            }
            else if (sender is DateTimePicker)
            {
                DateTimePicker control = ((DateTimePicker)sender);
                control.BackColor = Color.White;
            }
            else if (sender is NumericUpDown)
            {
                NumericUpDown control = ((NumericUpDown)sender);
                control.BackColor = Color.White;
            }
        }

        private void btnPartner_Click(object sender, EventArgs e)
        {
            frmPartnerTrazi partnerTrazi = new frmPartnerTrazi();
            partnerTrazi.ShowDialog();
            if (Properties.Settings.Default.id_partner != "")
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT * FROM partners " +
                    " LEFT JOIN grad ON grad.id_grad=partners.id_grad" +
                    " WHERE id_partner ='" + Properties.Settings.Default.id_partner + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtPrimateljSifra.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtPrimateljAdresa.Text = partner.Tables[0].Rows[0]["adresa"].ToString() + " " + partner.Tables[0].Rows[0]["grad"].ToString();

                    txtPrimateljNazivBanke.Text = partner.Tables[0].Rows[0]["naziv_banke"].ToString();
                    txtPrimateljAdresaBanke.Text = partner.Tables[0].Rows[0]["adresa_banke"].ToString();
                    txtPrimateljSifraZemljeBanke.Text = partner.Tables[0].Rows[0]["sifra_zemlje_banke"].ToString();
                    txtPrimateljSjedisteBanke.Text = partner.Tables[0].Rows[0]["sjediste_banke"].ToString();
                    txtPrimateljSWIFT.Text = partner.Tables[0].Rows[0]["swift"].ToString();
                    txtNaIbanPrimatelja.Text = partner.Tables[0].Rows[0]["iban"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }
            }
        }

        private void ttxBrojFakture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable DT = classSQL.select("SELECT broj FROM ulazna_faktura WHERE godina='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + ttxBrojFakture.Text + "'", "ufa").Tables[0];
                deleteFields();
                if (DT.Rows.Count == 0)
                {
                    if (brojUFA() == ttxBrojFakture.Text.Trim())
                    {
                        deleteFields();
                        edit = false;
                        EnableDisable(true, false);
                        btnSveFakture.Enabled = false;
                        //ttxBrojFakture.Text = brojFakture();
                        btnDeleteAllFaktura.Enabled = false;
                        txtPrimateljSifra.Select();
                    }
                    else
                    {
                        MessageBox.Show("Odabrani zahtjev ne postoji.", "Greška");
                    }
                }
                else if (DT.Rows.Count == 1)
                {
                    broj_fakture_edit = ttxBrojFakture.Text;
                    EnableDisable(true, false);
                    edit = true;
                    Fill();
                    btnDeleteAllFaktura.Enabled = true;
                    txtPrimateljSifra.Select();
                }
                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void deleteFields()
        {
            txtPartnerNaziv.Text = "";
            txtPrimateljAdresa.Text = "";
            txtPrimateljSjedistePrimatelja.Text = "";
            txtPrimateljSifraZemljePrimatelja.Text = "";
            txtPrimateljSWIFT.Text = "";
            txtPrimateljNazivBanke.Text = "";
            txtPrimateljAdresaBanke.Text = "";
            txtPrimateljSjedisteBanke.Text = "";
            txtPrimateljSifraZemljeBanke.Text = "";
            txtModel.Text = "";
            txtPozivNaBroj.Text = "";
            txtNaIbanPrimatelja.Text = "";
            txtModelPrimatelja.Text = "";
            txtPozivNaBrojPrimatelja.Text = "";
            rtbopisPlacanja.Text = "";
            txtPrimateljSifra.Text = "";

            txtUkupno.Text = "0";
        }

        private void EnableDisable(bool x, bool y)
        {
            nmGodinaFakture.Enabled = y;
            ttxBrojFakture.Enabled = y;

            txtPartnerNaziv.Enabled = x;
            txtPrimateljAdresa.Enabled = x;
            txtPrimateljSjedistePrimatelja.Enabled = x;
            txtPrimateljSifraZemljePrimatelja.Enabled = x;
            txtPrimateljSWIFT.Enabled = x;
            txtPrimateljNazivBanke.Enabled = x;
            txtPrimateljAdresaBanke.Enabled = x;
            txtPrimateljSjedisteBanke.Enabled = x;
            txtPrimateljSifraZemljeBanke.Enabled = x;
            txtModel.Enabled = x;
            txtPozivNaBroj.Enabled = x;
            txtNaIbanPrimatelja.Enabled = x;
            txtModelPrimatelja.Enabled = x;
            txtPozivNaBrojPrimatelja.Enabled = x;
            rtbopisPlacanja.Enabled = x;
            txtPrimateljSifra.Enabled = x;
        }

        private void Fill()
        {
            //DataTable DTfill = classSQL.select("SELECT * FROM ulazna_faktura WHERE godina='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + broj_fakture_edit + "'", "ufa").Tables[0];
            DataTable DTfill = classSQL.select("SELECT * FROM ulazna_faktura WHERE EXTRACT(YEAR FROM datum_izvrsenja)='" + nmGodinaFakture.Value.ToString() + "' AND broj='" + broj_fakture_edit + "'", "ufa").Tables[0];

            if (DTfill.Rows.Count > 0)
            {
                DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka  LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad WHERE id='1'", "podaci_tvrtka").Tables[0];
                if (DTpodaci.Rows.Count > 0)
                {
                    txtPlatitelj.Text = DTpodaci.Rows[0]["ime_tvrtke"].ToString() + "\r\n" +
                        DTpodaci.Rows[0]["adresa"].ToString() + "\r\n" +
                        DTpodaci.Rows[0]["grad"].ToString();
                }

                ttxBrojFakture.Text = DTfill.Rows[0]["broj"].ToString();
                txtPartnerNaziv.Text = DTfill.Rows[0]["primatelj_naziv"].ToString();
                txtPrimateljAdresa.Text = DTfill.Rows[0]["primatelj_adresa"].ToString();
                txtPrimateljSjedistePrimatelja.Text = DTfill.Rows[0]["primatelj_sjediste"].ToString();
                txtPrimateljSifraZemljePrimatelja.Text = DTfill.Rows[0]["primatelj_sifra_zemlje"].ToString();
                txtPrimateljSWIFT.Text = DTfill.Rows[0]["primatelj_swift"].ToString();
                txtPrimateljNazivBanke.Text = DTfill.Rows[0]["primatelj_naziv_banke"].ToString();
                txtPrimateljAdresaBanke.Text = DTfill.Rows[0]["primatelj_adresa_banke"].ToString();
                txtPrimateljSjedisteBanke.Text = DTfill.Rows[0]["primatelj_sjediste_banke"].ToString();
                txtPrimateljSifraZemljeBanke.Text = DTfill.Rows[0]["primatelj_sifra_zemlje_banke"].ToString();
                cbTroskovnaOpcija.SelectedValue = DTfill.Rows[0]["primatelj_troskovna_opcija"].ToString();
                cbVrstaStraneOsobe.SelectedValue = DTfill.Rows[0]["primatelj_vrste_strane_osobe"].ToString();
                txtValutaPokrica.Text = DTfill.Rows[0]["valuta_pokrica"].ToString();
                cbValuta.SelectedValue = DTfill.Rows[0]["valuta"].ToString();
                decimal ukupno; decimal.TryParse(DTfill.Rows[0]["iznos"].ToString(), out ukupno);
                txtUkupno.Text = ukupno.ToString("#0.00");
                cbSziroRacuna.Text = DTfill.Rows[0]["iban_platitelja"].ToString();
                txtModel.Text = DTfill.Rows[0]["model_platitelja"].ToString();
                txtPozivNaBroj.Text = DTfill.Rows[0]["poziv_na_broj_platitelja"].ToString();
                txtNaIbanPrimatelja.Text = DTfill.Rows[0]["iban_primatelja"].ToString();
                txtModelPrimatelja.Text = DTfill.Rows[0]["model_primatelja"].ToString();
                txtPozivNaBrojPrimatelja.Text = DTfill.Rows[0]["poziv_na_broj_primatelja"].ToString();
                cbSifraNamjene.SelectedValue = DTfill.Rows[0]["sifra_namjene"].ToString();
                rtbopisPlacanja.Text = DTfill.Rows[0]["opis_placanja"].ToString();
                DateTime dat;
                DateTime.TryParse(DTfill.Rows[0]["datum_izvrsenja"].ToString(), out dat);
                dtpDatumKnjizenja.Value = dat;
                if (DTfill.Rows[0]["oznaka_hitnosti"].ToString() == "1")
                    chbHitnoIzvrsiti.Checked = true;
                else
                    chbHitnoIzvrsiti.Checked = false;

                //DataTable DTp = classSQL.select("SELECT id_partner FROM partners WHERE ime_tvrtke='" + DTfill.Rows[0]["primatelj_naziv"].ToString() + "' LIMIT 1", "partners").Tables[0];

                //if(DTp.Rows.Count>0)
                //txtPrimateljSifra.Text=DTp.Rows[0][0].ToString();

                txtPrimateljSifra.Text = DTfill.Rows[0]["primatelj_sifra"].ToString();
                txtPrimateljSifra.Select();
                System.Windows.Forms.SendKeys.Send("{ENTER}");
                //txtPrimateljSifra_KeyDown(null,null);

                ControlDisableEnable(0, 1, 1, 0, 1);
                EnableDisable(true, false);
            }
        }

        private string brojUFA()
        {
            DataTable DSbr = classSQL.select("SELECT MAX(broj) FROM ulazna_faktura", "ulazna_faktura").Tables[0];
            if (DSbr.Rows[0][0].ToString() != "")
            {
                return (Convert.ToDouble(DSbr.Rows[0][0].ToString()) + 1).ToString();
            }
            else
            {
                return "1";
            }
        }

        private void ControlDisableEnable(int novi, int odustani, int spremi, int sve, int delAll)
        {
            if (novi == 0)
            {
                btnNoviUnos.Enabled = false;
            }
            else if (novi == 1)
            {
                btnNoviUnos.Enabled = true;
            }

            if (odustani == 0)
            {
                btnOdustani.Enabled = false;
            }
            else if (odustani == 1)
            {
                btnOdustani.Enabled = true;
            }

            if (spremi == 0)
            {
                btnSpremi.Enabled = false;
            }
            else if (spremi == 1)
            {
                btnSpremi.Enabled = true;
            }

            if (sve == 0)
            {
                btnSveFakture.Enabled = false;
            }
            else if (sve == 1)
            {
                btnSveFakture.Enabled = true;
            }

            if (delAll == 0)
            {
                btnDeleteAllFaktura.Enabled = false;
            }
            else if (delAll == 1)
            {
                btnDeleteAllFaktura.Enabled = true;
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            decimal dec_parse;
            if (!Decimal.TryParse(txtPrimateljSifra.Text, out dec_parse))
            {
                // MessageBox.Show("Greška kod upisa odredišta.", "Greška"); return;
            }

            if (rtbopisPlacanja.Text == "")
            {
                MessageBox.Show("Greška!!!\r\nOpis ne smije biti prazan.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            decimal iii;
            decimal.TryParse(txtUkupno.Text, out iii);
            if (iii == 0)
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani iznos.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            if (cbSziroRacuna.Text == "")
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani žiro račun.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            if (txtNaIbanPrimatelja.Text == "")
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani žiro račun primatelja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            if (txtPartnerNaziv.Text == "")
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani naziv primatelja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            if (!IsIbanChecksumValid(cbSziroRacuna.Text))
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani broj IBAN platitelja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            if (!IsIbanChecksumValid(txtNaIbanPrimatelja.Text))
            {
                MessageBox.Show("Greška!!!\r\nKrivo upisani broj IBAN primatelja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            string oznaka_hitrosti = chbHitnoIzvrsiti.Checked ? "1" : "0";

            if (edit == false)
            {
                string sql = "INSERT INTO ulazna_faktura (" +
                         " broj," +
                         " godina," +
                         " primatelj_naziv," +
                         " primatelj_adresa," +
                         " primatelj_sjediste," +
                         " primatelj_sifra_zemlje," +
                         " primatelj_swift," +
                         " primatelj_naziv_banke," +
                         " primatelj_adresa_banke," +
                         " primatelj_sjediste_banke," +
                         " primatelj_sifra_zemlje_banke," +
                         " primatelj_troskovna_opcija," +
                         " primatelj_vrste_strane_osobe," +
                         " valuta_pokrica," +
                         " valuta," +
                         " iznos," +
                         " iban_platitelja," +
                         " model_platitelja," +
                         " poziv_na_broj_platitelja," +
                         " iban_primatelja," +
                         " model_primatelja," +
                         " poziv_na_broj_primatelja," +
                         " sifra_namjene," +
                         " opis_placanja," +
                         " datum_izvrsenja," +
                         " id_zaposlenik," +
                         " izvor_dokumenta," +
                         " vrsta_naloga," +
                         " primatelj_sifra," +
                         " oznaka_hitnosti" +
                    ") VALUES (" +
                    "'" + ttxBrojFakture.Text + "'," +
                    "'" + nmGodinaFakture.Value.ToString() + "'," +
                    "'" + txtPartnerNaziv.Text + "'," +
                    "'" + txtPrimateljAdresa.Text + "'," +
                    "'" + txtPrimateljSjedistePrimatelja.Text + "'," +
                    "'" + txtPrimateljSifraZemljePrimatelja.Text + "'," +
                    "'" + txtPrimateljSWIFT.Text + "'," +
                    "'" + txtPrimateljNazivBanke.Text + "'," +
                    "'" + txtPrimateljAdresaBanke.Text + "'," +
                    "'" + txtPrimateljSjedisteBanke.Text + "'," +
                    "'" + txtPrimateljSifraZemljeBanke.Text + "'," +
                    "'" + cbTroskovnaOpcija.SelectedValue.ToString() + "'," +
                    "'" + cbVrstaStraneOsobe.SelectedValue.ToString() + "'," +
                    "'" + txtValutaPokrica.Text + "'," +
                    "'" + cbValuta.SelectedValue.ToString() + "'," +
                    "'" + txtUkupno.Text.Replace(",", ".") + "'," +
                    "'" + cbSziroRacuna.Text + "'," +
                    "'" + txtModel.Text + "'," +
                    "'" + txtPozivNaBroj.Text + "'," +
                    "'" + txtNaIbanPrimatelja.Text + "'," +
                    "'" + txtModelPrimatelja.Text + "'," +
                    "'" + txtPozivNaBrojPrimatelja.Text + "'," +
                    "'" + cbSifraNamjene.SelectedValue.ToString() + "'," +
                    "'" + rtbopisPlacanja.Text + "'," +
                    "'" + dtpDatumKnjizenja.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "'" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "'" + cbIzvorDokumenata.SelectedValue.ToString() + "'," +
                    "'" + cbVrstaNaloga.SelectedValue.ToString() + "'," +
                    "'" + txtPrimateljSifra.Text + "'," +
                    "'" + oznaka_hitrosti + "'" +
                    ");";

                provjera_sql(classSQL.insert(sql));
            }
            else
            {
                string sql = "UPDATE ulazna_faktura SET " +
                    "primatelj_naziv='" + txtPartnerNaziv.Text + "'," +
                    "primatelj_sifra='" + txtPrimateljSifra.Text + "'," +
                    "primatelj_adresa='" + txtPrimateljAdresa.Text + "'," +
                    "primatelj_sjediste='" + txtPrimateljSjedistePrimatelja.Text + "'," +
                    "primatelj_sifra_zemlje='" + txtPrimateljSifraZemljePrimatelja.Text + "'," +
                    "primatelj_swift='" + txtPrimateljSWIFT.Text + "'," +
                    "primatelj_naziv_banke='" + txtPrimateljNazivBanke.Text + "'," +
                    "primatelj_adresa_banke='" + txtPrimateljAdresaBanke.Text + "'," +
                    "primatelj_sjediste_banke='" + txtPrimateljSjedisteBanke.Text + "'," +
                    "primatelj_sifra_zemlje_banke='" + txtPrimateljSifraZemljeBanke.Text + "'," +
                    "primatelj_troskovna_opcija='" + cbTroskovnaOpcija.SelectedValue.ToString() + "'," +
                    "primatelj_vrste_strane_osobe='" + cbVrstaStraneOsobe.SelectedValue.ToString() + "'," +
                    "valuta_pokrica='" + txtValutaPokrica.Text + "'," +
                    "valuta='" + cbValuta.SelectedValue.ToString() + "'," +
                    "iznos='" + txtUkupno.Text.Replace(",", ".") + "'," +
                    "iban_platitelja='" + cbSziroRacuna.Text + "'," +
                    "model_platitelja='" + txtModel.Text + "'," +
                    "poziv_na_broj_platitelja='" + txtPozivNaBroj.Text + "'," +
                    "iban_primatelja='" + txtNaIbanPrimatelja.Text + "'," +
                    "model_primatelja='" + txtModelPrimatelja.Text + "'," +
                    "poziv_na_broj_primatelja='" + txtPozivNaBrojPrimatelja.Text + "'," +
                    "sifra_namjene='" + cbSifraNamjene.SelectedValue.ToString() + "'," +
                    "opis_placanja='" + rtbopisPlacanja.Text + "'," +
                    "datum_izvrsenja='" + dtpDatumKnjizenja.Value.ToString("yyyy-MM-dd H:mm:ss") + "'," +
                    "izvor_dokumenta='" + cbIzvorDokumenata.SelectedValue.ToString() + "'," +
                    "vrsta_naloga='" + cbVrstaNaloga.SelectedValue.ToString() + "'," +
                    "id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'," +
                    "oznaka_hitnosti='" + oznaka_hitrosti + "'" +
                    " WHERE broj='" + ttxBrojFakture.Text + "' AND godina='" + nmGodinaFakture.Value.ToString() + "'";

                provjera_sql(classSQL.update(sql));
            }

            if (txtPrimateljSifra.Text != "")
            {
                classSQL.update("UPDATE partners SET iban='" + txtNaIbanPrimatelja.Text +
                    "',naziv_banke='" + txtPrimateljNazivBanke.Text +
                    "',adresa_banke='" + txtPrimateljAdresaBanke.Text +
                    "',sjediste_banke='" + txtPrimateljSjedistePrimatelja.Text +
                    "',sifra_zemlje_banke='" + txtPrimateljSifraZemljeBanke.Text +
                    "',swift='" + txtPrimateljSWIFT.Text +
                    "'  WHERE id_partner='" + txtPrimateljSifra.Text + "'");
            }

            deleteFields();
            EnableDisable(false, true);
            ControlDisableEnable(1, 0, 0, 1, 0);
            MessageBox.Show("Spremljeno.", "Spremljeno.");
            edit = false;
        }

        private bool IsIbanChecksumValid(string iban)
        {
            try
            {
                if (iban.Length < 4 || iban[0] == ' ' || iban[1] == ' ' || iban[2] == ' ' || iban[3] == ' ') throw new InvalidOperationException();

                var checksum = 0;
                var ibanLength = iban.Length;
                for (int charIndex = 0; charIndex < ibanLength; charIndex++)
                {
                    if (iban[charIndex] == ' ') continue;

                    int value;
                    var c = iban[(charIndex + 4) % ibanLength];
                    if ((c >= '0') && (c <= '9'))
                    {
                        value = c - '0';
                    }
                    else if ((c >= 'A') && (c <= 'Z'))
                    {
                        value = c - 'A';
                        checksum = (checksum * 10 + (value / 10 + 1)) % 97;
                        value %= 10;
                    }
                    else if ((c >= 'a') && (c <= 'z'))
                    {
                        value = c - 'a';
                        checksum = (checksum * 10 + (value / 10 + 1)) % 97;
                        value %= 10;
                    }
                    else throw new InvalidOperationException();

                    checksum = (checksum * 10 + value) % 97;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            btnSveFakture.Enabled = true;
            EnableDisable(false, true);
            deleteFields();
            ttxBrojFakture.Text = brojUFA();
            edit = false;
            ttxBrojFakture.ReadOnly = false;
            nmGodinaFakture.ReadOnly = false;

            ControlDisableEnable(1, 0, 0, 1, 0);
        }

        private void btnDeleteAllFaktura_Click(object sender, EventArgs e)
        {
            classSQL.delete("DELETE FROM ulazna_faktura WHERE broj='" + ttxBrojFakture.Text + "' AND godina='" + nmGodinaFakture.Value.ToString() + "'");
            MessageBox.Show("Uspješno obrisano.");
            edit = false;
        }

        private void btnSveFakture_Click(object sender, EventArgs e)
        {
            //this.MdiParent.Controls.Contains(frmSveUlazneFakture)
            //foreach(Control c in this.MdiParent.Controls){
            //    if ((c is Form) && (c as Form).Name == "frmSveUlazneFakture") {
            //        (c as Form).BringToFront();

            //    }
            //}
            frmSveUlazneFakture Fu = new frmSveUlazneFakture();
            Fu.MainForm = this;
            Fu.ShowDialog();
            if (broj_fakture_edit != null)
            {
                deleteFields();
                Fill();
                edit = true;
            }
        }

        private void btnNoviUnos_Click(object sender, EventArgs e)
        {
            DataTable DTpodaci = classSQL.select_settings("SELECT * FROM podaci_tvrtka LEFT JOIN grad ON grad.id_grad=podaci_tvrtka.id_grad WHERE id='1'", "podaci_tvrtka").Tables[0];
            if (DTpodaci.Rows.Count > 0)
            {
                txtPlatitelj.Text = DTpodaci.Rows[0]["ime_tvrtke"].ToString() + "\r\n" +
                    DTpodaci.Rows[0]["adresa"].ToString() + "\r\n" +
                    DTpodaci.Rows[0]["grad"].ToString();

                cbSziroRacuna.Text = DTpodaci.Rows[0]["iban"].ToString();
                txtModel.Text = "HR99";
                txtModelPrimatelja.Text = "HR99";
            }

            edit = false;
            EnableDisable(true, false);
            ttxBrojFakture.Text = brojUFA(); ;
            ControlDisableEnable(0, 1, 1, 0, 1);
            txtPrimateljSifra.Select();
        }

        private void txtPrimateljSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataSet partner = new DataSet();
                partner = classSQL.select("SELECT * FROM partners " +
                    " LEFT JOIN grad ON grad.id_grad=partners.id_grad" +
                    " WHERE id_partner ='" + txtPrimateljSifra.Text + "'", "partners");
                if (partner.Tables[0].Rows.Count > 0)
                {
                    txtPrimateljSifra.Text = partner.Tables[0].Rows[0]["id_partner"].ToString();
                    txtPartnerNaziv.Text = partner.Tables[0].Rows[0]["ime_tvrtke"].ToString();
                    txtPrimateljAdresa.Text = partner.Tables[0].Rows[0]["adresa"].ToString() + " " + partner.Tables[0].Rows[0]["grad"].ToString();

                    txtPrimateljNazivBanke.Text = partner.Tables[0].Rows[0]["naziv_banke"].ToString();
                    txtPrimateljAdresaBanke.Text = partner.Tables[0].Rows[0]["adresa_banke"].ToString();
                    txtPrimateljSifraZemljeBanke.Text = partner.Tables[0].Rows[0]["sifra_zemlje_banke"].ToString();
                    txtPrimateljSjedisteBanke.Text = partner.Tables[0].Rows[0]["sjediste_banke"].ToString();
                    txtPrimateljSWIFT.Text = partner.Tables[0].Rows[0]["swift"].ToString();
                    txtNaIbanPrimatelja.Text = partner.Tables[0].Rows[0]["iban"].ToString();
                }
                else
                {
                    MessageBox.Show("Upisana šifra ne postoji.", "Greška");
                }

                ControlDisableEnable(0, 1, 1, 0, 1);
            }
        }

        private void frmSkeniraniDoc_Click(object sender, EventArgs e)
        {
            string filePDF = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "UFA_" +
                            ttxBrojFakture.Text + "_" + nmGodinaFakture.Value.ToString() + ".pdf";
            string fileJpg = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "UFA_" +
                            ttxBrojFakture.Text + "_" + nmGodinaFakture.Value.ToString() + ".jpg";

            if (File.Exists(filePDF))
            {
                OpenFile(filePDF);
            }
            else if (File.Exists(fileJpg))
            {
                OpenFile(fileJpg);
            }
            else
            {
                MoveFile();
            }
        }

        private void ttxBrojFakture_TextChanged(object sender, EventArgs e)
        {
            ProvjeriDaliPostojiSkeniraniDok();
        }

        private void MoveFile()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "JPG|*.jpg;*.jpeg|PDF|*.pdf|"
       + "All Graphics Types|*.pdf;*.jpg";

            openFileDialog1.FilterIndex = 7;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        if (!Directory.Exists("skenirani_fajlovi"))
                            Directory.CreateDirectory("skenirani_fajlovi");

                        string fileDestination = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "UFA_" +
                            ttxBrojFakture.Text + "_" + DateTime.Now.Year + Path.GetExtension(openFileDialog1.FileName);

                        try
                        {
                            if (File.Exists(fileDestination))
                                File.Delete(fileDestination);
                        }
                        catch (Exception ex)
                        { MessageBox.Show("Greška: Ne mogu obrisati dokumenat iz diska.\r\n Orginalna greška: " + ex.Message); }

                        File.Move(openFileDialog1.FileName, fileDestination);
                        ProvjeriDaliPostojiSkeniraniDok();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: Ne mogu učitati dokumenat iz diska.\r\n Orginalna greška: " + ex.Message);
                }
            }
        }

        private void OpenFile(string fileDestination)
        {
            if (MessageBox.Show("Datoteka za ovaj dokumenat već postoji.\r\nAko želite otvoriti dokumenat pritisnite 'YES'\r\na ako želite urediti postavljeni dokumenat pritisnite 'NO'.", "Uredi/Otvori",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                MoveFile();
                ProvjeriDaliPostojiSkeniraniDok();
            }
            else
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.FileName = fileDestination;
                proc.Start();
            }
        }

        private void ProvjeriDaliPostojiSkeniraniDok()
        {
            string filePDF = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "UFA_" +
                            ttxBrojFakture.Text + "_" + nmGodinaFakture.Value.ToString() + ".pdf";
            string fileJpg = DTpostavke.Rows[0]["putanja_za_skenirane_fajlove"].ToString() + "UFA_" +
                            ttxBrojFakture.Text + "_" + nmGodinaFakture.Value.ToString() + ".jpg";

            if (File.Exists(filePDF) || File.Exists(fileJpg))
            {
                frmSkeniraniDoc.Image = global::PCPOS.Properties.Resources.Done;
                this.toolTip1.SetToolTip(this.frmSkeniraniDoc, "Skenirani dokumenat postoji na adresi: " + fileJpg);
            }
            else
            {
                frmSkeniraniDoc.Image = global::PCPOS.Properties.Resources.scanner1;
                this.toolTip1.SetToolTip(this.frmSkeniraniDoc, "Skenirani dokumenat ne postoji.");
            }
        }
    }
}