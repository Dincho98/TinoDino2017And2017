using Npgsql;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLracun
    {
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }
                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste," +
                        "mpc,porez,kolicina,rabat,vpc,nbc,porez_potrosnja,naziv)" +
                        " VALUES (@broj_racuna,@sifra_robe,@id_skladiste,@mpc,@porez,@kolicina,@rabat,@vpc,@nbc,@porez_potrosnja,@naziv," +
                        "@povratna_naknada,@povratna_naknada_izn,@rabat_izn,@mpc_rabat,@ukupno_rabat,@ukupno_vpc,@ukupno_mpc," +
                        "@ukupno_mpc_rabat,@ukupno_porez,@ukupno_osnovica)";

                    nonqueryCommand.Parameters.Add("@broj_racuna", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@sifra_robe", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.Int, 4);
                    nonqueryCommand.Parameters.Add("@mpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 5);
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 6);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 4);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money);
                    nonqueryCommand.Parameters.Add("@porez_potrosnja", SqlDbType.Money);
                    nonqueryCommand.Parameters.Add("@naziv", SqlDbType.NVarChar, 100);
                    nonqueryCommand.Parameters.Add("@povratna_naknada", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@povratna_naknada_izn", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@rabat_izn", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@mpc_rabat", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_rabat", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_mpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_mpc_rabat", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_porez", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@ukupno_osnovica", SqlDbType.Money, 8);
                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@broj_racuna"].Value = DT.Rows[i]["broj_racuna"].ToString();
                        nonqueryCommand.Parameters["@sifra_robe"].Value = DT.Rows[i]["sifra_robe"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@mpc"].Value = DT.Rows[i]["mpc"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();

                        string naziv = DT.Rows[i]["naziv"].ToString();
                        nonqueryCommand.Parameters["@naziv"].Value = !string.IsNullOrWhiteSpace(naziv) ? naziv : "";

                        nonqueryCommand.Parameters["@porez_potrosnja"].Value = DT.Rows[i]["porez_potrosnja"].ToString();
                        nonqueryCommand.Parameters["@povratna_naknada"].Value = DT.Rows[i]["povratna_naknada"].ToString();
                        nonqueryCommand.Parameters["@povratna_naknada_izn"].Value = DT.Rows[i]["povratna_naknada_izn"].ToString();
                        nonqueryCommand.Parameters["@rabat_izn"].Value = DT.Rows[i]["rabat_izn"].ToString();
                        nonqueryCommand.Parameters["@mpc_rabat"].Value = DT.Rows[i]["mpc_rabat"].ToString();
                        nonqueryCommand.Parameters["@ukupno_rabat"].Value = DT.Rows[i]["ukupno_rabat"].ToString();
                        nonqueryCommand.Parameters["@ukupno_vpc"].Value = DT.Rows[i]["ukupno_vpc"].ToString();
                        nonqueryCommand.Parameters["@ukupno_mpc"].Value = DT.Rows[i]["ukupno_mpc"].ToString();
                        nonqueryCommand.Parameters["@ukupno_mpc_rabat"].Value = DT.Rows[i]["ukupno_mpc_rabat"].ToString();
                        nonqueryCommand.Parameters["@ukupno_porez"].Value = DT.Rows[i]["ukupno_porez"].ToString();
                        nonqueryCommand.Parameters["@ukupno_osnovica"].Value = DT.Rows[i]["ukupno_osnovica"].ToString();
                        nonqueryCommand.ExecuteNonQuery();
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
                finally
                {
                    //classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string naziv = DT.Rows[i]["ime"].ToString();

                        string sql = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat," +
                            "vpc,nbc,porez_potrosnja,naziv,povratna_naknada,povratna_naknada_izn,rabat_izn,mpc_rabat,ukupno_rabat," +
                            "ukupno_vpc,ukupno_mpc,ukupno_mpc_rabat,ukupno_porez,ukupno_osnovica,id_ducan,id_kasa,porez_na_dohodak,prirez,porez_na_dohodak_iznos,prirez_iznos)" +
                            " VALUES (" +
                            "'" + DT.Rows[i]["broj_racuna"].ToString() + "'," +
                            "'" + DT.Rows[i]["sifra_robe"].ToString() + "'," +
                            "'" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            "'" + DT.Rows[i]["mpc"].ToString() + "'," +
                            "'" + DT.Rows[i]["porez"].ToString() + "'," +
                            "'" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            "'" + DT.Rows[i]["rabat"].ToString() + "'," +
                            "'" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["nbc"].ToString() + "'," +
                            "'" + DT.Rows[i]["porez_potrosnja"].ToString() + "'," +
                            "'" + (!string.IsNullOrWhiteSpace(naziv) ? naziv : "") + "'," +
                            "'" + DT.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["povratna_naknada_izn"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["rabat_izn"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_mpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_porez"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_osnovica"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["id_ducan"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["id_kasa"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["porez_na_dohodak"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["prirez"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["porez_na_dohodak_iznos"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["prirez_iznos"].ToString().Replace(",", ".") + "'" +
                            ")";

                        //NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        //comm.ExecuteNonQuery();
                        classSQL.transaction(sql);
                    }

                    //nonqueryCommand.CommandText = "INSERT INTO racun_stavke (broj_racuna,sifra_robe,id_skladiste,mpc,porez,kolicina,rabat,vpc)" +
                    //    " VALUES (@broj_racuna,@sifra_robe,@id_skladiste,@mpc,@porez,@kolicina,@rabat,@vpc)";

                    //nonqueryCommand.Parameters.Add("@broj_racuna", NpgsqlDbType.Varchar, 10);
                    //nonqueryCommand.Parameters.Add("@sifra_robe", NpgsqlDbType.Varchar, 20);
                    //nonqueryCommand.Parameters.Add("@id_skladiste", NpgsqlDbType.Integer, 4);
                    //nonqueryCommand.Parameters.Add("@mpc", NpgsqlDbType.Money);
                    //nonqueryCommand.Parameters.Add("@vpc", NpgsqlDbType.Money);
                    //nonqueryCommand.Parameters.Add("@porez", NpgsqlDbType.Varchar, 5);
                    //nonqueryCommand.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 6);
                    //nonqueryCommand.Parameters.Add("@rabat", NpgsqlDbType.Varchar, 4);
                    //nonqueryCommand.Prepare();
                    //for (int i = 0; i < DT.Rows.Count; i++)
                    //{
                    //    nonqueryCommand.Parameters["@broj_racuna"].Value = DT.Rows[i]["broj_racuna"].ToString();
                    //    nonqueryCommand.Parameters["@sifra_robe"].Value = DT.Rows[i]["sifra_robe"].ToString();
                    //    nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                    //    nonqueryCommand.Parameters["@mpc"].Value = decimal.Parse(DT.Rows[i]["mpc"].ToString());
                    //    nonqueryCommand.Parameters["@vpc"].Value = decimal.Parse(DT.Rows[i]["vpc"].ToString());
                    //    nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                    //    nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                    //    nonqueryCommand.Parameters["@rabat"].Value = Convert.ToDecimal(DT.Rows[i]["rabat"].ToString());
                    //    nonqueryCommand.ExecuteNonQuery();
                    //}
                }
                catch (SqlCeException ex)
                {
                    classSQL.remoteConnection.Close();
                    return ex.ToString();
                }
                finally
                {
                    //classSQL.connection.Close();
                }
                return "";
            }
        }
    }
}