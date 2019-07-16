using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PCPOS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Count() > 0 && args[0] == "OtvoriKronologijuPartnera")
            {
                frmPartnerKronologija pk = new frmPartnerKronologija();
                pk.ShowDialog();
            }

            // Ovaj di bi se mogao iskoristiti za rjesavanje regionalnih problema,
            // ali neznam kako ce postgresql funkcionirati ako regionalne postavke nisu uredu.
            //System.Globalization.CultureInfo c = new System.Globalization.CultureInfo("hr-HR");
            //System.Threading.Thread.CurrentThread.CurrentCulture = c;

            try
            {
                SkiniPotrebneStavke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (classSQL.remoteConnectionString.Length > 0 &&
                (System.Environment.MachineName == "POWER-RAC" || System.Environment.MachineName == "DejanVibović" || System.Environment.MachineName == "PC-PRO"))
                {
                    string[] server = classSQL.remoteConnectionString.Split(';');
                    if (server.Length > 0)
                    {
                        if (server[0].ToUpper().Trim() == "server=192.168.1.200".ToUpper().Trim())
                        {
                            if (MessageBox.Show("Koristite produktivni server.\r\nAko želite promijeniti konekciju protisnite \"Yes\".", "Upozorenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMenu());
                //Application.Run(new Report.Naljepnice.NaljepniceEan());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #region FUNKCIJA SKIDA SA FTP-A NOVIJE DLL-OVE ZA POSTGRES

        /// <summary>
        /// FUNKCIJA SKIDA SA FTP-A NOVIJE DLL-OVE ZA POSTGRES
        /// </summary>
        private static void SkiniPotrebneStavke()
        {
            bool moram_skidati = false;
            //PCPOS.Util.classDownloadFiles D = new Util.classDownloadFiles();
            //if (File.Exists("DBbackup\\VerzijaPostgres.txt"))
            //{
            //    string verzijaPG = File.ReadAllText("DBbackup\\VerzijaPostgres.txt");
            //    if (verzijaPG != "9.2")
            //    {
            //        moram_skidati = true;
            //    }
            //}
            //else
            //{
            //    moram_skidati = true;
            //}

            //if (moram_skidati)
            //{
            //    try
            //    {
            //        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //        if (!File.Exists("SkiniNovijeFajloveZaPostgres.exe"))
            //        {
            //            D.SkiniDatoteku("http://pc1.hr/caffe/update/ostalo/ftpbin/SkiniNovijeFajloveZaPostgres.doc", "SkiniNovijeFajloveZaPostgres.exe");
            //            MessageBox.Show("Aplikacija se mora ugasiti i skinuti neke dodatke za bazu podataka!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            GC.Collect();
            //        }

            //        GC.Collect();
            //        Process proc = Process.Start(path + "\\SkiniNovijeFajloveZaPostgres.exe");
            //        Application.Exit();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString());
            //    }
            //}
        }

        #endregion FUNKCIJA SKIDA SA FTP-A NOVIJE DLL-OVE ZA POSTGRES
    }
}