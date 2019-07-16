using Raverus.FiskalizacijaDEV;
using Raverus.FiskalizacijaDEV.PopratneFunkcije;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PCPOS.Fiskalizacija
{
    internal class classFiskalizacija__Staro
    {
        private static DataTable DTfis = classSQL.select_settings("SELECT * FROM fiskalizacija", "fiskalizacija").Tables[0];

        public static string[] Fiskalizacija(string oib,
            string oib_operatera,
            DataTable DTartikli,
            DateTime datum_racuna,
            bool sustavPDV,
            int broj_rac,
            string prodajnoMJ,
            int broj_kase,
            DataTable DTpdv,
            string[] porez_na_potrosnju,
            DataTable DTostali_porezi,
            string iznososlobpdv,
            string iznos_marza,
            DataTable DTnaknade,
            decimal ukupno,
            string nacin_placanja,
            bool naknadno_poslano)
        {
            string[] za_vratiti = new string[3];

            Raverus.FiskalizacijaDEV.Schema.ZaglavljeType zaglavlje = new Raverus.FiskalizacijaDEV.Schema.ZaglavljeType()
            {
                DatumVrijeme = Razno.DohvatiFormatiranoTrenutnoDatumVrijeme(),
                IdPoruke = Guid.NewGuid().ToString()
            };

            X509Certificate2 certifikat = Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.DohvatiCertifikat(DTfis.Rows[0]["naziv_certifikata"].ToString());
            string datum_vrijeme = DateTime.Now.ToString("dd.MM.yyyyThh:mm:ss");
            string ZkiGotov = ZKI(certifikat,
                oib,
                datum_vrijeme,
                broj_rac.ToString(),
                prodajnoMJ,
                broj_kase.ToString(),
                ukupno.ToString("#0.00").Replace(",", "."));

            string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>" +
                             @"<tns:RacunZahtjev xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Id=""signXmlId"" xmlns:tns=""http://www.apis-it.hr/fin/2012/types/f73"">" +

                                @"<tns:Zaglavlje>" +
                                @"<tns:IdPoruke>" + zaglavlje.IdPoruke + "</tns:IdPoruke>" +
                                @"<tns:DatumVrijeme>" + datum_vrijeme + "</tns:DatumVrijeme>" +
                                @"</tns:Zaglavlje>" +

                                @"<tns:Racun>" +

                                @"<tns:Oib>" + oib + "</tns:Oib>" +
                                @"<tns:USustPdv>" + sustavPDV.ToString().ToLower() + "</tns:USustPdv>" +
                                @"<tns:DatVrijeme>" + datum_racuna.ToString("dd.MM.yyyyThh:mm:ss") + "</tns:DatVrijeme>" +
                                @"<tns:OznSlijed>" + DTfis.Rows[0]["oznaka_slijednosti"].ToString() + "</tns:OznSlijed>" +

                                @"<tns:BrRac>" +
                                @"<tns:BrOznRac>" + broj_rac + "</tns:BrOznRac>" +
                                @"<tns:OznPosPr>" + prodajnoMJ + "</tns:OznPosPr>" +
                                @"<tns:OznNapUr>" + broj_kase + "</tns:OznNapUr>" +
                                @"</tns:BrRac>" +

                                PDVreturn(DTpdv) +
                                PorezNaPotrosnju(porez_na_potrosnju) +
                                OstaliPorezi(DTostali_porezi) +
                                IznosOslobPdv(iznososlobpdv) +
                                IznosMarza(iznos_marza) +
                                Naknade(DTnaknade) +

                                @"<tns:IznosUkupno>" + ukupno.ToString("#0.00").Replace(",", ".") + "</tns:IznosUkupno>" +
                                @"<tns:NacinPlac>" + nacin_placanja + "</tns:NacinPlac>" +
                                @"<tns:OibOper>" + oib_operatera + "</tns:OibOper>" +
                                @"<tns:ZastKod>" + ZkiGotov + "</tns:ZastKod>" +
                                @"<tns:NakDost>" + naknadno_poslano.ToString().ToLower() + "</tns:NakDost>" +

                                @"</tns:Racun>" +

                                @"</tns:RacunZahtjev>";

            XmlDocument dokument = new XmlDocument();
            dokument.LoadXml(xml);

            Raverus.FiskalizacijaDEV.CentralniInformacijskiSustav cis = new CentralniInformacijskiSustav();

            Raverus.FiskalizacijaDEV.PopratneFunkcije.Potpisivanje.PotpisiXmlDokument(dokument, certifikat);

            Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DodajSoapEnvelope(ref dokument);

            //XmlNode node = dokument.SelectSingleNode("Root/Node/Element");
            //node.Attributes[0].Value = "true";

            //string s = node.Attributes[0].ToString();

            dokument.Save("d:/fiskalizacija" + broj_rac + ".xml");

            try
            {
                XmlDocument odgovor = cis.PosaljiSoapPoruku(dokument);
                if (odgovor != null)
                {
                    string jir = Raverus.FiskalizacijaDEV.PopratneFunkcije.XmlDokumenti.DohvatiJir(odgovor);
                    za_vratiti[0] = jir;
                    za_vratiti[1] = ZkiGotov;
                    za_vratiti[1] = zaglavlje.IdPoruke;
                    return za_vratiti;
                }
                else
                {
                    za_vratiti[1] = ZkiGotov;
                    return za_vratiti;
                }
            }
            catch (Exception ex)
            {
                if (cis.OdgovorGreska != null)
                {
                    XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(dokument.NameTable);
                    xmlnsManager.AddNamespace("tns", "http://www.apis-it.hr/fin/2012/types/f73");
                    xmlnsManager.AddNamespace("s", "http://schemas.xmlsoap.org/soap/envelope/");
                    string Severity = dokument.SelectSingleNode("s:Envelope/s:Body/tns:RacunZahtjev/tns:Racun/tns:NakDost", xmlnsManager).ChildNodes[0].Value = "true";
                    dokument.Save("Fiskalizacija/_" + broj_rac.ToString() + ".xml");

                    MessageBox.Show(cis.OdgovorGreska.InnerXml); za_vratiti[1] = ZkiGotov; return za_vratiti;
                }
                else
                    MessageBox.Show(string.Format("Greska: {0}", ex.Message)); za_vratiti[1] = ZkiGotov; return za_vratiti;
            }
        }

        private static string PDVreturn(DataTable DTpdv)
        {
            string pdv = "";

            foreach (DataRow row in DTpdv.Rows)
            {
                pdv = pdv + @"<tns:Pdv>" +
                      @"<tns:Porez>" +
                      @"<tns:Stopa>" + decimal.Parse(row["stopa"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Stopa>" +
                      @"<tns:Osnovica>" + decimal.Parse(row["osnovica"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Osnovica>" +
                      @"<tns:Iznos>" + decimal.Parse(row["iznos"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Iznos>" +
                      @"</tns:Porez>" +
                      @"</tns:Pdv>";
            }
            return pdv;
        }

        private static string PorezNaPotrosnju(string[] porez)
        {
            string pnp = "";
            if (porez[2] != "0")
            {
                pnp = pnp + @"<tns:Pnp>" +
                            @"<tns:Porez>" +
                            @"<tns:Stopa>" + decimal.Parse(porez[0].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Stopa>" +
                            @"<tns:Osnovica>" + decimal.Parse(porez[1].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Osnovica>" +
                            @"<tns:Iznos>" + decimal.Parse(porez[2].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Iznos>" +
                            @"</tns:Porez>" +
                            @"</tns:Pnp>";
            }
            return pnp;
        }

        private static string OstaliPorezi(DataTable DTostali_porez)
        {
            string OstaliPor = "";
            foreach (DataRow row in DTostali_porez.Rows)
            {
                OstaliPor = OstaliPor + @"<tns:OstaliPor>" +
                                @"<tns:Porez>" +
                                @"<tns:Naziv>" + row["naziv"].ToString() + "</tns:Naziv>" +
                                @"<tns:Stopa>" + decimal.Parse(row["stopa"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Stopa>" +
                                @"<tns:Osnovica>" + decimal.Parse(row["osnovica"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Osnovica>" +
                                @"<tns:Iznos>" + decimal.Parse(row["iznos"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:Iznos>" +
                                @"</tns:Porez>" +
                                @"</tns:OstaliPor>";
            }
            return OstaliPor;
        }

        private static string Naknade(DataTable DTnaknade)
        {
            string naknade = "";
            foreach (DataRow row in DTnaknade.Rows)
            {
                naknade = naknade + @"<tns:Naknade>" +
                                @"<tns:Naknada>" +
                                @"<tns:NazivN>" + row["naziv"].ToString() + "</tns:NazivN>" +
                                @"<tns:IznosN>" + decimal.Parse(row["iznos"].ToString()).ToString("#0.00").Replace(",", ".") + "</tns:IznosN>" +
                                @"</tns:Naknada>" +
                                @"</tns:Naknade>";
            }
            return naknade;
        }

        private static string IznosOslobPdv(string iznos_oslob_pdv)
        {
            string OstaliPor = "";
            if (iznos_oslob_pdv != "")
            {
                OstaliPor = "<tns:IznosOslobPdv>" + decimal.Parse(iznos_oslob_pdv.ToString()).ToString("#0.00").Replace(",", ".") + "</tns:IznosOslobPdv>";
            }
            return OstaliPor;
        }

        private static string IznosMarza(string iznos_marza)
        {
            string neka_marza = "";
            if (iznos_marza != "")
            {
                neka_marza = "<tns:IznosMarza>" + decimal.Parse(iznos_marza.ToString()).ToString("#0.00").Replace(",", ".") + "</tns:IznosMarza>";
            }
            return neka_marza;
        }

        private static string ComputeHash(byte[] objectAsBytes)
        {
            //MD5 md5 =  new MD5CryptoServiceProvider();
            MD5 md5 = MD5.Create();
            try
            {
                byte[] result = md5.ComputeHash(objectAsBytes);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("x2"));
                }

                // And return it
                return sb.ToString();
            }
            catch (ArgumentNullException ane)
            {
                //If something occurred during serialization,
                //this method is called with a null argument.
                MessageBox.Show("Hash has not been generated.");
                return null;
            }
        }

        private static string ZKI(X509Certificate2 certifikat, string oibObveznika, string datumVrijemeIzdavanjaRacuna, string brojcanaOznakaRacuna, string oznakaPoslovnogProstora, string oznakaNaplatnogUredaja, string ukupniIznosRacuna)
        {
            string zastitniKod;

            StringBuilder sb = new StringBuilder();
            sb.Append(oibObveznika);
            sb.Append(datumVrijemeIzdavanjaRacuna);
            sb.Append(brojcanaOznakaRacuna);
            sb.Append(oznakaPoslovnogProstora);
            sb.Append(oznakaNaplatnogUredaja);
            sb.Append(ukupniIznosRacuna.Replace(',', '.'));

            byte[] by = Potpisivanje.PotpisiTekst(sb.ToString(), certifikat);
            if (by != null)
                zastitniKod = ComputeHash(by);
            else
                zastitniKod = "";

            return zastitniKod;
        }
    }
}