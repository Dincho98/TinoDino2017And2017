using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class Tecajna_lista : Form
    {
        public Tecajna_lista()
        {
            InitializeComponent();
        }

        private void tecaj_provjera()
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                WebRequest reguest = WebRequest.Create("http://www.pbz.hr/Downloads/HNBteclist.xml");

                DataSet DSpodaci = new DataSet();
                WebResponse response = reguest.GetResponse();
                Stream stream = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream);
                try
                {
                    // string a = sr.ReadToEnd();
                    DSpodaci.ReadXml(sr);
                    sr.Close();
                    stream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dgv1.DataSource = DSpodaci.Tables[1];
                    dgv1.Columns["Name"].HeaderText = "Ime";
                    dgv1.Columns["Unit"].HeaderText = "Jedinica";
                    dgv1.Columns["BuyRateForeign"].HeaderText = "Kupovni";
                    dgv1.Columns["MeanRate"].HeaderText = "Srednji";
                    dgv1.Columns["SellRateForeign"].HeaderText = "Prodajni";
                    dgv1.Columns["Code"].HeaderText = "Kod";
                    //if(DSpodaci.Tables[1].Columns.)
                    //dgv1.Columns["BuyRateCache"].Visible = false;
                    //dgv1.Columns["SellRateCache"].Visible = false;
                }
            }
        }

        private void Tecajna_lista_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.Date.ToString();
            tecaj_provjera();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgv1.Rows.Count; i++)
            {
                string sifra = dgv1.Rows[i].Cells["Code"].FormattedValue.ToString();
                string srednji_tecaj = dgv1.Rows[i].Cells["MeanRate"].FormattedValue.ToString();

                string sql_azuriraj_val = "Update valute Set tecaj = '" + srednji_tecaj + "' WHERE sifra = '" + sifra + "'";
                classSQL.update(sql_azuriraj_val);
            }

            MessageBox.Show("Tečajevi ažurirani prema HNB srednjem tečaju za današnji dan !");
        }

        private void Tecajna_lista_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }
    }
}