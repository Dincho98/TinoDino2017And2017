using Npgsql;
using NpgsqlTypes;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Forms;

namespace PCPOS
{
    internal class classSQL
    {
        public static string connectionString = SQL.claasConnectDatabase.GetCompactConnectionString();
        public static DataSet dataSet = new DataSet();
        public static SqlCeConnection connection = new SqlCeConnection(connectionString);
        public static SqlCeDataAdapter adapter = new SqlCeDataAdapter();
        public static NpgsqlDataAdapter Npgadapter = new NpgsqlDataAdapter();
        public static string remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
        public static NpgsqlConnection remoteConnection = new NpgsqlConnection(remoteConnectionString);

        public static void SetCaffeRemoteString()
        {
            remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString(true);
            remoteConnection = new NpgsqlConnection(remoteConnectionString);
        }

        //SELECT
        public static DataSet select(string sql, string table, bool caffe = false)
        {
            if (caffe)
                SetCaffeRemoteString();
            else
            {
                remoteConnectionString = SQL.claasConnectDatabase.GetRemoteConnectionString();
                remoteConnection = new NpgsqlConnection(remoteConnectionString);
            }

            try
            {
                if (remoteConnectionString == "")
                {
                    if (connection.State == ConnectionState.Closed) { connection.Open(); }
                    dataSet = new DataSet();
                    SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql.Replace("zbroj", "+"), connectionString);
                    adapter.Fill(dataSet, table);
                    connection.Close();
                    return dataSet;
                }
                else
                {
                    dataSet = new DataSet();
                    if (remoteConnection.State == ConnectionState.Closed) { remoteConnection.Open(); }
                    string comm = sql;
                    if (!sql.ToLower().Contains("setval"))
                    {
                        comm = sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+");
                    }
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(comm, remoteConnection);
                    dataSet.Reset();
                    da.Fill(dataSet);
                    remoteConnection.Close();
                    return dataSet;
                }
            }
            catch (NpgsqlException ex)
            {
                OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                remoteConnection.Close();
                return dataSet;
            }
        }

