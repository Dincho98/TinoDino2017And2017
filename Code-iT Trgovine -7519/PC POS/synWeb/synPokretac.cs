using System;
using System.Data;
using System.Linq;

namespace PCPOS.synWeb
{
    internal class synPokretac
    {
        private synRacuni Racuni = new synRacuni(false);
        private synKalkulacije Kalkulacija = new synKalkulacije(false);
        private synPrimka Primka = new synPrimka(false);
        private synArtikli Artikli = new synArtikli(false);
        private synRobaProdaja RobaProdaja = new synRobaProdaja(false);
        private synGrupe Grupe = new synGrupe(false);
        private synZaposlenici Zaposlenici = new synZaposlenici(false);
        private synOtpisRobe OtpisRobe = new synOtpisRobe(false);
        private synPovratRobe PovratRobe = new synPovratRobe(false);
        private synPocetnoStanje PocetnoStanje = new synPocetnoStanje(false);
        private synInventura Inventura = new synInventura(false);
        private synPartner Partneri = new synPartner(false);
        private synFakture Fakture = new synFakture(false);
        private synOtpremnice Otpremnice = new synOtpremnice(false);
        private synSkladista Skladiste = new synSkladista(false);
        private synPoslovnice Poslovnice = new synPoslovnice(false);
        private synMeduSkladiste MeduSkladiste = new synMeduSkladiste(false);

        private synKalkulacije1 Kalkulacija1 = new synKalkulacije1(false);
        /*
        synPromjena_cijene PromjenaCijena = new synPromjena_cijene(false);
        synPoslovnice Poslovnice = new synPoslovnice(false);
        synMeduskladisnica Meduskl = new synMeduskladisnica(false);
        synPotrosniMaterijal PotrosniMat = new synPotrosniMaterijal(false);
        synBlagajnickiIzvjestaj BlagajnickiIzvjestaj = new synBlagajnickiIzvjestaj(false);*/
        private newSql SqlPostgres = new newSql();
        private DataTable DTpostavke;

        public synPokretac()
        {
            DTpostavke = SqlPostgres.select_settings("SELECT * FROM postavke", "postavke").Tables[0];
        }

        /*
            DELETE FROM racuni WHERE poslovnica='POWER';
            DELETE FROM fakture WHERE poslovnica='POWER';
            DELETE FROM roba WHERE poslovnica='POWER';
            DELETE FROM roba_prodaja WHERE poslovnica='POWER';
            DELETE FROM normativ WHERE poslovnica='POWER';
            DELETE FROM partneri WHERE poslovnica='POWER';
         */

