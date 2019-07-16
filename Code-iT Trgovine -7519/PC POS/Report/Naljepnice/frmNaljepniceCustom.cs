using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Naljepnice
{
    public partial class frmNaljepniceCustom : Form
    {
        public frmNaljepniceCustom()
        {
            InitializeComponent();
        }

        public bool vpcBool;
        public bool mpcBool;

        public int pocetak { get; set; }
        public DataTable DT { get; set; }

        private void frmNaljepnice_Load(object sender, EventArgs e)
        {
            SetNaljepnice(pocetak, DT);
            this.reportViewer1.RefreshReport();
        }

        private void SetNaljepnice(int pocetak, DataTable DT)
        {
            string sifra = "";
            string naziv = "";
            DataRow DTrow = dSnaljepnice.Tables[0].NewRow();

            int j = 1;
            int all = 1;
            for (int i = 0; i < DT.Rows.Count + pocetak; i++)
            {
                if (j == 1) { DTrow = dSnaljepnice.Tables[0].NewRow(); }

                if (pocetak > i)
                {
                    DTrow["polje" + j] = "";
                }
                else
                {
                    sifra = DT.Rows[i - pocetak]["sifra"].ToString();
                    naziv = DT.Rows[i - pocetak]["naziv"].ToString();

                    if (sifra.Length > 22) { sifra = sifra.Remove(22); }
                    if (naziv.Length > 22) { naziv = naziv.Remove(22); }

                    if (vpcBool && mpcBool)
                    {
                        DTrow["polje" + j] = sifra + "\r\n" + naziv + "\r\n" + DT.Rows[i - pocetak]["jmj"].ToString() + "|" +
                            DT.Rows[i - pocetak]["broj"].ToString() + "/" + DT.Rows[i - pocetak]["godina"].ToString() +
                            "   MPC: " + DT.Rows[i - pocetak]["mpc"].ToString() + " kn" +
                            "   VPC: " + DT.Rows[i - pocetak]["vpc"].ToString() + " kn";
                    }
                    else if (mpcBool)
                    {
                        DTrow["polje" + j] = sifra + "\r\n" + naziv + "\r\n" + DT.Rows[i - pocetak]["jmj"].ToString() + "|" +
                            DT.Rows[i - pocetak]["broj"].ToString() + "/" + DT.Rows[i - pocetak]["godina"].ToString() +
                            "   MPC: " + DT.Rows[i - pocetak]["mpc"].ToString() + " kn";
                    }
                    else
                    {
                        DTrow["polje" + j] = sifra + "\r\n" + naziv + "\r\n" + DT.Rows[i - pocetak]["jmj"].ToString() + "|" +
                            DT.Rows[i - pocetak]["broj"].ToString() + "/" + DT.Rows[i - pocetak]["godina"].ToString() +
                            "   VPC: " + DT.Rows[i - pocetak]["vpc"].ToString() + " kn";
                    }
                }

                if (all == DT.Rows.Count + pocetak)
                {
                    if (j < 4)
                    {
                        for (int br = j; br < (4 - j); br++)
                        {
                            DTrow["polje" + (br + 1)] = "";
                        }
                    }

                    dSnaljepnice.Tables[0].Rows.Add(DTrow);
                }
                else if (j == 4) { j = 0; dSnaljepnice.Tables[0].Rows.Add(DTrow); }

                j++;
                all++;
            }
        }
    }
}