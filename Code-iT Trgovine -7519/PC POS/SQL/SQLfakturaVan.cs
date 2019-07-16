using Npgsql;
using System.Data;
using System.Data.SqlServerCe;

namespace PCPOS.SQL
{
    internal class SQLfakturaVan
    {
        public static string InsertStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }
                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();
                try
                {
                    nonqueryCommand.CommandText = "INSERT INTO faktura_van_stavke (kolicina,vpc,nbc,porez,broj_fakture_van,rabat,id_skladiste,sifra,oduzmi)" +
                        " VALUES (@kolicina,@vpc,@nbc,@porez,@broj_fakture,@rabat,@id_skladiste,@sifra,@oduzmi,@povratna_naknada)";
                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_fakture", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
                    nonqueryCommand.Parameters.Add("@povratna_naknada", SqlDbType.Money, 8);

                    nonqueryCommand.Prepare();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                        nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_fakture"].Value = DT.Rows[i]["broj_fakture"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                        nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                        nonqueryCommand.Parameters["@povratna_naknada"].Value = DT.Rows[i]["povratna_naknada"].ToString();
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
                    classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                //////////////////////////////////////////////////////REMOTE

                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }
                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();
                try
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "INSERT INTO faktura_van_stavke (kolicina,vpc,nbc,porez,broj_fakture,rabat,id_skladiste,sifra," +
                            "povratna_naknada,oduzmi,povratna_naknada_izn,rabat_izn,mpc_rabat,ukupno_rabat," +
                            "ukupno_vpc,ukupno_mpc,ukupno_mpc_rabat,ukupno_porez,ukupno_osnovica) VALUES " +
                            "(" +
                            "'" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            "'" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["nbc"].ToString() + "'," +
                            "'" + DT.Rows[i]["porez"].ToString() + "'," +
                            "'" + DT.Rows[i]["broj_fakture"].ToString() + "'," +
                            "'" + DT.Rows[i]["rabat"].ToString() + "'," +
                            "'" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            "'" + DT.Rows[i]["sifra"].ToString() + "'," +
                            "'" + DT.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                            "'" + DT.Rows[i]["povratna_naknada_izn"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["rabat_izn"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_vpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_mpc"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_porez"].ToString().Replace(",", ".") + "'," +
                            "'" + DT.Rows[i]["ukupno_osnovica"].ToString().Replace(",", ".") + "'" +
                            ")";

                        NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                    }