        /// <summary>
        /// FUNKCIJA KOJA POZIVA SINKRONIZACIJU SA WEBOM
        /// </summary>
        /// <param name="_artikli"></param>
        /// <param name="_roba_prodaja"></param>
        /// <param name="_racuni"></param>
        /// <param name="_primke"></param>
        /// <param name="_grupe"></param>
        public void PokreniSinkronizaciju(bool _racuni, bool _kalkulacija, bool _primke, bool _artikli, bool _roba_prodaja, bool _grupe, bool _zaposlenici,
            bool _otpis_robe, bool _povrat_robe, bool _pocetno_stanje, bool _inventura, bool _partneri, bool _fakture, bool _otpremnice, bool _medu_skladiste)
        {
            if (System.Environment.MachineName == "PC-PRO")
            {
                return;
            }

            if (DTpostavke.Rows[0]["centrala_aktivno"].ToString().ToLower() == "true")
            {
                string sql = "select id_skl_centrala from skladiste where aktivnost = 'DA' and id_skl_centrala <> 0;";
                DataSet dsSkladista = classSQL.select(sql, "skladiste");
                if (_kalkulacija)
                {
                    if (dsSkladista != null && dsSkladista.Tables.Count > 0 && dsSkladista.Tables[0] != null && dsSkladista.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drow in dsSkladista.Tables[0].Rows)
                        {
                            Kalkulacija1.UzmiPodatkeSaWeba(DTpostavke.Rows[0]["centrala_poslovnica"].ToString(), Convert.ToInt32(drow["id_skl_centrala"]));
                        }
                    }
                }

                if (_artikli)
                {
                    foreach (DataRow drow in dsSkladista.Tables[0].Rows)
                    {
                        sql = @"select ks.sifra
from kalkulacija_stavke ks
left join roba r on ks.sifra = r.sifra
where ks.id_skladiste = '" + Convert.ToInt32(drow["id_skl_centrala"]) + @"' and r.sifra is null";

                        DataSet dsArtikli = classSQL.select(sql, "kalkulacina_stavke");
                        if (dsArtikli != null && dsArtikli.Tables.Count > 0 && dsArtikli.Tables[0] != null && dsArtikli.Tables[0].Rows.Count > 0)
                        {
                            string sifra = "";
                            var idlist = dsArtikli.Tables[0].AsEnumerable().Select(r => "'" + r.Field<string>("sifra") + "'").ToArray();
                            sifra = string.Join(", ", idlist);
                            idlist = null;

                            //foreach (DataRow dr in dsArtikli.Tables[0].Rows) {
                            //    if (sifra.Length > 0) {
                            //        sifra += ", ";
                            //    }

                            //    sifra += "'" + dr["sifra"] + "'";
                            //}
                            Artikli.UzmiPodatkeSaWeba(DTpostavke.Rows[0]["centrala_poslovnica"].ToString(), Convert.ToInt32(drow["id_skl_centrala"]), sifra);
                        }
                    }
                }

                if (_roba_prodaja)
                {
                    foreach (DataRow drow in dsSkladista.Tables[0].Rows)
                    {
                        sql = @"select ks.sifra
from kalkulacija_stavke ks
left join roba_prodaja r on ks.sifra = r.sifra and ks.id_skladiste = r.id_skladiste
where ks.id_skladiste = '" + Convert.ToInt32(drow["id_skl_centrala"]) + @"' and r.sifra is null";

                        DataSet dsArtikli = classSQL.select(sql, "kalkulacina_stavke");
                        if (dsArtikli != null && dsArtikli.Tables.Count > 0 && dsArtikli.Tables[0] != null && dsArtikli.Tables[0].Rows.Count > 0)
                        {
                            string sifra = "";
                            var idlist = dsArtikli.Tables[0].AsEnumerable().Select(r => "'" + r.Field<string>("sifra") + "'").ToArray();
                            sifra = string.Join(", ", idlist);
                            idlist = null;
                            //foreach (DataRow dr in dsArtikli.Tables[0].Rows) {
                            //    if (sifra.Length > 0) {
                            //        sifra += ", ";
                            //    }

                            //    sifra += "'" + dr["sifra"] + "'";
                            //}
                            RobaProdaja.UzmiPodatkeSaWeba(DTpostavke.Rows[0]["centrala_poslovnica"].ToString(), Convert.ToInt32(drow["id_skl_centrala"]), sifra);
                        }
                    }
                }
            }
            else if (DTpostavke.Rows[0]["posalji_dokumente_na_web"].ToString() == "1" &&
                System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() &&
                !Util.Korisno.RadimSinkronizaciju)
            {
                //return;

                /*KORISTIM AKO JE OVO MOJE RAČUNALO DA NEBI SLUČAJNO KORISTIO SINKRONIZACIJU SA WEB-OM*/
                if (System.Environment.MachineName == "POWER-RAC")
                {
                    Properties.Settings.Default.domena_za_sinkronizaciju = "http://localhost/pcpos.pc1.hr/";
                    Properties.Settings.Default.Save();
                }

                Util.Korisno.RadimSinkronizaciju = true;

                if (_racuni) { Racuni.Send(); }
                if (_kalkulacija) { Kalkulacija1.Send(); Kalkulacija1.UzmiPodatkeSaWeba(); }
                if (_primke) { Primka.Send(); }
                if (_artikli) { Artikli.Send(); Artikli.UzmiPodatkeSaWeba(); }
                if (_roba_prodaja) { RobaProdaja.Send(); RobaProdaja.UzmiPodatkeSaWeba(); }
                if (_grupe) { Grupe.Send(); Skladiste.Send(); }//Poslovnice.Send(); }
                if (_zaposlenici) { Zaposlenici.Send(); Zaposlenici.UzmiPodatkeSaWeba(); }
                if (_otpis_robe) { OtpisRobe.Send(); }
                //if (_povrat_robe) { PovratRobe.Send(); }
                if (_pocetno_stanje) { PocetnoStanje.Send(); PocetnoStanje.UzmiPodatkeSaWeba(); }
                //if (_inventura) { Inventura.Send(); Inventura.UzmiPodatkeSaWeba(); }
                if (_partneri) { Partneri.Send(); Partneri.UzmiPodatkeSaWeba(); }
                if (_fakture) { Fakture.Send(); Fakture.UzmiPodatkeSaWeba(); }
                if (_otpremnice) { Otpremnice.Send(); Otpremnice.UzmiPodatkeSaWeba(); }
                if (_medu_skladiste) { MeduSkladiste.Send(); MeduSkladiste.UzmiPodatkeSaWeba(); }
                Util.Korisno.RadimSinkronizaciju = false;
            }
        }
    }
}