namespace PCPOS
{
    internal class ProvjeraPostGressFunkcija
    {
        #region FUNKCIJA "plpgsql" PROVJERAVA NABAVNE CIJENE

        /// <summary>
        /// FUNKCIJA PROVJERAVA NABAVNE CIJENE I VRAČA PREMA KRITERIJIMA
        /// </summary>
        public void ProvjeraDaliPostojiFunkcijaProvjeraNabavnihCijena()
        {
            string sqlFunkNBC = @"CREATE OR REPLACE FUNCTION ProvjeraNabavneCijene(_sifra character varying, _datum timestamp without time zone,_id_skladiste integer) RETURNS decimal AS $$
	                        DECLARE
	                        NBC_kalk NUMERIC;
	                        NBC_radni_nalog NUMERIC;
	                        NBC_roba NUMERIC;
	                        NBC_poc NUMERIC;
                                BEGIN

                                /*Provjera kroz kalkulaciju*/
                                NBC_kalk:=(SELECT
		                        ROUND(COALESCE((kalkulacija_stavke.fak_cijena-
		                        (kalkulacija_stavke.fak_cijena*
		                        CAST(REPLACE(kalkulacija_stavke.rabat,',','.') AS numeric)/100)),0)
                                + COALESCE((kalkulacija_stavke.prijevoz),0)
                                ,4) AS cijena
		                        FROM kalkulacija_stavke
		                        LEFT JOIN kalkulacija ON kalkulacija.broj = kalkulacija_stavke.broj AND kalkulacija.id_skladiste = kalkulacija_stavke.id_skladiste
		                        WHERE kalkulacija_stavke.sifra=_sifra AND kalkulacija.datum<=_datum AND kalkulacija.id_skladiste=_id_skladiste
		                        ORDER BY kalkulacija.datum DESC LIMIT 1
	                        );

	                        /*Provjera kroz pocetno*/
                                NBC_poc:=(SELECT
		                        nbc AS cijena
		                        FROM pocetno
		                        WHERE pocetno.sifra=_sifra AND pocetno.id_skladiste=_id_skladiste
		                        LIMIT 1
	                        );

	                        /*Provjera kroz radni nalog*/
	                        NBC_radni_nalog:=(
		                        SELECT ROUND(COALESCE((SUM((CAST(roba_prodaja.nc AS numeric)*CAST(REPLACE(normativi_stavke.kolicina,',','.') AS numeric))*CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') AS numeric)))/

		                        (SELECT SUM(CAST(REPLACE(radni_nalog_stavke.kolicina,',','.') AS numeric))
		                        FROM radni_nalog_stavke
		                        LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
		                        WHERE radni_nalog_stavke.sifra_robe=_sifra
		                        AND radni_nalog.datum_naloga<=_datum
		                        AND radni_nalog_stavke.id_skladiste=_id_skladiste)

		                        ,0),4) as cijena
		                        FROM radni_nalog_stavke
		                        LEFT JOIN radni_nalog ON radni_nalog.broj_naloga = radni_nalog_stavke.broj_naloga
		                        LEFT JOIN normativi ON normativi.sifra_artikla=radni_nalog_stavke.sifra_robe
		                        LEFT JOIN normativi_stavke ON normativi_stavke.broj_normativa=normativi.broj_normativa
		                        LEFT JOIN roba_prodaja ON roba_prodaja.sifra=normativi_stavke.sifra_robe AND roba_prodaja.id_skladiste=normativi_stavke.id_skladiste
		                        WHERE radni_nalog_stavke.sifra_robe=_sifra AND radni_nalog.datum_naloga<=_datum AND radni_nalog_stavke.id_skladiste=_id_skladiste
	                        );

	                        /*Provjera kroz robu*/
	                        NBC_roba:=(
		                        SELECT COALESCE(CAST(nc AS NUMERIC),0) FROM roba WHERE sifra=_sifra LIMIT 1
	                        );

	                        RETURN (
		                        CASE
			                    WHEN NBC_roba<>0 AND NBC_roba is not null THEN NBC_roba
			                    WHEN NBC_kalk<>0 AND NBC_kalk is not null THEN NBC_kalk
						        WHEN NBC_poc<>0 AND NBC_poc is not null THEN NBC_poc
			                    WHEN NBC_radni_nalog<>0 AND NBC_radni_nalog is not null THEN NBC_radni_nalog
			                    ELSE 0
			                    END
		                        );
                                END;
                        $$ LANGUAGE plpgsql";
            classSQL.insert(sqlFunkNBC);
        }

        #endregion FUNKCIJA "plpgsql" PROVJERAVA NABAVNE CIJENE
    }
}