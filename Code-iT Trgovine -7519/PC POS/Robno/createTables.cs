using System.Data;

namespace PCPOS.Robno
{
    internal class createTablesRobno
    {
        public void create()
        {
            DataTable DTremote = classSQL.select("select table_name from information_schema.tables where TABLE_TYPE <> 'VIEW'", "Table").Tables[0];

            if (DTremote.Rows.Count > 0)
            {
                DataRow[] dataROW = DTremote.Select("table_name = 'primka'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE primka" +
                        "(broj bigint NOT NULL," +
                        "id_partner bigint," +
                        "originalni_dokument character varying(100)," +
                        "id_izradio integer," +
                        "datum timestamp without time zone," +
                        "napomena text," +
                        "id_skladiste integer," +
                        "id_primka serial NOT NULL," +
                        "godina character varying(6)," +
                        "id_mjesto integer," +
                        "CONSTRAINT primka_pkey PRIMARY KEY (id_primka))";
                    classSQL.select(sql, "primka");
                }

                dataROW = DTremote.Select("table_name = 'primka_stavke'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE primka_stavke" +
                        "(sifra character varying(25)," +
                        "vpc numeric," +
                        "mpc numeric," +
                        "rabat character varying(15)," +
                        "broj bigint," +
                        "kolicina character(15)," +
                        "nbc numeric," +
                        "id_stavka serial NOT NULL," +
                        "pdv character varying(5)," +
                        "ukupno numeric," +
                        "iznos numeric," +
                        "id_primka integer NOT NULL," +
                        "CONSTRAINT primka_stavke_pkey PRIMARY KEY (id_stavka));";
                    classSQL.select(sql, "primka_stavke");
                }

                dataROW = DTremote.Select("table_name = 'izdatnica'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE izdatnica" +
                        "(broj bigint NOT NULL," +
                        "id_partner bigint," +
                        "originalni_dokument character varying(100)," +
                        "id_izradio integer," +
                        "datum timestamp without time zone," +
                        "napomena text," +
                        "id_skladiste integer," +
                        "id_izdatnica serial NOT NULL," +
                        "godina character varying(6)," +
                        "id_mjesto integer," +
                        "CONSTRAINT izdatnica_pkey PRIMARY KEY (id_izdatnica))";
                    classSQL.select(sql, "izdatnica");
                }

                dataROW = DTremote.Select("table_name = 'izdatnica_stavke'");
                if (dataROW.Length == 0)
                {
                    string sql = "CREATE TABLE izdatnica_stavke" +
                        "(sifra character varying(25)," +
                        "vpc numeric," +
                        "mpc numeric," +
                        "rabat character varying(15)," +
                        "broj bigint," +
                        "kolicina character(15)," +
                        "nbc numeric," +
                        "id_stavka serial NOT NULL," +
                        "pdv character varying(5)," +
                        "id_izdatnica integer NOT NULL," +
                        "ukupno numeric," +
                        "iznos numeric," +
                        "CONSTRAINT izdatnica_stavke_pkey PRIMARY KEY (id_stavka));";
                    classSQL.select(sql, "izdatnica_stavke");
                }
            }
        }
    }
}