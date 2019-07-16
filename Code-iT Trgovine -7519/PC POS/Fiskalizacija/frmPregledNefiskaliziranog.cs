using System;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Fiskalizacija
{
    public partial class frmPregledNefiskaliziranog : Form
    {
        public frmPregledNefiskaliziranog()
        {
            InitializeComponent();
        }

        public string brojRacuna { get; set; }
        public string poslano { get; set; }
        public string greska { get; set; }
        public string ducan { get; set; }
        public string blagajna { get; set; }
        public string datum { get; set; }

        private void frmPregledNefiskaliziranog_Load(object sender, EventArgs e)
        {
            txtBrojRacuna.Text = brojRacuna;
            txtDucan.Text = ducan;
            txtBlagajna.Text = blagajna;
            txtPoslano.Text = poslano;
            txtGreška.Text = greska;
            txtDatum.Text = datum;

            int x, y;
            if (greska.IndexOf("System") == 0) txtPoruka.Text = "Nije ostvarena veza sa poreznom upravom";
            else
            {
                x = greska.IndexOf("<tns:PorukaGreske>") + 18;
                y = greska.IndexOf("</tns:PorukaGreske>");
                txtPoruka.Text = new string(greska.ToArray(), x, y - x);
            }

            x = poslano.IndexOf("<tns:Oib>") + 9;
            y = poslano.IndexOf("</tns:Oib>");
            txtOib.Text = new string(poslano.ToArray(), x, y - x);
            poslano = poslano.Remove(x, y - x);

            x = poslano.IndexOf("<tns:OibOper>") + 13;
            y = poslano.IndexOf("</tns:OibOper>");
            txtOibOper.Text = new string(poslano.ToArray(), x, y - x);
            poslano = poslano.Remove(x, y - x);

            x = poslano.IndexOf("<tns:NakDost>") + 13;
            y = poslano.IndexOf("</tns:NakDost>");
            string bul = new string(poslano.ToArray(), x, y - x);
            if (bul == "true") chBxNaknada.Checked = true;
            poslano = poslano.Remove(x, y - x);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x;
            //string sql = "UPDATE neuspjela_fiskalizacija SET " +
            //    " xml='" + txtPoslano.Text + "'," +
            //    " greska='" + txtGreška.Text + "' WHERE broj_racuna='" + brojRacuna + "' AND id_ducan='" + txtDucan.Text + "' AND id_kasa='" + txtBlagajna.Text + "'";
            x = poslano.IndexOf("<tns:Oib>") + 9;
            poslano = poslano.Insert(x, txtOib.Text);

            x = poslano.IndexOf("<tns:OibOper>") + 13;
            poslano = poslano.Insert(x, txtOibOper.Text);

            x = poslano.IndexOf("<tns:NakDost>") + 13;
            if (chBxNaknada.Checked) poslano = poslano.Insert(x, "true");
            else poslano = poslano.Insert(x, "false");

            string sql = "UPDATE neuspjela_fiskalizacija SET " +
                " xml='" + poslano + "'," +
                " greska='" + txtGreška.Text + "' WHERE broj_racuna='" + brojRacuna + "'";
            classSQL.update(sql);

            MessageBox.Show("Spremljeno.");

            brojRacuna = txtBrojRacuna.Text;
            ducan = txtDucan.Text;
            blagajna = txtBlagajna.Text;
            //poslano = txtPoslano.Text;
            greska = txtGreška.Text;
            datum = txtDatum.Text;
            this.Close();
        }
    }
}