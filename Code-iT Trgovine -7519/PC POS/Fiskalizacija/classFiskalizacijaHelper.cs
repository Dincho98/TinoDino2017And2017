namespace PCPOS.Fiskalizacija
{
    internal class classFiskalizacijaHelper
    {
        //void FiskalizirajFakturu(DataTable DTsend, DateTime datum, int brojFakture)
        //{
        //    DataSet DSkolicina = new DataSet();

        //    dodajKoloneDTpdv();
        //    DTpdv.Clear();

        //    double mnozeno = 1;
        //    double osnovicaStavka, pdvStavka;
        //    double osnovicaSve = 0;
        //    double pdvSve = 0;
        //    double povratnaNaknadaSve = 0;
        //    double rabatSve = 0;
        //    double kolNaknada;
        //    double povratnaNaknada;
        //    DataTable DTtemp;
        //    string sifra = "";
        //    string kol = "";

        //    for (int i = 0; i < dgw.Rows.Count; i++)
        //    {
        //        //ovo zakomentirano porez na potrošnju ne treba kod maloprodaje (?)
        //        double kolicina = Convert.ToDouble(dg(i, "kolicina").ToString());
        //        mnozeno = kolicina >= 0 ? 1 : -1;
        //        //double PP = Convert.ToDouble(DTstavke.Rows[i]["porez_potrosnja"].ToString());
        //        double PDV = Convert.ToDouble(dg(i, "porez").ToString());
        //        double VPC = Convert.ToDouble(dg(i, "vpc").ToString());
        //        double rabat = Convert.ToDouble(dg(i, "rabat").ToString());
        //        povratnaNaknada = Convert.ToDouble(dg(i, "povratna_naknada"));

        //        //double povratnaNaknada;
        //        ////mora biti tak jer prije nije postojala povratna naknada!
        //        //try
        //        //{
        //        //    povratnaNaknada = Convert.ToDouble(row["povratna_naknada"].ToString());
        //        //}
        //        //catch
        //        //{
        //        //    povratnaNaknada = 0;
        //        //}

        //        //double cijena = ((VPC * (PP + PDV) / 100) + VPC);
        //        double cijena = Math.Round(VPC * PDV / 100 + VPC - 0.0000001, 2);
        //        //double cijena = Math.Round(VPC * PDV / 100 + VPC, 2);
        //        double mpc = cijena * kolicina * (1 - rabat / 100);
        //        mpc = Convert.ToDouble(mpc.ToString("#0.00"));

        //        rabatSve += cijena * kolicina - mpc;

        //        //Ovaj kod dobiva PDV
        //        //double PreracunataStopaPDV = Convert.ToDouble((100 * PDV) / (100 + PDV + PP));
        //        double PreracunataStopaPDV = (100 * PDV) / (100 + PDV);

        //        //Ovaj kod dobiva porez na potrošnju
        //        //double PreracunataStopaPorezNaPotrosnju = Convert.ToDouble((100 * PP) / (100 + PDV + PP));
        //        double PreracunataStopaPorezNaPotrosnju = 100 / (100 + PDV);

        //        //treba smanjiti za iznos povratne naknade

        //        povratnaNaknada *= mnozeno;
        //        osnovicaStavka = (mpc - povratnaNaknada) / (1 + PDV / 100);
        //        pdvStavka = ((mpc - povratnaNaknada) * PreracunataStopaPDV) / 100;

        //        dodajPDV(PDV, osnovicaStavka);

        //        osnovicaSve += osnovicaStavka;

        //        pdvSve += pdvStavka;

        //        povratnaNaknadaSve += povratnaNaknada;
        //    }

        //    //--------------------------------------------------------
        //    //fiskalizacija

        //    DataTable DTOstaliPor = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTOstaliPor();
        //    DataTable DTnaknade = PosPrint.classPosPrintMaloprodaja2.dodajKoloneDTnaknade();

        //    if (povratnaNaknadaSve != 0)
        //    {
        //        RowPdv = DTnaknade.NewRow();
        //        RowPdv["iznos"] = povratnaNaknadaSve.ToString("0.00");
        //        RowPdv["naziv"] = "Povratna naknada";
        //        DTnaknade.Rows.Add(RowPdv);
        //    }

        //    double porezPotrosnjaSve = 0;

        //    string[] porezNaPotrosnju = setPorezNaPotrosnju();
        //    porezNaPotrosnju[0] = DTpostavke.Rows[0]["porez_potrosnja"].ToString();
        //    porezNaPotrosnju[1] = Convert.ToString(osnovicaSve);
        //    porezNaPotrosnju[2] = Convert.ToString(porezPotrosnjaSve);

        //    string iznososlobpdv = "";
        //    string iznos_marza = "";

        //    string np;
        //    switch (cbNacinPlacanja.Text.ToLower())
        //    {
        //        case "gotovina":
        //            np = "G";
        //            break;
        //        case "novčanice":
        //            np = "G";
        //            break;
        //        case "novčanica":
        //            np = "G";
        //            break;
        //        case "kartica":
        //            np = "K";
        //            break;
        //        case "kartice":
        //            np = "K";
        //            break;
        //        case "virman":
        //            np = "T";
        //            break;
        //        case "transakcijski račun":
        //            np = "T";
        //            break;
        //        default:
        //            np = "O";
        //            break;
        //    }

        //    if (cbNacinPlacanja.Text.ToLower().Contains("transakcijski"))
        //    {
        //        np = "T";
        //    }

        //    if (cbNacinPlacanja.Text.ToLower().Contains("kartic"))
        //    {
        //        np = "K";
        //    }

        //    if (cbNacinPlacanja.Text.ToLower().Contains("novčan"))
        //    {
        //        np = "G";
        //    }

        //    bool pdv = false;
        //    if (DTpostavke.Rows[0]["sustav_pdv"].ToString() == "1")
        //    {
        //        pdv = true;
        //    }

        //    string oib = DToib.Rows.Count > 0 ? DToib.Rows[0][0].ToString() : "";

        //    string[] fiskalizacija = new string[3];

        //    try
        //    {
        //        fiskalizacija = Fiskalizacija.classFiskalizacija.Fiskalizacija(
        //            DTtvrtka.Rows[0]["oib"].ToString(),
        //            oib,
        //            datum,
        //            pdv,
        //            brojFakture,
        //            DTpdv,
        //            porezNaPotrosnju,
        //            DTOstaliPor,
        //            iznososlobpdv,
        //            iznos_marza,
        //            DTnaknade,
        //            Convert.ToDecimal(u),//.ToString().Replace(",", ".")
        //            np,
        //            false,
        //            "faktura"
        //            );
        //    }
        //    catch
        //    {
        //        fiskalizacija = new string[3];
        //        fiskalizacija[0] = "";
        //        fiskalizacija[1] = "";
        //        fiskalizacija[2] = "";
        //    }

        //    //ažuriraj fakturu sa zki i jir
        //    string sql = "UPDATE fakture SET zki = '" + fiskalizacija[1] + "', jir='" + fiskalizacija[0] + "'" +
        //        " WHERE broj_fakture='" + ttxBrojFakture.Text + "'";
        //    provjera_sql(classSQL.update(sql));
        //}
    }
}