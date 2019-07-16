using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmSpranci : Form
    {
        public frmSpranci()
        {
            InitializeComponent();
        }

        private void frmSpranci_Load(object sender, EventArgs e)
        {
            KreirajTablicuAkoNePostoji();
            PopuniKucice();
        }

        private void txtLogo_TextChanged(object sender, EventArgs e)
        {
            DodajSlikuNaPictureBox(pbZaLogo, txtLogo.Text);
        }

        private void txtSlika_TextChanged(object sender, EventArgs e)
        {
            DodajSlikuNaPictureBox(pbGlavnaSlika, txtSlika.Text);
        }

        private void btnOtvoriLink_Click(object sender, EventArgs e)
        {
            UcitajSliku(txtLogo);
        }

        private void btnOtvoriSliku_Click(object sender, EventArgs e)
        {
            UcitajSliku(txtSlika);
        }

        private void btnPrikazi_Click(object sender, EventArgs e)
        {
            UpdateDB();

            Report.Spranci.frmSpranciReport s = new Report.Spranci.frmSpranciReport();
            s.logo = ProvjeriDaliJeWebIliFile(txtLogo.Text);
            s.slika = ProvjeriDaliJeWebIliFile(txtSlika.Text);
            s.poslovnica = rtbPoslovnica.Text;
            s.sifra = "ŠIFRA: " + txtSifra.Text;
            s.opis = "OPIS: \r\n" + rtbOpis.Text;
            s.gotovina = txtCijenaGotovina.Text + "Kn";
            s.naziv = txtNaziv.Text;
            if (txtCijenaKartice.Text == txtCijenaGotovina.Text)
                s.kartice = "";
            else
                s.kartice = "KARTICE CIJENA MPC: " + txtCijenaKartice.Text + " Kn";

            s.ShowDialog();
        }

        #region *****************************POPRATNE FUNKCIJE**************************************

        private void PopuniKucice()
        {
            DataTable DT = classSQL.select("SELECT * FROM spranci", "spranci").Tables[0];
            txtLogo.Text = DT.Rows[0]["slika_logo"].ToString();
            txtSlika.Text = DT.Rows[0]["slika_glavna"].ToString();
            txtSifra.Text = DT.Rows[0]["sifra"].ToString();
            txtCijenaGotovina.Text = DT.Rows[0]["cijena_gotovina"].ToString();
            txtCijenaKartice.Text = DT.Rows[0]["cijena_kartice"].ToString();
            rtbOpis.Text = DT.Rows[0]["opis"].ToString();
            rtbPoslovnica.Text = DT.Rows[0]["tvrtka"].ToString();
            txtNaziv.Text = DT.Rows[0]["naziv"].ToString();
        }

        private void UpdateDB()
        {
            classSQL.update("UPDATE spranci SET " +
                " slika_logo='" + txtLogo.Text + "'," +
                " slika_glavna='" + txtSlika.Text + "'," +
                " sifra='" + txtSifra.Text + "'," +
                " cijena_gotovina='" + txtCijenaGotovina.Text + "'," +
                " cijena_kartice='" + txtCijenaKartice.Text + "'," +
                " opis='" + rtbOpis.Text + "'," +
                " naziv='" + txtNaziv.Text + "'," +
                " tvrtka='" + rtbPoslovnica.Text + "'" +
                "");
        }

        private void DodajSlikuNaPictureBox(PictureBox pb, string path)
        {
            if (path.Length > 6)
            {
                pb.ImageLocation = path;
            }
        }

        private void UcitajSliku(TextBox txt)
        {
            OpenFileDialog saveFileDialog1 = new OpenFileDialog();
            saveFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            saveFileDialog1.FilterIndex = 1;
            string file = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = saveFileDialog1.FileName;
                txt.Text = file;
            }
        }

        private string ProvjeriDaliJeWebIliFile(string path)
        {
            if (path.Length > 7)
            {
                if (path.Remove(4) == "http")
                {
                    return path;
                }
                else
                {
                    if (File.Exists(path))
                    {
                        return "file://" + path;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }

        private void KreirajTablicuAkoNePostoji()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW' AND table_name='spranci'", "Table").Tables[0];

            if (DTremote.Rows.Count == 0)
            {
                string sql = "CREATE TABLE spranci (" +
                    " id serial," +
                    " slika_logo character varying," +
                    " slika_glavna character varying," +
                    " tvrtka character varying," +
                    " opis character varying," +
                    " cijena_gotovina character varying," +
                    " cijena_kartice character varying," +
                    " naziv character varying," +
                    " sifra character varying," +
                    " CONSTRAINT spranci_id_key PRIMARY KEY (id )" +
                    "); INSERT INTO spranci (tvrtka,opis,cijena_gotovina,cijena_kartice,sifra) VALUES ('Ovdje upišite ime tvrtke','Opis proizvoda','0','0','Šifra proizvoda');";
                classSQL.insert(sql);
            }
        }

        private void txtSifra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                try
                {
                    if (Util.CheckConnection.Check())
                    {
                        //pc1.konektor k = new pc1.konektor();
                        //DataTable DT = k.GetDataset("SELECT * FROM Artikli WHERE ProductID='" + txtSifra.Text + "'", "Artikili", "admin", "q1w2e3r4123").Tables[0];
                        string[] s = new string[4];
                        using (var ws = new wsSoftKontrol.wsSoftKontrol())
                        {
                            s = ws.getArtiklData(txtSifra.Text, null);
                        }

                        if (s != null && s.Count() == 5)
                        {
                            string naziv = s[1];
                            string opis = s[2];
                            if (naziv.Length > 30)
                            {
                                naziv = naziv.Remove(30);
                            }

                            if (opis.Length > 150)
                            {
                                opis = opis.Remove(150);
                            }

                            txtNaziv.Text = naziv;
                            txtCijenaGotovina.Text = Math.Round(Convert.ToDecimal(s[3].ToString().Replace(".", ",")), 3).ToString("#0.00");
                            txtCijenaKartice.Text = Math.Round((Convert.ToDecimal(s[3].ToString().Replace(".", ","))), 3).ToString("#0.00");
                            rtbOpis.Text = opis;
                            //txtSlika.Text = "http://pc1.hr/Img/artikli/" + DT.Rows[0]["Picture"].ToString();
                            txtSlika.Text = s[4];
                        }
                    }
                }
                catch (Exception ex)
                {
                    //NE PRIKAZUJ GREŠKU JER NA KEY DOWN RADI SAMO NAŠ WEB
                }
            }
        }

        #endregion *****************************POPRATNE FUNKCIJE**************************************
    }
}