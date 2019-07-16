using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmCreateRemoteDB : Form
    {
        private string AutoNumber = "";

        public frmCreateRemoteDB()
        {
            InitializeComponent();
        }

        private void frmCreateRemoteDB_Load(object sender, EventArgs e)
        {
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics c = e.Graphics;
            Brush bG = new LinearGradientBrush(new Rectangle(0, 0, Width, Height), Color.AliceBlue, Color.LightSlateGray, 250);
            c.FillRectangle(bG, 0, 0, Width, Height);
        }

        private void CreateRemoteDB_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DataSet dataSet = new DataSet();
            string sqlString = "Server = localhost; Port = 5432; User Id = postgres; Password = drazen2814mia; Database = postgres;";
            NpgsqlConnection remoteConnection = new NpgsqlConnection(sqlString);
            if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("CREATE DATABASE pc_pos_test" + DateTime.Now.Year.ToString() + " WITH OWNER = postgres ENCODING = 'UTF8' TABLESPACE = pg_default LC_COLLATE = 'Croatian_Croatia.1250' LC_CTYPE = 'Croatian_Croatia.1250' CONNECTION LIMIT = -1", remoteConnection);
            dataSet.Reset();
            da.Fill(dataSet);
            remoteConnection.Close();

            string sql = "";
            string br = "";
            string data_type = "";
            DataTable DT = classSQL.select_settings("SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE", "Table").Tables[0];
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                DataTable DTT = classSQL.select_settings("SELECT AUTOINC_INCREMENT,COLUMN_NAME,DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION FROM information_schema.columns WHERE TABLE_NAME ='" + DT.Rows[i]["TABLE_NAME"].ToString() + "'", "Table").Tables[0];
                sql += "CREATE TABLE " + DT.Rows[i]["TABLE_NAME"].ToString() + "(\n";

                for (int y = 0; y < DTT.Rows.Count; y++)
                {
                    if (DTT.Rows[y]["CHARACTER_MAXIMUM_LENGTH"].ToString() != "" && DTT.Rows[y]["DATA_TYPE"].ToString() != "ntext")
                    {
                        br = "(" + DTT.Rows[y]["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")";
                    }
                    else
                    {
                        br = "";
                    }

                    if (DTT.Rows[y]["DATA_TYPE"].ToString() == "int")
                    {
                        data_type = "integer";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "nvarchar")
                    {
                        data_type = "character varying";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "datetime")
                    {
                        data_type = "timestamp without time zone";
                    }
                    else if (DTT.Rows[y]["DATA_TYPE"].ToString() == "ntext")
                    {
                        data_type = "text";
                    }
                    else
                    {
                        data_type = DTT.Rows[y]["DATA_TYPE"].ToString();
                    }

                    if (DTT.Rows[y]["AUTOINC_INCREMENT"].ToString() == "1")
                    {
                        AutoNumber = DT.Rows[i]["COLUMN_NAME"].ToString();
                        data_type = "serial";
                    }

                    sql += DTT.Rows[y]["COLUMN_NAME"].ToString() + " " + data_type + br + ",\n";
                }

                if (DT.Rows[i]["COLUMN_NAME"].ToString() != "")
                {
                    sql += "CONSTRAINT " + DT.Rows[i]["TABLE_NAME"].ToString() + "_primary_key" + " PRIMARY KEY (" + DT.Rows[i]["COLUMN_NAME"].ToString() + "),\n";
                }

                sql = sql.Remove(sql.Length - 2, 2);
                sql += ")\n\n";

                //sqlString = "Server = localhost; Port = 5432; User Id = postgres; Password = drazen2814mia; Database = pc_pos" + DateTime.Now.Year.ToString() + ";";
                sqlString = SQL.claasConnectDatabase.GetRemoteConnectionString();
                remoteConnection = new NpgsqlConnection(sqlString);
                if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                da = new NpgsqlDataAdapter(sql, remoteConnection);
                dataSet.Reset();
                da.Fill(dataSet);
                remoteConnection.Close();
                sql = "";

                DataTable DTcompact = classSQL.select_settings("SELECT * FROM " + DT.Rows[i]["TABLE_NAME"].ToString(), DT.Rows[i]["TABLE_NAME"].ToString()).Tables[0];

                for (int r = 0; r < DTcompact.Rows.Count; r++)
                {
                    string columnName = "";
                    string values = "";
                    sql = "INSERT INTO " + DT.Rows[i]["TABLE_NAME"].ToString() + " ";

                    for (int ii = 0; ii < DTcompact.Columns.Count; ii++)
                    {
                        if (AutoNumber != DTcompact.Columns[ii].ToString())
                        {
                            columnName += DTcompact.Columns[ii].ToString() + ",";
                            values += "'" + DTcompact.Rows[r][DTcompact.Columns[ii]].ToString().Replace("'", "") + "'" + ",";
                            values = values.Replace("\"", "");
                            values = values.Replace("[", "");
                            values = values.Replace("]", "");
                            values = values.Replace("(", "");
                            values = values.Replace(")", "");
                            values = values.Replace("\\", "");
                        }
                        else
                        {
                            AutoNumber = "";
                        }
                    }

                    columnName = columnName.Remove(columnName.Length - 1, 1);
                    values = values.Remove(values.Length - 1, 1);
                    sql += " ( " + columnName + " ) " + " VALUES " + " ( " + values + " ) ";

                    remoteConnection = new NpgsqlConnection(sqlString);
                    if (remoteConnection.State.ToString() == "Closed") { remoteConnection.Open(); }
                    da = new NpgsqlDataAdapter(sql, remoteConnection);
                    dataSet.Reset();
                    da.Fill(dataSet);
                    remoteConnection.Close();
                    sql = "";
                }
            }

            MessageBox.Show("Spremljeno.");
        }
    }
}