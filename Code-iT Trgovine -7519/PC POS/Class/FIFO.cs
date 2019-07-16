using System;
using System.Data;
using System.Windows.Forms;

namespace PCPOS.Class
{
    public static class FIFO
    {
        public static DataTable setRacunNbc(DataTable dt, string[] kolone = null, DateTime? datum = null)
        {
            try
            {
                string sKolicina = (kolone == null ? "kolicina" : kolone[0]);
                string sNbc = (kolone == null ? "nbc" : kolone[1]);
                string sSifra = (kolone == null ? "sifra_robe" : kolone[2]);
                string sSkladiste = (kolone == null ? "id_skladiste" : kolone[3]);

                foreach (DataRow dRow in dt.Rows)
                {
                    double _nbc = 0, _kol = 0;
                    double.TryParse(dRow[sKolicina].ToString(), out _kol);
                    double.TryParse(dRow[sNbc].ToString(), out _nbc);
                    string sql = string.Format("select * from getcurrnbc({0}, {1}, {2}, {3}, '{4}', {5}, TRUE);", Util.Korisno.GodinaKojaSeKoristiUbazi, (datum == null ? "null" : datum.Value.ToString("yyyy-MM-dd HH:mm:ss")), Convert.ToInt32(dRow[sSkladiste].ToString()), Math.Round(_kol, 6).ToString().Replace(',', '.'), dRow[sSifra].ToString(), Math.Round(_nbc, 6).ToString().Replace(',', '.'));

                    DataTable dtNbc = classSQL.select(sql, "NBC").Tables[0];
                    decimal nbc = 0;
                    if (decimal.TryParse(dtNbc.Rows[0]["currentvalue"].ToString(), out nbc))
                    {
                        if (nbc > 0)
                            dRow[sNbc] = nbc.ToString();
                        else
                            dRow[sNbc] = _nbc.ToString();
                    }
                }

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void setRacunDGW(ref DataGridView dgw, int skladiste, DateTime dt, string[] kolone = null)
        {
            try
            {
                string sKolicina = (kolone == null ? "kolicina" : kolone[0]);
                string sNbc = (kolone == null ? "nc" : kolone[1]);
                string sSifra = (kolone == null ? "sifra" : kolone[2]);
                //string sSkladiste = (kolone == null ? "id_skladiste" : kolone[3]);

                foreach (DataGridViewRow dRow in dgw.Rows)
                {
                    double _nbc = 0, _kol = 0;
                    double.TryParse(dRow.Cells[sKolicina].Value.ToString(), out _kol);
                    double.TryParse(dRow.Cells[sNbc].Value.ToString(), out _nbc);
                    //if (_kol == 0)
                    //_kol = 1;
                    string sql = string.Format("select * from getcurrnbc({0}, '{1}', {2}, {3}, '{4}',{5}, TRUE);", Util.Korisno.GodinaKojaSeKoristiUbazi, dt.ToString("yyyy-MM-dd HH:mm:ss"), skladiste, Math.Round(_kol, 6).ToString().Replace(',', '.'), dRow.Cells[sSifra].Value.ToString(), Math.Round(_nbc, 6).ToString().Replace(',', '.'));

                    DataTable dtNbc = classSQL.select(sql, "NBC").Tables[0];
                    decimal nbc = 0;
                    if (decimal.TryParse(dtNbc.Rows[0]["currentvalue"].ToString(), out nbc))
                    {
                        if (nbc > 0)
                            dRow.Cells[sNbc].Value = nbc.ToString();
                        else
                            dRow.Cells[sNbc].Value = _nbc.ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static decimal getNbc(int p, DateTime dateTime, int p_2, double kol, string p_3, double nbc)
        {
            try
            {
                string sql = string.Format("select * from getcurrnbc({0}, '{1}', {2}, {3}, '{4}',{5}, TRUE);", p, dateTime.ToString("yyyy-MM-dd HH:mm:ss"), p_2, kol.ToString().Replace(",", "."), p_3, nbc.ToString().Replace(',', '.'));
                DataTable dtNbc = classSQL.select(sql, "NBC").Tables[0];
                double _nbc = 0;
                if (dtNbc != null && dtNbc.Rows.Count > 0 && double.TryParse(dtNbc.Rows[0]["currentvalue"].ToString(), out _nbc) && _nbc > 0)
                {
                    return (decimal)_nbc;
                }

                return (decimal)nbc;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}