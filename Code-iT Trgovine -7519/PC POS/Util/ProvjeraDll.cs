using System;
using System.IO;
using System.Reflection;

namespace PCPOS.Util
{
    internal class ProvjeraDll
    {
        static public void ZamijeniDll()
        {
            if (!Util.CheckConnection.Check())
            {
                return;
            }

            if (File.Exists("Raverus.FiskalizacijaDEV.XmlSerializersNew.dll"))
            {
                File.Delete("Raverus.FiskalizacijaDEV.XmlSerializers.dll");
                File.Move("Raverus.FiskalizacijaDEV.XmlSerializersNew.dll", "Raverus.FiskalizacijaDEV.XmlSerializers.dll");
            }

            if (File.Exists("Raverus.FiskalizacijaDEVNew.dll"))
            {
                File.Delete("Raverus.FiskalizacijaDEV.dll");
                File.Move("Raverus.FiskalizacijaDEVNew.dll", "Raverus.FiskalizacijaDEV.dll");
            }

            Assembly assembly = Assembly.LoadFrom("Raverus.FiskalizacijaDEV.XmlSerializers.dll");
            Version ver = assembly.GetName().Version;

            if (ver.Major < 2)
            {
                string file_web = "https://www.pc1.hr/pcpos/update/Raverus.FiskalizacijaDEV.XmlSerializers.dll";
                string path_save = "Raverus.FiskalizacijaDEV.XmlSerializersNew.dll";
                Util.classDownload.DownloadFile(file_web, path_save);
            }

            assembly = Assembly.LoadFrom("Raverus.FiskalizacijaDEV.dll");
            ver = assembly.GetName().Version;
            if (ver.Major < 3)
            {
                string file_web = "https://www.pc1.hr/pcpos/update/Raverus.FiskalizacijaDEV.dll";
                string path_save = "Raverus.FiskalizacijaDEVNew.dll";
                Util.classDownload.DownloadFile(file_web, path_save);
            }
        }
    }
}