        //SELECT SETTINGS
        public static DataSet select_settings(string sql, string table)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
                dataSet = new DataSet();
                SqlCeDataAdapter adapter = new SqlCeDataAdapter(sql, connectionString);
                adapter.Fill(dataSet, table);
                connection.Close();
                return dataSet;
            }
            catch (Exception ex)
            {
                OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return dataSet;
            }
        }

        public static string Setings_Update(string sql)
        {
            try
            {
                if (connection.State == ConnectionState.Closed) { connection.Open(); }
                SqlCeCommand comm = new SqlCeCommand(sql, connection);
                comm.ExecuteNonQuery();
                connection.Close();
                return "";
            }
            catch (NpgsqlException ex)
            {
                connection.Close();
                OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                return ex.ToString();
            }
        }

        //INSERT
        public static string insert(string sql)
        {
            //sql = sql.Replace('\'', ' ');
            if (remoteConnectionString == "")
            {
                try
                {
                    if (connection.State.ToString() == "Closed") { connection.Open(); }
                    SqlCeTransaction tx = connection.BeginTransaction();
                    SqlCeCommand cmd = connection.CreateCommand();
                    cmd.Transaction = tx;
                    cmd.Connection = connection;
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                    connection.Close();
                    return "";
                }
                catch (Exception msg)
                {
                    connection.Close();
                    OstaleFunkcije.SetInLog(sql + "\r\n" + msg.ToString(), "65", "RemoteDB");
                    return msg.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                    comm.CommandTimeout = 60;
                    comm.ExecuteNonQuery();
                    remoteConnection.Close();
                    return "";
                }
                catch (NpgsqlException ex)
                {
                    remoteConnection.Close();
                    OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                    return ex.ToString();
                }
            }
        }

        //UPDATE

        public static string update(string sql)
        {
            //sql = sql.Replace('\'', ' ');
            if (remoteConnectionString == "")
            {
                try
                {
                    {
                        if (connection.State.ToString() == "Closed") { connection.Open(); }
                        dataSet = new DataSet();
                        adapter.SelectCommand = new SqlCeCommand(sql, connection);
                        SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
                        adapter.Fill(dataSet);
                        connection.Close();
                    }

                    return "";
                }
                catch (Exception ex)
                {
                    connection.Close();
                    OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                    comm.CommandTimeout = 1200;
                    comm.ExecuteNonQuery();
                    remoteConnection.Close();
                    return "";
                }
                catch (NpgsqlException ex)
                {
                    remoteConnection.Close();
                    OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                    return ex.ToString();
                }
            }
        }

        //ADAPTER
        public static SqlCeDataAdapter CeAdatpter(string sql)
        {
            if (connection.State.ToString() == "Closed") { connection.Open(); }
            dataSet = new DataSet();
            adapter.SelectCommand = new SqlCeCommand(sql.Replace("zbroj", "+"), connection);
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
            //connection.Close();
            return adapter;
        }

        public static NpgsqlDataAdapter NpgAdatpter(string sql)
        {
            try
            {
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                dataSet = new DataSet();
                Npgadapter = new NpgsqlDataAdapter();
                string sqlll = sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+");
                Npgadapter.SelectCommand = new NpgsqlCommand(sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+"), remoteConnection);
                //string debug = sql.Replace("+", "||").Replace("[", "\"").Replace("]", "\"").Replace("zbroj", "+");
                NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(Npgadapter);
                //remoteConnection.Close();
                return Npgadapter;
            }
            catch (NpgsqlException ex)
            {
                OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                remoteConnection.Close();
                return Npgadapter;
            }
        }

        //DELETE
        public static string delete(string sql)
        {
            if (remoteConnectionString == "")
            {
                try
                {
                    if (connection.State.ToString() == "Closed") { connection.Open(); }
                    SqlCeTransaction tx = connection.BeginTransaction();
                    SqlCeCommand cmd1 = connection.CreateCommand();
                    cmd1.Transaction = tx;
                    cmd1.Connection = connection;
                    cmd1.CommandText = sql;
                    cmd1.ExecuteNonQuery();
                    tx.Commit();
                    connection.Close();
                    return "";
                }
                catch (Exception ex)
                {
                    OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                    connection.Close();
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    NpgsqlCommand comm = new NpgsqlCommand(sql, remoteConnection);
                    comm.CommandTimeout = 60;
                    comm.ExecuteNonQuery();
                    remoteConnection.Close();
                    return "";
                }
                catch (NpgsqlException ex)
                {
                    remoteConnection.Close();
                    OstaleFunkcije.SetInLog(sql + "\r\n" + ex.ToString(), "65", "RemoteDB");
                    return ex.ToString();
                }
            }
        }

        public static string UpdateRows(string id_skladiste, string kolicina, string nc, string vpc, string porez, string sifra)
        {
            if (remoteConnectionString == "")
            {
                try
                {
                    if (connection.State.ToString() == "Closed") { connection.Open(); }
                    string updateSql = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";
                    SqlCeCommand UpdateCmd = new SqlCeCommand(updateSql, connection);

                    UpdateCmd.Parameters.Add("@id_skladiste", SqlDbType.Int, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", SqlDbType.NVarChar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@npc", SqlDbType.Money, 8, "npc");
                    UpdateCmd.Parameters.Add("@vpc", SqlDbType.Money, 8, "vpc");
                    UpdateCmd.Parameters.Add("@porez", SqlDbType.NVarChar, 20, "porez");
                    UpdateCmd.Parameters.Add("@sifra", SqlDbType.NVarChar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@npc"].Value = kolicina;
                    UpdateCmd.Parameters["@vpc"].Value = vpc;
                    UpdateCmd.Parameters["@porez"].Value = porez;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();
                    connection.Close();
                    return "";
                }
                catch (SqlCeException ex)
                {
                    connection.Close();
                    return ex.ToString();
                }
            }
            else
            {
                try
                {
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    string updateSql = "UPDATE roba_prodaja " + "SET kolicina = @kolicina " + "WHERE id_skladiste = @id_skladiste AND sifra=@sifra";
                    NpgsqlCommand UpdateCmd = new NpgsqlCommand(updateSql, remoteConnection);

                    UpdateCmd.Parameters.Add("@id_skladiste", NpgsqlDbType.Integer, 4, "id_skladiste");
                    UpdateCmd.Parameters.Add("@kolicina", NpgsqlDbType.Varchar, 10, "kolicina");
                    UpdateCmd.Parameters.Add("@npc", NpgsqlDbType.Money, 8, "npc");
                    UpdateCmd.Parameters.Add("@vpc", NpgsqlDbType.Money, 8, "vpc");
                    UpdateCmd.Parameters.Add("@porez", NpgsqlDbType.Varchar, 20, "porez");
                    UpdateCmd.Parameters.Add("@sifra", NpgsqlDbType.Varchar, 20, "sifra");

                    UpdateCmd.Parameters["@sifra"].Value = sifra;
                    UpdateCmd.Parameters["@id_skladiste"].Value = id_skladiste;
                    UpdateCmd.Parameters["@npc"].Value = kolicina;
                    UpdateCmd.Parameters["@vpc"].Value = vpc;
                    UpdateCmd.Parameters["@porez"].Value = porez;
                    UpdateCmd.Parameters["@kolicina"].Value = kolicina;

                    UpdateCmd.ExecuteNonQuery();
                    remoteConnection.Close();
                    return "";
                }
                catch (SqlCeException ex)
                {
                    remoteConnection.Close();
                    return ex.ToString();
                }
            }
        }

        private static string transtring = "";

        public static string transaction(string sql)
        {
            switch (sql.Trim().ToUpper())
            {
                case "BEGIN":
                case "BEGIN;":
                    if (transtring != "") MessageBox.Show("Prethodni dokument nije pravilno spremljen", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto case "ROLLBACK;";
                case "ROLLBACK":
                case "ROLLBACK;":
                    transtring = "";
                    return "";

                case "COMMIT":
                case "COMMIT;":
                    try
                    {
                        if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                        NpgsqlCommand comm = new NpgsqlCommand(transtring, remoteConnection);
                        comm.ExecuteNonQuery();
                        transtring = "";
                        remoteConnection.Close();
                        return "";
                    }
                    catch (NpgsqlException ex)
                    {
                        remoteConnection.Close();
                        OstaleFunkcije.SetInLog(transtring + "\r\n" + ex.ToString(), "65", "RemoteDB");
                        transtring = "";
                        return ex.ToString();
                    }
                default:
                    transtring += sql + "; ";
                    return "";
            }
        }
    }
}