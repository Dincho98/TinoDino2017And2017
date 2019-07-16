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
    public partial class FrmOpcijeRezervacije : Form
    {
        public frmMenu MainMenu;
        public int BrojRezervacije { get; set; }

        private DataTable DTrezervacija { get; set; }

        public FrmOpcijeRezervacije()
        {
            InitializeComponent();
        }

        private void FrmOpcijeRezervacije_Load(object sender, EventArgs e)
        {
            LoadRezervacija();
        }

        /// <summary>
        /// Loads reservation data
        /// </summary>
        private void LoadRezervacija()
        {
            DTrezervacija = Global.Database.GetRezervacije(BrojRezervacije.ToString());
            if (DTrezervacija?.Rows.Count > 0)
            {
                DataRow row = DTrezervacija.Rows[0];
                lblBroj.Text = BrojRezervacije.ToString();
                lblPartnerNaziv.Text = Global.Database.GetPartners(row["id_partner"].ToString())?.Rows[0]["ime_tvrtke"].ToString();
                lblDatumDolaska.Text = DateTime.Parse(row["datum_dolaska"].ToString()).ToString("dd.MM.yyyy. HH:mm");
                lblDatumOdlaska.Text = DateTime.Parse(row["datum_odlaska"].ToString()).ToString("dd.MM.yyyy. HH:mm");
            }
        }

        /// <summary>
        /// Opens reservation form and enabled edit mode
        /// </summary>
        private void EditReservation()
        {
            Hide();
            FrmRezervacija form = new FrmRezervacija
            {
                BrojRezervacije = BrojRezervacije,
                EditMode = true
            };
            form.FormClosed += (s, args) => Close();
            form.ShowDialog();
        }
        
        /// <summary>
        /// Used to open frmFaktura form and send reservation data to it
        /// </summary>
        private void Faktura()
        {
            frmFaktura form = new frmFaktura
            {
                DTrezervacija = DTrezervacija,
                MainForm = MainMenu,
                Dock = DockStyle.Fill,
                WindowState = FormWindowState.Maximized
            };
            Hide();
            form.FormClosed += (s, args) => Close();
            form.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Maloprodaja()
        {
            frmKasa form = new frmKasa
            {
                DTrezervacija = DTrezervacija,
                Dock = DockStyle.Fill,
                WindowState = FormWindowState.Maximized
            };
            Hide();
            form.FormClosed += (s, args) => Close();
            form.ShowDialog();
        }

        private void BtnUrediRezervaciju_Click(object sender, EventArgs e)
        {
            EditReservation();
        }

        private void BtnFaktura_Click(object sender, EventArgs e)
        {
            Faktura();
        }

        private void BtnMaloprodaja_Click(object sender, EventArgs e)
        {
            Maloprodaja();
        }
    }
}
