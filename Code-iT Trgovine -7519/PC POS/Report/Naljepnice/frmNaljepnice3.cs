using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Report.Naljepnice
{
    public partial class frmNaljepnice3 : Form
    {
        public frmNaljepnice3()
        {
            InitializeComponent();
        }

        public bool vpcBool;
        public bool mpcBool;
        public bool rucno { get; set; }

        public int pocetak { get; set; }
        public DataTable DT { get; set; }
        private Random rnd1 = new Random();

        private void frmNaljepnice3_Load(object sender, EventArgs e)
        {
            SetNaljepnice(pocetak, DT);
            this.reportViewer1.RefreshReport();
        }

        private void SetNaljepnice(int pocetak, DataTable DT)
        {
            string sifra = "";
            string naziv = "";
            string jmj = "";
            string barcode = "";
            string jmjSkraceno = "";
            string broj = "";
            string godina = "";
            string mpc = "";
            string vpc = "";
            string kolicina = "0";
            double mpcD = 0;
            double vpcD = 0;
            double cijenaKgMPC;
            double cijenaKgVPC;
            bool t;
            DataRow DTrow = dSnaljepnice.Tables[0].NewRow();

            int j = 1;
            int all = 1;
            for (int i = 0; i < DT.Rows.Count + pocetak; i++)
            {
                if (j == 1) { DTrow = dSnaljepnice.Tables[0].NewRow(); }

                if (pocetak > i)
                {
                    DTrow["polje" + j] = "";
                    DTrow["poljex" + j] = "";
                }
                else
                {
                    kolicina = DT.Rows[i - pocetak]["kolicina"].ToString();

                    if (kolicina != "0")
                    {
                        sifra = DT.Rows[i - pocetak]["sifra"].ToString();
                        naziv = DT.Rows[i - pocetak]["naziv"].ToString();
                        broj = DT.Rows[i - pocetak]["broj"].ToString();
                        jmj = DT.Rows[i - pocetak]["jmj"].ToString().ToLower();
                        barcode = DT.Rows[i - pocetak]["barcode"].ToString().ToLower();
                        //jmj = "g";

                        godina = DT.Rows[i - pocetak]["godina"].ToString();
                        mpc = DT.Rows[i - pocetak]["mpc"].ToString();
                        vpc = DT.Rows[i - pocetak]["vpc"].ToString();
                        mpcD = Convert.ToDouble(mpc);
                        vpcD = Convert.ToDouble(vpc);
                        //if (sifra.Length > 22) { sifra = sifra.Remove(22); }
                        //if (naziv.Length > 22) { naziv = naziv.Remove(22); }

                        //DTrow["polje" + j] = sifra + "\r\n" + naziv + "\r\n" + DT.Rows[i - pocetak]["jmj"].ToString()+ "|" +
                        //    DT.Rows[i - pocetak]["broj"].ToString() + "/" + DT.Rows[i - pocetak]["godina"].ToString() +
                        //    "   MPC: " + DT.Rows[i - pocetak]["mpc"].ToString() + " kn";

                        t = false;
                        if (jmj == "g" || jmj == "gram")
                        {
                            cijenaKgMPC = 1000 * mpcD;
                            cijenaKgVPC = 1000 * vpcD;
                            jmjSkraceno = "g";
                            t = true;
                        }
                        else if (jmj == "dg" || jmj == "dekagram" || jmj == "dag" || jmj.Contains("deka"))
                        {
                            cijenaKgMPC = 100 * mpcD;
                            cijenaKgVPC = 100 * vpcD;
                            jmjSkraceno = "dg";
                            t = true;
                        }
                        else if (jmj == "kg" || jmj == "kilogram" || jmj == "kila" || jmj.Contains("kilo"))
                        {
                            cijenaKgMPC = 1 * mpcD;
                            cijenaKgVPC = 1 * vpcD;
                            jmjSkraceno = "kg";
                            t = true;
                        }
                        else if (jmj == "hg" || jmj == "hektogram" || jmj.Contains("hekto"))
                        {
                            cijenaKgMPC = 10 * mpcD;
                            cijenaKgVPC = 10 * vpcD;
                            jmjSkraceno = "hg";
                            t = true;
                        }
                        else if (jmj == "t" || jmj == "tona" || jmj.Contains("tona"))
                        {
                            cijenaKgMPC = .001 * mpcD;
                            cijenaKgVPC = .001 * vpcD;
                            jmjSkraceno = "t";
                            t = true;
                        }
                        else if (jmj == "kom" || jmj.Contains("kom"))
                        {
                            cijenaKgMPC = mpcD;
                            cijenaKgVPC = vpcD;
                            jmjSkraceno = "kom";
                            t = true;
                        }
                        else
                        {
                            cijenaKgMPC = 1 * mpcD;
                            cijenaKgVPC = 1 * vpcD;
                        }

                        //string barcode = rnd1.Next(100000000, 999999999).ToString();
                        if (rucno)
                        {
                            jmj = t ? "Kol: " + "1" + " " + jmjSkraceno : "";
                            //jmj = t ? "Kol: " + "1" + " " + jmjSkraceno + Environment.NewLine : "";
                        }
                        else
                        {
                            jmj = t ? "Kol: " + kolicina + " " + jmjSkraceno : "";
                            //jmj = t ? "Kol: " + kolicina + " " + jmjSkraceno + Environment.NewLine : "";
                        }

                        jmj += ", " + DT.Rows[i - pocetak]["dokument"].ToString() + "|" + DT.Rows[i - pocetak]["broj"].ToString() + "/" + DT.Rows[i - pocetak]["godina"].ToString() + Environment.NewLine;

                        if (vpcBool && mpcBool)
                        {
                            DTrow["polje" + j] = vratiNaljepnicuOboje(sifra, naziv, barcode,
                                cijenaKgMPC.ToString("#0.00"), cijenaKgVPC.ToString("#0.00"), jmj, jmjSkraceno);
                            DTrow["poljex" + j] = vratiNaljepnicuOboje(mpc, vpc);
                        }
                        else if (mpcBool)
                        {
                            DTrow["polje" + j] = vratiNaljepnicu(sifra, naziv, barcode, cijenaKgMPC.ToString("#0.00"), jmj, jmjSkraceno);
                            DTrow["poljex" + j] = vratiNaljepnicu(mpc, true);
                        }
                        else
                        {
                            DTrow["polje" + j] = vratiNaljepnicu(sifra, naziv, barcode, cijenaKgVPC.ToString("#0.00"), jmj, jmjSkraceno);
                            DTrow["poljex" + j] = vratiNaljepnicu(vpc, false);
                        }
                    }
                }

                if (all == DT.Rows.Count + pocetak)
                {
                    if (j < 3)
                    {
                        for (int br = j; br < (3 - j); br++)
                        {
                            DTrow["polje" + (br + 1)] = "";
                            DTrow["poljex" + (br + 1)] = "";
                        }
                    }

                    dSnaljepnice.Tables[0].Rows.Add(DTrow);
                }
                else if (j == 3) { j = 0; dSnaljepnice.Tables[0].Rows.Add(DTrow); }

                j++;
                all++;
            }
        }

        private string vratiNaljepnicu(string sifra, string naziv, string barcode, string cijenaKg, string jmj, string jmj_)
        {
            if (jmj_.ToString().ToLower() != "kom")
            {
                jmj_ = "kg";
            }
            string str = "";

            string levo = Truncate(naziv + ", Šifra: " + sifra, 50) + Environment.NewLine;

            str = levo;

            str += jmj;

            str += cijenaKg != "0,00" ? jmj_ + "=" + cijenaKg + " kn" + Environment.NewLine : "";

            str += "Barcode: " + barcode;

            return str;
        }

        private string vratiNaljepnicu(string mpc, bool mpcBool)
        {
            string str = "";

            string levo = mpcBool ? "MPC: " : "VPC: ";
            string desno = mpc.PadLeft(1);

            str = levo + desno + " kn";

            return str;
        }

        private string vratiNaljepnicuOboje(string sifra, string naziv, string barcode, string cijenaKgMpc, string cijenaKgVpc, string jmj, string jmj_)
        {
            if (jmj_.ToString().ToLower() != "kom")
            {
                jmj_ = "kg";
            }

            string str = "";

            string levo = Truncate(naziv + ", Šifra: " + sifra, 50) + Environment.NewLine;

            str = levo;

            str += jmj;

            str += cijenaKgMpc != "0,00" ? jmj_ + "=" + cijenaKgMpc + " kn MPC" + Environment.NewLine : "";

            str += cijenaKgVpc != "0,00" ? jmj_ + "=" + cijenaKgVpc + " kn VPC" + Environment.NewLine : "";

            str += "Barcode: " + barcode;

            return str;
        }

        private string vratiNaljepnicuOboje(string mpc, string vpc)
        {
            string str = "";

            string levo = "MPC: ";
            string desno = mpc.PadLeft(1);
            str = levo + desno + " kn" + Environment.NewLine;

            levo = "VPC: ";
            desno = vpc.PadLeft(1);
            str += levo + desno + " kn";

            return str;
        }

        private string Truncate(string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}