using System;
using System.Windows.Forms;

namespace PCPOS.Util
{
    internal class AktivnostZaposlenika
    {
        public static void SpremiAktivnost(DataGridView _dgv, string _skladiste, string _dokumenat, string _broj, bool _UredivanjeDokumenta)
        {
            try
            {
                string IDzaposlenik = Properties.Settings.Default.id_zaposlenik;
                DateTime Datum = DateTime.Now;
                string StavkeSaDokumenta = UzmiStavkeIzDataGrid(_dgv, _skladiste);
                string uredivano = _UredivanjeDokumenta == true ? "1" : "0";
                string skladiste = _skladiste == null ? "0" : _skladiste;

                string sql = "INSERT INTO povijest_koristenja_dokumenata (" +
                    "dokument," +
                    "id_izradio," +
                    "datum," +
                    "stavke," +
                    "editirano," +
                    "broj_dokumenta," +
                    "id_skladiste" +
                    ") VALUES (" +
                    "'" + _dokumenat + "'," +
                    "'" + IDzaposlenik + "'," +
                    "'" + Datum + "'," +
                    "'" + StavkeSaDokumenta + "'," +
                    "'" + uredivano + "'," +
                    "'" + _broj + "'," +
                    skladiste +
                    ")";
                classSQL.insert(sql);
            }
            catch { /* Ovaj try-catch sluzi da funkcija nebi remetila normalno odvijanje programa */ }
        }

        #region UZMI STAVKE IZ DATAGRIDA

        private static string UzmiStavkeIzDataGrid(DataGridView _dgv, string _skladiste)
        {
            string KolonaSifra = "";
            string KolonaSkladiste = "";
            string KolonaKolicina = "";
            string StringZaVratiti = "";

            foreach (DataGridViewColumn c in _dgv.Columns)
            {
                //Tražim kolonu šifra
                if (c.Name == "sifra") { KolonaSifra = "sifra"; }
                if (c.Name == "sifra_roba") { KolonaSifra = "sifra_roba"; }
                if (c.Name == "sifra_robe") { KolonaSifra = "sifra_robe"; }
                //Tražim kolonu skladište
                if (c.Name == "skladiste") { KolonaSkladiste = "skladiste"; }
                else if (c.Name == "id_skladiste") { KolonaSkladiste = "id_skladiste"; }

                if (c.Name == "kolicina") { KolonaKolicina = "kolicina"; }
            }

            //**********************************************************************************************
            //Za razdjeljivanje između šifre, kolicine i skladišta korišten je znak; ˇ
            //Za razdjeljivanje nove stavke znak: ; (točka zarez)
            //**********************************************************************************************
            foreach (DataGridViewRow r in _dgv.Rows)
            {
                if (_skladiste != null)
                {
                    StringZaVratiti += "sif:" + r.Cells[KolonaSifra].FormattedValue.ToString() + "ˇ" + "kol:" + r.Cells[KolonaKolicina].FormattedValue.ToString() + "ˇ" + "skl:" + _skladiste + ";";
                }
                else
                {
                    if (_dgv.Columns[KolonaSkladiste].CellType.ToString() == "System.Windows.Forms.DataGridViewComboBoxCell")
                    {
                        StringZaVratiti += "sif:" + r.Cells[KolonaSifra].FormattedValue.ToString() + "ˇ" + "kol:" + r.Cells[KolonaKolicina].FormattedValue.ToString() + "ˇ" + "skl:" + r.Cells[KolonaSkladiste].Value.ToString() + ";";
                    }
                    else
                    {
                        StringZaVratiti += "sif:" + r.Cells[KolonaSifra].FormattedValue.ToString() + "ˇ" + "kol:" + r.Cells[KolonaKolicina].FormattedValue.ToString() + "ˇ" + "skl:" + r.Cells[KolonaSkladiste].FormattedValue.ToString() + ";";
                    }
                }
            }

            return StringZaVratiti;
        }

        #endregion UZMI STAVKE IZ DATAGRIDA
    }
}