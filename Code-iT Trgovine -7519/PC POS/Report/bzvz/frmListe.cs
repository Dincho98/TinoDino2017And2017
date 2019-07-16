using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS.Report.bzvz
{
    public partial class frmListe : Form
    {
        public frmListe()
        {
            InitializeComponent();
        }

        public string artikl { get; set; }
        public string godina { get; set; }
        public string broj_dokumenta { get; set; }
        public string dokument { get; set; }
        public string ImeForme { get; set; }
        public string blagajnik { get; set; }
        public string skladiste { get; set; }
        public string ducan { get; set; }
        public string partner { get; set; }
        public DateTime datumOD { get; set; }
        public DateTime datumDO { get; set; }

        private void frmListe_Load(object sender, EventArgs e)
        {
            int heigt = SystemInformation.VirtualScreen.Height;
            this.Height = heigt - 60;
            this.Location = new Point((SystemInformation.VirtualScreen.Width / 2) - 411, 5);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            //dokumenat = "PrometPoRobi";
            //broj_dokumenta = "2";
            //skladiste = "2";
            //datumOD = DateTime.Now.AddDays(-2);
            //datumDO = DateTime.Now.AddDays(+10);
            staracki_izvjestaj();

            this.reportViewer1.RefreshReport();
        }

        private void staracki_izvjestaj()
        {
            string sql = "SELECT ime, prezime, broj FROM imena ";

            DataTable DTpopis = classSQL.select(sql, "popis").Tables[0];

            for (int i = 0; i < DTpopis.Rows.Count; i++)
            {
                DataRow DTrow1 = dSRlisteTekst.Tables[0].NewRow();
                DTrow1 = dSRlisteTekst.Tables[0].NewRow();
                DTrow1["naslov"] = DTpopis.Rows[i]["ime"].ToString();
                DTrow1["string3"] = DTpopis.Rows[i]["prezime"].ToString();
                DTrow1["string1"] = DTpopis.Rows[i]["broj"].ToString();
                dSRlisteTekst.Tables[0].Rows.Add(DTrow1);
            }
        }
    }
}