using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public partial class frmPromjenaGodine : Form
    {
        public frmPromjenaGodine()
        {
            InitializeComponent();
        }

        private PCPOS.Util.classFukcijeZaUpravljanjeBazom B = new classFukcijeZaUpravljanjeBazom("MALOPRODAJA", "POS");

        private void frmPotvrdaZaNovuGodinu_Load(object sender, EventArgs e)
        {
            List<string> L = B.UzmiSveBazeIzPostgressa();
            DataTable DT = new DataTable();
            if (DT.Columns.Count == 0)
            {
                DT.Columns.Add("id");
                DT.Columns.Add("name", typeof(int));
            }

            foreach (string db in L)
            {
                if (db != "postgres" && (db.StartsWith(Util.Korisno.prefixBazeKojaSeKoristi())))
                {
                    string baza = db;
                    baza = db.Remove(0, Util.Korisno.prefixBazeKojaSeKoristi().Length);
                    //baza = baza.Replace("DB", "");
                    //baza = baza.Replace("POS", "");
                    //baza = baza.Replace("db", "");
                    //baza = baza.Replace("pos", "");
                    DT.Rows.Add(db, baza);
                }
            }

            DataView dv = new DataView(DT);
            dv.Sort = "name asc";
            DT = dv.ToTable();

            cbGodina.DataSource = DT;
            cbGodina.ValueMember = "id";
            cbGodina.DisplayMember = "name";

            try
            {
                cbGodina.SelectedValue = B.UzmiBazuKojaSeKoristi();
            }
            catch
            { }
        }

        private void btnPromjenaGodine_Click(object sender, EventArgs e)
        {
            B.PostaviGodinu_U_XML(cbGodina.SelectedValue.ToString());
        }
    }
}