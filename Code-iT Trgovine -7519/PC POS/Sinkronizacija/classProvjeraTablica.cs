using System.Data;

namespace PCPOS.Sinkronizacija
{
    internal class classProvjeraTablica
    {
        public static void Provjera()
        {
            DataTable DTroba = classSQL.select("SELECT * FROM roba LIMIT 1", "roba").Tables[0];
            ProvjeraRoba(DTroba);
        }

        public static void ProvjeraRoba(DataTable DT)
        {
            if (DT.Columns["opis"] == null)
            {
                classSQL.insert("ALTER TABLE roba ADD COLUMN opis text;");
                classSQL.insert("ALTER TABLE roba ADD COLUMN brand character varying;");
                classSQL.insert("ALTER TABLE roba ADD COLUMN jamstvo int;");
                classSQL.insert("ALTER TABLE roba ADD COLUMN akcija smallint;");
                classSQL.insert("ALTER TABLE roba ADD COLUMN link_za_slike character varying;");
                classSQL.insert("ALTER TABLE podgrupa ADD COLUMN id_grupa int;");
                classSQL.insert("ALTER TABLE grupa DROP COLUMN podgrupa;");
                classSQL.insert("ALTER TABLE grupa DROP COLUMN id_podgrupa;");
                classSQL.insert("ALTER TABLE roba ALTER COLUMN naziv TYPE character varying USING naziv::character varying;");
                classSQL.insert("ALTER TABLE roba ALTER COLUMN jm TYPE character varying(30) USING jm::character varying(30);");
                classSQL.insert("ALTER TABLE roba ALTER COLUMN sifra TYPE character varying USING sifra::character varying;");
                classSQL.insert("ALTER TABLE roba ALTER COLUMN jm TYPE character varying(30) USING jm::character varying(30);");
                classSQL.insert("ALTER TABLE roba ALTER COLUMN jm TYPE character varying(30) USING jm::character varying(30);");
                classSQL.insert("INSERT INTO podgrupa (id_podgrupa,naziv,id_grupa) VALUES ('1','Bez podgrupe','1');");
            }
        }
    }
}