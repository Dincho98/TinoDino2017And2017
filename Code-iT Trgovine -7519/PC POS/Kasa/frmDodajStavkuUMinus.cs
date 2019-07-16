using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS.Kasa
{
    public partial class frmDodajStavkuUMinus : Form
    {
        private string _dokumenat = "racun";
        public string dokumenat { get { return _dokumenat; } internal set { _dokumenat = value; } }

        public frmDodajStavkuUMinus()
        {
            InitializeComponent();
        }

        private void frmDodajStavkuUMinus_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable DTDB = classSQL.select("SELECT datname FROM pg_database WHERE datistemplate IS FALSE AND datallowconn IS TRUE AND datname!='postgres' ORDER BY datname asc;", "").Tables[0];

                for (int i = 0; i < DTDB.Rows.Count; i++)
                {
                    cmbBaza.Items.Add(DTDB.Rows[i][0].ToString());
                }

                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
                XDocument xmlFile = XDocument.Load(path);
                var query = from c in xmlFile.Element("settings").Elements("database_remote").Elements("postgree") select c;
                foreach (XElement book in query)
                {
                    cmbBaza.Text = book.Attribute("database").Value;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSrch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBaza.Items.Count == 0)
                {
                    MessageBox.Show("Nemate niti jednu bazu.");
                    return;
                }

                string s = txtInput.Text.Trim();
                if (s.Split('/').Count() != 3)
                {
                    MessageBox.Show("Krivi unos za broj računa!" + Environment.NewLine + "Upišite broj računa u punom formatu(broj/poslovnica/naplatni).");
                    return;
                }
                string[] sss = s.Split('/');
                int br = 0;
                bool b = int.TryParse(sss[0], out br);
                if (!b)
                {
                    MessageBox.Show("Krivi unos za broj računa!" + Environment.NewLine + "Upišite broj računa u punom formatu(broj/poslovnica/naplatni).");
                    return;
                }
                string sql = "";
                if (dokumenat == "racun")
                {
                    sql = string.Format(@"CREATE EXTENSION if not exists dblink;

SELECT broj_racuna as [Broj računa],
ime_ducana as [Poslovnica],
ime_blagajne as [Naplatni],
id_skladiste as [Skladište],
sifra_robe as [Šifra],
naziv as [Naziv],
replace(kolicina, ',', '.')::numeric as [Količina],
nbc::numeric(15, 2) as [NBC],
vpc::numeric(15, 3) as [VPC],
mpc::numeric(15, 2) as [MPC],
replace(porez, ',', '.')::numeric(6, 2) as [PDV],
replace(porez_potrosnja, ',', '.')::numeric(6, 2) as [PP],
replace(rabat, ',', '.')::numeric(15, 6) as [Rabat],
prirez as [Prirez],
porez_na_dohodak as [PND],
prirez_iznos as [Prirez iznos],
porez_na_dohodak_iznos as [PND iznos],
oduzmi as [Oduzmi],
jm as [JM]
FROM dblink('host=localhost dbname={0} user=postgres password=q1w2e3r4',
            'select rs.broj_racuna,
d.ime_ducana,
b.ime_blagajne,
rs.id_skladiste,
rs.sifra_robe,
r.naziv,
rs.kolicina,
rs.nbc,
rs.vpc,
rs.mpc,
rs.porez,
rs.porez_potrosnja,
rs.rabat,
rs.prirez,
rs.porez_na_dohodak,
rs.prirez_iznos,
rs.porez_na_dohodak_iznos,
r.oduzmi,
r.jm
from racun_stavke rs
left
join ducan d on rs.id_ducan = d.id_ducan
left
join blagajna b on rs.id_kasa = b.id_blagajna
left join roba r on rs.sifra_robe = r.sifra')
as t1(broj_racuna character varying(10),
ime_ducana character varying(100),
ime_blagajne character varying(100),
id_skladiste integer,
sifra_robe character varying(30),
naziv character varying,
kolicina character varying,
nbc money,
vpc numeric,
mpc money,
porez character varying(5),
porez_potrosnja character varying(10),
rabat character varying(20),
prirez numeric,
porez_na_dohodak numeric,
prirez_iznos numeric,
porez_na_dohodak_iznos numeric,
oduzmi character(2),
jm character varying(30))
where t1.broj_racuna = '{1}' and t1.ime_ducana = '{2}' and t1.ime_blagajne = '{3}';", cmbBaza.SelectedItem, br, sss[1], sss[2]);
                }
                else if (dokumenat == "faktura")
                {
                    sql = string.Format(@"CREATE EXTENSION if not exists dblink;

SELECT broj_fakture as [Broj fakture],
ime_ducana as [Poslovnica],
ime_blagajne as [Naplatni],
id_skladiste as [Skladište],
sifra as [Šifra],
naziv as [Naziv],
replace(kolicina, ',', '.')::numeric as [Količina],
nbc::numeric(15, 2) as [NBC],
vpc::numeric(15, 3) as [VPC],
replace(rabat, ',', '.')::numeric(15, 6) as [Rabat],
replace(porez, ',', '.')::numeric(6, 2) as [PDV],
replace(porez_potrosnja, ',', '.')::numeric(6, 2) as [PP],
povratna_naknada as [PN],
oduzmi as [Oduzmi],
jm as [JM]
FROM dblink('host=localhost dbname={0} user=postgres password=q1w2e3r4',
            'SELECT fs.broj_fakture,
d.ime_ducana,
b.ime_blagajne,
fs.id_skladiste,
fs.sifra,
r.naziv,
fs.kolicina,
fs.nbc,
fs.vpc,
fs.rabat,
fs.porez,
fs.porez_potrosnja,
fs.povratna_naknada,
r.oduzmi,
r.jm
FROM faktura_stavke fs
left join ducan d on fs.id_ducan = d.id_ducan
left join blagajna b on fs.id_kasa = b.id_blagajna
left join roba r on fs.sifra = r.sifra')
as t1(broj_fakture bigint,
ime_ducana character varying(100),
ime_blagajne character varying(100),
id_skladiste bigint,
sifra character varying(30),
naziv character varying,
kolicina character varying(8),
nbc money,
vpc numeric,
rabat character varying(10),
porez character varying(10),
porez_potrosnja character varying(5),
povratna_naknada numeric,
oduzmi character(2),
jm character varying(30))
where t1.broj_fakture = '{1}' and t1.ime_ducana = '{2}' and t1.ime_blagajne = '{3}';", cmbBaza.SelectedItem, br, sss[1], sss[2]);
                }

                if (sql.Length > 0)
                {
                    DataSet dsRacun = classSQL.select(sql, "racuni");
                    if (dsRacun != null && dsRacun.Tables.Count > 0 && dsRacun.Tables[0] != null && dsRacun.Tables[0].Rows.Count > 0)
                    {
                        dgvRez.DataSource = null;
                        dgvRez.Rows.Clear();
                        dgvRez.DataSource = dsRacun.Tables[0];
                    }
                    else
                    {
                        dgvRez.DataSource = null;
                        dgvRez.Rows.Clear();
                        MessageBox.Show("Nema odabranog računa.");
                    }
                }
                else
                {
                    MessageBox.Show("Nije odabrana tablica.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvRez.Rows.Count > 0)
                {
                    decimal kolicina = 0;
                    decimal oldKolicina = 0;
                    decimal.TryParse(dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["Količina"].Value.ToString(), out oldKolicina);

                    if (MessageBox.Show("Želite dodati odabrani artikl?", "Dodavanje artikla", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        return;

                    if (decimal.TryParse(txtKolicina.Text, out kolicina) && kolicina > 0 && Math.Abs(kolicina) <= oldKolicina)
                    {
                        decimal nbc = 0, vpc = 0, mpc = 0;
                        decimal.TryParse(dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["NBC"].Value.ToString(), out nbc);
                        decimal.TryParse(dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["VPC"].Value.ToString(), out vpc);
                        if (dgvRez.Columns.Contains("MPC"))
                            decimal.TryParse(dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["MPC"].Value.ToString(), out mpc);

                        kolicina = kolicina * (-1);

                        dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["Količina"].Value = kolicina.ToString();
                        dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["NBC"].Value = (nbc * (dokumenat == "racun" ? 1 : 1)).ToString();
                        dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["VPC"].Value = (vpc * (dokumenat == "racun" ? 1 : 1)).ToString();
                        if (dgvRez.Columns.Contains("MPC"))
                            dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["MPC"].Value = (mpc * (dokumenat == "racun" ? 1 : 1)).ToString();

                        this.DialogResult = DialogResult.Yes;
                    }
                }
                else
                {
                    MessageBox.Show("Nema ni jedne stavke za odabrati.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvRez_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvRez.Rows.Count == 0)
                    return;

                txtKolicina.Text = dgvRez.Rows[dgvRez.CurrentCell.RowIndex].Cells["Količina"].Value.ToString();
                txtKolicina.SelectAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtKolicina_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //decimal kolicina = 0;
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd.PerformClick();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSrch.PerformClick();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}