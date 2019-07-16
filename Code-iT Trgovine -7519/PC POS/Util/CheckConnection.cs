using System;
using System.Runtime.InteropServices;

namespace PCPOS.Util
{
    internal class CheckConnection
    {
        //Check() radi dobro, ovu drugu nisam sprobal, trebala bi isto dobro raditi
        static public bool Check()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        ////http://www.c-sharpcorner.com/uploadfile/nipuntomar/check-internet-connection/
        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        static public bool CheckDatabase()
        {
            try
            {
                System.Data.SqlServerCe.SqlCeConnection connection = new System.Data.SqlServerCe.SqlCeConnection(classSQL.connectionString);
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}