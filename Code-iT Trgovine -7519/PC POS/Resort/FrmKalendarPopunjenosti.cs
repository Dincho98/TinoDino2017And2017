using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS.Resort
{
    public partial class FrmKalendarPopunjenosti : Form
    {
        public frmMenu MainMenu;

        private int SelectedMonth { get; set; }

        public FrmKalendarPopunjenosti()
        {
            InitializeComponent();
        }

        private void FrmKalendarPopunjenosti_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(FrmKalendarPopunjenosti_Paint); // Sets background gradient
            SelectedMonth = DateTime.Now.Month;
            NumericYearOnLoad();
            SetMonthCB();
            LoadMonthData(SelectedMonth, Convert.ToInt32(numYear.Value));
        }

        private void FrmKalendarPopunjenosti_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        public void LoadMonthData(int month, int year)
        {
            ClearGrid();

            // Set columns (days) for selected months
            int days = DateTime.DaysInMonth(year, month);
            if (days > 0)
            {
                dataGridView.Columns.Add("naziv_sobe", "Naziv sobe");
                dataGridView.Columns[0].Width = 150;
                for (int i = 1; i <= days; i++)
                {
                    dataGridView.Columns.Add(i.ToString(), i.ToString());
                    dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            // Add rooms
            DataTable DTsobe = Global.Database.GetSobe();
            if (DTsobe?.Rows.Count > 0)
            {
                foreach (DataRow row in DTsobe.Rows)
                {
                    int index = dataGridView.Rows.Add();
                    dataGridView.Rows[index].Cells["naziv_sobe"].Value = row["naziv_sobe"];

                    // Guest reservations
                    DataTable DTrezervacije = Global.Database.GetRezervacije(null, row["broj_sobe"].ToString());
                    if (DTrezervacije.Rows.Count > 0)
                    {
                        foreach (DataRow reservationRow in DTrezervacije.Rows)
                        {
                            DateTime datumDolaska = DateTime.Parse(reservationRow["datum_dolaska"].ToString());
                            DateTime datumOdlaska = DateTime.Parse(reservationRow["datum_odlaska"].ToString());

                            if (datumDolaska.Month == SelectedMonth || datumOdlaska.Month == SelectedMonth)
                            {
                                int daysDifference = (datumOdlaska - datumDolaska).Days;
                                int startingDay = 1;

                                if (SelectedMonth == datumDolaska.Month)
                                    startingDay = datumDolaska.Day;

                                for (int i = 0; i <= daysDifference; i++)
                                {
                                    if ((startingDay + i > days) || (startingDay == 1 && i == datumOdlaska.Day))
                                        break;
                                    

                                    DataGridViewCell cell = dataGridView.Rows[index].Cells[startingDay + i];

                                    if (string.IsNullOrEmpty(cell.FormattedValue.ToString()))
                                    {
                                        // Set cell color
                                        cell.Style.SelectionForeColor = SystemColors.Highlight;

                                        if (reservationRow["naplaceno"].ToString() == "1")
                                        {
                                            cell.Style.BackColor = Color.LightGreen;
                                            cell.Style.ForeColor = Color.LightGreen;

                                        }
                                        else if (reservationRow["naplaceno"].ToString() == "0" && datumOdlaska >= DateTime.Now)
                                        {
                                            cell.Style.BackColor = Color.CornflowerBlue;
                                            cell.Style.ForeColor = Color.CornflowerBlue;
                                        }
                                        else if (reservationRow["naplaceno"].ToString() == "0" && datumOdlaska < DateTime.Now)
                                        {
                                            cell.Style.BackColor = Color.Salmon;
                                            cell.Style.ForeColor = Color.Salmon;
                                        }

                                        cell.Value = "R" + reservationRow["broj"].ToString();

                                        SetHoverText(index, startingDay, i, reservationRow, cell, cell.Value.ToString());
                                        
                                    }
                                    else
                                    {
                                        cell.Style.BackColor = Color.DarkSlateBlue;
                                        cell.Style.ForeColor = Color.DarkSlateBlue;
                                        cell.Style.SelectionForeColor = SystemColors.Highlight;
                                        cell.Value = cell.Value + "R" + reservationRow["broj"].ToString();

                                        SetHoverText(index, startingDay, i, reservationRow, cell, cell.Value.ToString());
                                        continue;
                                    }
                                    

                                }
                            }
                        }
                    }
                }
            }
        }

        //Vraca broj rezervacija koje se nalaze u odabranoj celiji
        //Npr R1R4, vraca 2
        //Npr R1, vraca 1
        private int BrojRezervacijaUOdredjenojCeliji(string cellValue)
        {
            cellValue = cellValue.Remove(0, 1);
            string[] array = cellValue.Split('R');
            return array.Length;
        }

        private void SetHoverText(int index, int startingDay, int i, DataRow reservationRow, DataGridViewCell cell, string cellValue)
        {
            int duljina = BrojRezervacijaUOdredjenojCeliji(dataGridView.Rows[index].Cells[startingDay + i].FormattedValue.ToString());

            //Uzimamo vrijednost koja piše u čeliji
            if (duljina <= 1)
            {
                DataTable dt = Global.Database.GetPartners(reservationRow["id_partner"].ToString());
                if (dt?.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    cell.ToolTipText = dataRow["ime_tvrtke"].ToString();
                }
                else
                {
                    cell.ToolTipText = "Nema partnera.";
                }
            }
            else
            {
                //Potreban za pronalazak partnera na odlasku i partnera za dolazak
                cellValue = cellValue.Remove(0, 1);
                string[] array = cellValue.Split('R');

                //Partner 1 ID
                DataTable dt = Global.Database.GetIdPartnera(array[0]);
                string idPartneraNaOdlasku = dt.Rows[0]["id_partner"].ToString();
                //Lik koji odlazi
                string naOdlasku = "";
                dt = Global.Database.GetPartners(idPartneraNaOdlasku);
                if (dt?.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    naOdlasku = dataRow["ime_tvrtke"].ToString();
                }

                //Partner 2 ID
                dt = Global.Database.GetIdPartnera(array[1]);
                string idPartneraNaDolasku = dt.Rows[0]["id_partner"].ToString();
                //Lik koji dolazi
                string naDolasku = "";
                dt = Global.Database.GetPartners(idPartneraNaDolasku);
                if (dt?.Rows.Count > 0)
                {
                    DataRow dataRow = dt.Rows[0];
                    naDolasku = dataRow["ime_tvrtke"].ToString();
                }

                //Usluge ID
                //Imam u array[0] ID
                //U unos rezervacije pronađem id_vrsta_usluge
                //Iz tablice vrsta_usluge uzimam ime usluge i to zapisujem također u ToolTipText
                dt = Global.Database.GetIdVrstaUsluge(array[0]);
                string idVrsteUsluge = dt.Rows[0]["id_vrsta_usluge"].ToString();
                //Ime usluge
                //MessageBox.Show(idVrsteUsluge);
                dt = Global.Database.GetVrstaUsluge(idVrsteUsluge);
                string vrstaUsluge = dt.Rows[0]["naziv_usluge"].ToString();
                
                cell.ToolTipText = $"Odlazi: {naOdlasku}\nDolazi: {naDolasku}\nUsluga: {vrstaUsluge}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMonthCB()
        {
            DataTable DTmonths = new DataTable();
            DTmonths.Columns.Add("number", typeof(int));
            DTmonths.Columns.Add("name", typeof(string));

            DTmonths.Rows.Add(1, "Siječanj");
            DTmonths.Rows.Add(2, "Veljača");
            DTmonths.Rows.Add(3, "Ožujak");
            DTmonths.Rows.Add(4, "Travanj");
            DTmonths.Rows.Add(5, "Svibanj");
            DTmonths.Rows.Add(6, "Lipanj");
            DTmonths.Rows.Add(7, "Srpanj");
            DTmonths.Rows.Add(8, "Kolovoz");
            DTmonths.Rows.Add(9, "Rujan");
            DTmonths.Rows.Add(10, "Listopad");
            DTmonths.Rows.Add(11, "Studeni");
            DTmonths.Rows.Add(12, "Prosinac");

            cbMonth.DataSource = DTmonths;
            cbMonth.ValueMember = "number";
            cbMonth.DisplayMember = "name";

            cbMonth.SelectedValue = DateTime.Now.Month;
        }

        /// <summary>
        ///  
        /// </summary>
        private void NumericYearOnLoad()
        {
            numYear.Minimum = DateTime.Now.Year - 30;
            numYear.Maximum = DateTime.Now.Year + 30;
            numYear.Value = DateTime.Now.Year;
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearGrid()
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();
            dataGridView.Refresh();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= 0 || e.RowIndex < 0)
                return;

            string cellValue = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (string.IsNullOrWhiteSpace(cellValue))
                return;

            cellValue = cellValue.Remove(0, 1);
            string[] array = cellValue.Split('R');

            FrmOpcijeRezervacije form = new FrmOpcijeRezervacije
            {
                BrojRezervacije = Convert.ToInt32(array[0])
            };
            form.ShowDialog();
        }

        private void BtnPreviousMonth_Click(object sender, EventArgs e)
        {
            if (SelectedMonth > 1)
            {
                SelectedMonth--;
                cbMonth.SelectedValue = SelectedMonth;
                LoadMonthData(SelectedMonth, Convert.ToInt32(numYear.Value));
            }
        }

        private void BtnNextMonth_Click(object sender, EventArgs e)
        {
            if (SelectedMonth < 12)
            {
                SelectedMonth++;
                cbMonth.SelectedValue = SelectedMonth;
                LoadMonthData(SelectedMonth, Convert.ToInt32(numYear.Value));
            }
        }

        private void CbMonth_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SelectedMonth = (int)cbMonth.SelectedValue;
            LoadMonthData(SelectedMonth, Convert.ToInt32(numYear.Value));
        }

        private void BtnReservation_Click(object sender, EventArgs e)
        {
            FrmRezervacija form = new FrmRezervacija();
            form.ShowDialog();
            if (form.ReservationCreated)
                LoadMonthData(SelectedMonth, Convert.ToInt32(numYear.Value));
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
