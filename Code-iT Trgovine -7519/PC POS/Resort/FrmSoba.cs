using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    //public partial class FrmSoba : Form
    public partial class FrmSoba:Form
    {
        public int BrojSobe = 0;
        public bool EditMode = false;
        public bool Successful { get; set; }

        public FrmSoba()
        {
            InitializeComponent();
        }

        private void FrmSoba_Load(object sender, EventArgs e)
        {
            FillTipSobeCB();
            if (!EditMode)
                tbBroj.Text = Global.Database.GetMaxBroj("sobe", "broj_sobe");
            else
                LoadData();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillTipSobeCB()
        {
            DataTable DTtipSobe = Global.Database.GetTipSobe();
            if (DTtipSobe?.Rows.Count > 0)
            {
                cbTipSobe.ValueMember = "id";
                cbTipSobe.DisplayMember = "tip";
                cbTipSobe.DataSource = DTtipSobe;
            }
            else
            {
                MessageBox.Show("Trenutno ne postoji niti jedan tip sobe. Molimo da ih kreirate.", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadData()
        {
            DataTable DTsoba = Global.Database.GetSobe(BrojSobe.ToString());
            if (DTsoba?.Rows.Count > 0)
            {
                DataRow row = DTsoba.Rows[0];
                tbBroj.Text = row["broj_sobe"].ToString();
                tbNazivSobe.Text = row["naziv_sobe"].ToString();
                cbTipSobe.SelectedValue = row["id_tip_sobe"].ToString();
                numBrojLezaja.Value = Convert.ToInt32(row["broj_lezaja"].ToString());
                tbCijenaNocenja.Text = row["cijena_nocenja"].ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddRoom()
        {
            try
            {
                string query = $@"INSERT INTO sobe (broj_sobe, id_tip_sobe, naziv_sobe, broj_lezaja, cijena_nocenja, aktivnost, napomena) 
                                VALUES ({tbBroj.Text}, {cbTipSobe.SelectedValue.ToString()}, '{tbNazivSobe.Text}', {numBrojLezaja.Value}, {tbCijenaNocenja.Text}, 1, '')";
                classSQL.insert(query);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Successful = true;
                //this.Hide();
                Close();
                //Soba(false);
            }
            catch (Exception ex)
            {
                Successful = false;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateRoom()
        {
            try
            {
                string query = $@"UPDATE sobe SET id_tip_sobe = {cbTipSobe.SelectedValue.ToString()}
                                    , naziv_sobe = '{tbNazivSobe.Text}'
                                    , broj_lezaja = {numBrojLezaja.Value}
                                    , cijena_nocenja = {tbCijenaNocenja.Text}
                                WHERE broj_sobe = {BrojSobe}";
                classSQL.insert(query);
                MessageBox.Show("Spremljeno.", "Obavijest", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Successful = true;
                Close();
            }
            catch (Exception ex)
            {
                Successful = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSpremi_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbNazivSobe.Text))
            {
                MessageBox.Show("Naziv sobe je obavezno polje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(decimal.TryParse(tbCijenaNocenja.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal cijena))
            {
                if(cijena < 0)
                {
                    MessageBox.Show("Cijena noćenja mora biti veća od 0.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Neispravan unos cijene!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirmDialog = MessageBox.Show("Jeste li sigurni da želite spremiti?", "Spremi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmDialog == DialogResult.Yes)
            {
                if (!EditMode)
                    AddRoom();
                else
                    UpdateRoom();
            }
        }
    }
}
