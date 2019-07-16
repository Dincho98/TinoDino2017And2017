using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    public partial class FrmTipSobe : Form
    {
        public string BrojSobe { get; set; }
        public bool EditMode { get; set; }
        public bool Successful { get; set; }

        public FrmTipSobe()
        {
            InitializeComponent();
        }

        private void FrmTipSobe_Load(object sender, EventArgs e)
        {
            if (EditMode)
                LoadData();
        }

        /// <summary>
        /// Used to load room type data from database
        /// </summary>
        private void LoadData()
        {
            DataTable DTtipSobe = Global.Database.GetTipSobe(BrojSobe);
            if (DTtipSobe?.Rows.Count > 0)
                tbNaziv.Text = DTtipSobe.Rows[0]["tip"].ToString();
        }

        /// <summary>
        /// Used to insert new data into database
        /// </summary>
        private void CreateType()
        {
            try
            {
                string query = $@"INSERT INTO tip_sobe (tip, aktivnost) VALUES ('{tbNaziv.Text}', 1)";
                classSQL.insert(query);
                Successful = true;
                Close();
            }
            catch (Exception ex)
            {
                Successful = false;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Used to update data in database
        /// </summary>
        private void UpdateType()
        {
            try
            {
                string query = $@"UPDATE tip_sobe SET tip = '{tbNaziv.Text}' WHERE id = {BrojSobe}";
                classSQL.insert(query);
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
            if(!string.IsNullOrWhiteSpace(tbNaziv.Text))
            {
                if (!EditMode)
                    CreateType();
                else
                    UpdateType();
            }
            else
                MessageBox.Show("Naziv je obavezno polje!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
