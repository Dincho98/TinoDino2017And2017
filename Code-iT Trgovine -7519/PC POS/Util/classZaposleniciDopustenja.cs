using System.Data;

namespace PCPOS.Util
{
    internal class classZaposleniciDopustenja
    {
        public static DataTable DTz = classSQL.select("SELECT * FROM zaposlenici", "zaposlenici").Tables[0];

        private static bool superuser = false;

        public static bool provjeraZaposlenika(int razina)
        {
            if (superuser) return true;

            DataRow[] r = DTz.Select("id_zaposlenik='" + Properties.Settings.Default.id_zaposlenik + "'");

            int a;
            int.TryParse(r[0]["id_dopustenje"].ToString(), out a);

            if (razina < a) return false;
            else return true;
        }

        public static void superUserOn()
        {
            superuser = true;
        }

        public static void superUserOff()
        {
            superuser = false;
        }

        public static void osvjeziDopustenja()
        {
            DTz = classSQL.select("SELECT * FROM zaposlenici", "zaposlenici").Tables[0];
        }
    }
}