                    //nonqueryCommand.CommandText = "INSERT INTO faktura_van_stavke (kolicina,vpc,porez,broj_fakture_van,rabat,id_skladiste,sifra,oduzmi)" +
                    //    " VALUES (@kolicina,@vpc,@porez,@broj_fakture_van,@rabat,@id_skladiste,@sifra,@oduzmi)";
                    //nonqueryCommand.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 8);
                    //nonqueryCommand.Parameters.Add("@vpc", NpgsqlDbType.Money, 8);
                    //nonqueryCommand.Parameters.Add("@porez", NpgsqlDbType.Varchar, 10);
                    //nonqueryCommand.Parameters.Add("@broj_fakture_van", NpgsqlDbType.Bigint, 8);
                    //nonqueryCommand.Parameters.Add("@rabat", NpgsqlDbType.Varchar, 10);
                    //nonqueryCommand.Parameters.Add("@id_skladiste", NpgsqlDbType.Bigint, 8);
                    //nonqueryCommand.Parameters.Add("@sifra", NpgsqlDbType.Varchar, 20);
                    //nonqueryCommand.Parameters.Add("@oduzmi", NpgsqlDbType.Varchar, 2);
                    //nonqueryCommand.Prepare();
                    //for (int i = 0; i < DT.Rows.Count; i++)
                    //{
                    //    nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                    //    nonqueryCommand.Parameters["@vpc"].Value = Convert.ToDecimal( DT.Rows[i]["vpc"].ToString());
                    //    nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                    //    nonqueryCommand.Parameters["@broj_fakture_van"].Value = Convert.ToDecimal(DT.Rows[i]["broj_fakture_van"].ToString());
                    //    nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                    //    nonqueryCommand.Parameters["@id_skladiste"].Value = Convert.ToDecimal(DT.Rows[i]["id_skladiste"].ToString());
                    //    nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                    //    nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
                    //    nonqueryCommand.ExecuteNonQuery();
                    //}
                }
                catch (NpgsqlException ex)
                {
                    classSQL.remoteConnection.Close();
                    return ex.ToString();
                }
                finally
                {
                    //classSQL.remoteConnection.Close();
                }
                return "";
            }
        }

        public static string UpdateStavke(DataTable DT)
        {
            if (classSQL.remoteConnectionString == "")
            {
                if (classSQL.connection.State.ToString() == "Closed") { classSQL.connection.Open(); }

                SqlCeCommand nonqueryCommand = classSQL.connection.CreateCommand();

                try
                {
                    nonqueryCommand.CommandText = "UPDATE faktura_van_stavke SET kolicina=@kolicina,vpc=@vpc,nbc=@nbc,porez=@porez,broj_fakture_van=@broj_fakture,rabat=@rabat,id_skladiste=@id_skladiste,sifra=@sifra,oduzmi=@oduzmi,povratna_naknada=@povratna_naknada  WHERE sifra=@sifra AND broj_fakture_van=@broj_fakture_van";

                    nonqueryCommand.Parameters.Add("@kolicina", SqlDbType.NVarChar, 8);
                    nonqueryCommand.Parameters.Add("@vpc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@nbc", SqlDbType.Money, 8);
                    nonqueryCommand.Parameters.Add("@porez", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@broj_fakture", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@rabat", SqlDbType.NVarChar, 10);
                    nonqueryCommand.Parameters.Add("@id_skladiste", SqlDbType.BigInt, 8);
                    nonqueryCommand.Parameters.Add("@sifra", SqlDbType.NVarChar, 20);
                    nonqueryCommand.Parameters.Add("@oduzmi", SqlDbType.NVarChar, 2);
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
                        nonqueryCommand.Parameters["@kolicina"].Value = DT.Rows[i]["kolicina"].ToString();
                        nonqueryCommand.Parameters["@vpc"].Value = DT.Rows[i]["vpc"].ToString();
                        nonqueryCommand.Parameters["@nbc"].Value = DT.Rows[i]["nbc"].ToString();
                        nonqueryCommand.Parameters["@porez"].Value = DT.Rows[i]["porez"].ToString();
                        nonqueryCommand.Parameters["@broj_fakture"].Value = DT.Rows[i]["broj_fakture"].ToString();
                        nonqueryCommand.Parameters["@rabat"].Value = DT.Rows[i]["rabat"].ToString();
                        nonqueryCommand.Parameters["@id_skladiste"].Value = DT.Rows[i]["id_skladiste"].ToString();
                        nonqueryCommand.Parameters["@sifra"].Value = DT.Rows[i]["sifra"].ToString();
                        nonqueryCommand.Parameters["@oduzmi"].Value = DT.Rows[i]["oduzmi"].ToString();
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
                    classSQL.connection.Close();
                }
                return "";
            }
            else
            {
                //REMOTE UPDATE//////////////////////////////////////////////////////////////

                if (classSQL.remoteConnection.State.ToString() == "Closed") { classSQL.remoteConnection.Open(); }

                NpgsqlCommand nonqueryCommand = classSQL.remoteConnection.CreateCommand();

                try
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        string sql = "UPDATE faktura_van_stavke SET " +
                            " kolicina='" + DT.Rows[i]["kolicina"].ToString() + "'," +
                            "vpc='" + DT.Rows[i]["vpc"].ToString().Replace(",", ".") + "'," +
                            "nbc='" + DT.Rows[i]["nbc"].ToString() + "'," +
                            "porez='" + DT.Rows[i]["porez"].ToString() + "'," +
                            "broj_fakture='" + DT.Rows[i]["broj_fakture"].ToString() + "'," +
                            "rabat='" + DT.Rows[i]["rabat"].ToString() + "'," +
                            "id_skladiste='" + DT.Rows[i]["id_skladiste"].ToString() + "'," +
                            "sifra='" + DT.Rows[i]["sifra"].ToString() + "'," +
                            "povratna_naknada='" + DT.Rows[i]["povratna_naknada"].ToString().Replace(",", ".") + "'," +
                            "oduzmi='" + DT.Rows[i]["oduzmi"].ToString() + "'," +
                            "povratna_naknada_izn='" + DT.Rows[i]["povratna_naknada_izn"].ToString().Replace(",", ".") + "'," +
                            "rabat_izn='" + DT.Rows[i]["rabat_izn"].ToString().Replace(",", ".") + "'," +
                            "mpc_rabat='" + DT.Rows[i]["mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "ukupno_rabat='" + DT.Rows[i]["ukupno_rabat"].ToString().Replace(",", ".") + "'," +
                            "ukupno_vpc='" + DT.Rows[i]["ukupno_vpc"].ToString().Replace(",", ".") + "'," +
                            "ukupno_mpc='" + DT.Rows[i]["ukupno_mpc"].ToString().Replace(",", ".") + "'," +
                            "ukupno_mpc_rabat='" + DT.Rows[i]["ukupno_mpc_rabat"].ToString().Replace(",", ".") + "'," +
                            "ukupno_porez='" + DT.Rows[i]["ukupno_porez"].ToString().Replace(",", ".") + "'," +
                            "ukupno_osnovica='" + DT.Rows[i]["ukupno_osnovica"].ToString().Replace(",", ".") + "'" +
                            " WHERE id_stavka='" + DT.Rows[i]["id_stavka"].ToString() + "'";

                        NpgsqlCommand comm = new NpgsqlCommand(sql, classSQL.remoteConnection);
                        comm.ExecuteNonQuery();
                    }
                }
                catch (SqlCeException ex)
                {
                    classSQL.connection.Close();
                    return ex.ToString();
                }
                finally
                {
                    classSQL.connection.Close();
                }
                return "";
            }
        }
    }
}