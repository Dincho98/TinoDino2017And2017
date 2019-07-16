using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PCPOS
{
    internal class variable
    {
        public static string ulog = "";
        public static string modul = GetProgram();
        private INIFile ini = new INIFile();

        public static string Zaposlenici()
        {
            string ulogirani = Properties.Settings.Default.id_zaposlenik;
            string sql = "SELECT zaposlenici.ime, zaposlenici.prezime, dopustenja.naziv" +
                " FROM zaposlenici LEFT JOIN dopustenja ON dopustenja.id_dopustenje = zaposlenici.id_dopustenje" +
                " WHERE id_zaposlenik = '" + ulogirani + "';";
            DataSet dsZaposlenici = classSQL.select(sql, "zaposlenici");
            if (dsZaposlenici != null && dsZaposlenici.Tables.Count > 0 && dsZaposlenici.Tables[0] != null && dsZaposlenici.Tables[0].Rows.Count > 0)
            {
                DataTable DTzaposlenici = dsZaposlenici.Tables[0];

                INIFile ini = new INIFile();
                string admin1 = "0";
                if (ini.Read("POSTAVKE", "admin_permision") != "")
                {
                    admin1 = ini.Read("POSTAVKE", "admin_permision");
                }
                string admin = "";
                if (admin1 == "0")
                {
                    try
                    {
                        if (DTzaposlenici.Rows.Count > 0)
                            admin = DTzaposlenici.Rows[0]["naziv"].ToString();
                        else
                            admin = "administrator";
                    }
                    catch
                    {
                        admin = "administrator";
                    }

                    if (admin.ToString().ToLower() == "administrator")
                    {
                        admin = "administrator";
                    }
                    else if (admin.ToString().ToLower() == "blagajnik")
                    {
                        admin = "administrator";
                    }
                    else if (admin.ToString().ToLower() == "korisnik")
                    {
                        admin = "administrator";
                    }

                    return admin;
                }
                else
                {
                    try
                    {
                        admin = DTzaposlenici.Rows[0]["naziv"].ToString();
                    }
                    catch
                    {
                        admin = "administrator";
                    }

                    if (admin.ToString().ToLower() == "administrator")
                    {
                        admin = "administrator";
                    }
                    else if (admin.ToString().ToLower() == "blagajnik")
                    {
                        admin = "blagajnik";
                    }
                    else if (admin.ToString().ToLower() == "korisnik")
                    {
                        admin = "korisnik";
                    }

                    return admin;
                }
            }

            return "";
        }

        public static string MessText()
        {
            string poruka = "Nemate administratorske ovlasti za otvaranje ove stavke ! \n\rRegistrirajte se kao administrator!";

            return poruka;
        }

        public static string GetProgram()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\settings.xml";
            XDocument xmlFile = XDocument.Load(path);
            var query = from c in xmlFile.Element("settings").Elements("program").Elements("run_progam") select c;
            foreach (XElement book in query)
            {
                return book.Attribute("run").Value;
            }

            return "";
        }
    }
}