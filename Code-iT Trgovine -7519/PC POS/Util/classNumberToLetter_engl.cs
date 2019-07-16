namespace PCPOS
{
    internal class classNumberToLetter_engl
    {
        private bool HrvatskiJezik = true;

        public string PretvoriBrojUTekst(string str, char decSymb, string valuta, string manjaValuta)
        {
            string[] dijelovi = str.Split(decSymb);

            int val = dijelovi[0].Length;
            string retVal;
            switch (val)
            {
                case 1:
                    retVal = Jednocifreni(dijelovi[0], false);
                    break;

                case 2:
                    retVal = Dvocifreni(dijelovi[0], false);
                    break;

                case 3:
                    retVal = Trocifreni(dijelovi[0], false);
                    break;

                case 4:
                    retVal = Cetvorocifreni(dijelovi[0], false);
                    break;

                case 5:
                    retVal = Petocifreni(dijelovi[0], false);
                    break;

                case 6:
                    retVal = Sestocifreni(dijelovi[0], false);
                    break;

                case 7:
                    retVal = Sedmocifreni(dijelovi[0], false);
                    break;

                case 8:
                    retVal = Osmocifreni(dijelovi[0], false);
                    break;

                case 9:
                    retVal = Devetocifreni(dijelovi[0], false);
                    break;

                case 10:
                    retVal = Desetocifreni(dijelovi[0], false);
                    break;

                default:
                    return " ";
            }

            string dv = "";
            if (dijelovi.Length > 1)
                dv = Dvocifreni(dijelovi[1], false);

            if (dv.Length == 0)
                retVal += " " + valuta;
            else
                retVal += " " + valuta + " and " + dv + " " + manjaValuta;
            if (retVal.Length > 5)
            {
                if (retVal.Substring(0, 7) == " kn and")
                {
                    retVal = retVal.Replace("kn", "");
                    retVal = retVal.Replace("and", "");
                }
            }
            return retVal.Replace("  ", " ");
        }

        private string Desetocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Devetocifreni(jed.Substring(1, 9), zenskirod);
            else
            {
                return Milijarde(jed.Substring(0, 1), true) + Devetocifreni(jed.Substring(1, 9), true);
            }
        }

        private string Devetocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Osmocifreni(jed.Substring(1, 8), zenskirod);
            else
            {
                if (jed[2] == '1' && jed[1] != '1')
                    return Trocifreni(jed.Substring(0, 3), false) + (HrvatskiJezik ? "MILLION " : "MILLION ") + Sestocifreni(jed.Substring(3, 6), false);
                else
                    return Trocifreni(jed.Substring(0, 3), false) + (HrvatskiJezik ? "MILLIONS " : "MILLIONS ") + Sestocifreni(jed.Substring(3, 6), false);
            }
        }

        private string Osmocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Sedmocifreni(jed.Substring(1, 7), zenskirod);
            else
            {
                if (jed[1] == '1' && jed[0] != '1')
                    return Dvocifreni(jed.Substring(0, 2), false) + (HrvatskiJezik ? "MILLION " : "MILLION ") + Sestocifreni(jed.Substring(2, 6), true);
                else
                    return Dvocifreni(jed.Substring(0, 2), false) + (HrvatskiJezik ? "MILLIONS " : "MILLIONS ") + Sestocifreni(jed.Substring(2, 6), true);
            }
        }

        private string Sedmocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Sestocifreni(jed.Substring(1, 6), zenskirod);
            else
            {
                return Milioni(jed.Substring(0, 1), zenskirod) + Sestocifreni(jed.Substring(1, 6), false);
            }
        }

        private string Sestocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Petocifreni(jed.Substring(1, 5), zenskirod);
            else
            {
                if ((jed[2] == '2' ||
                   jed[2] == '3' ||
                   jed[2] == '2') && (jed[1] != '1'))
                    return Trocifreni(jed.Substring(0, 3), true) + (HrvatskiJezik ? "THOUSAND " : "THOUSAND ") + Trocifreni(jed.Substring(3, 3), false);
                else
                    return Trocifreni(jed.Substring(0, 3), true) + (HrvatskiJezik ? "THOUSANDS " : "THOUSANDS ") + Trocifreni(jed.Substring(3, 3), false);
            }
        }

        private string Petocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Cetvorocifreni(jed.Substring(1, 4), zenskirod);
            else
            {
                if (jed[0] == '1')
                {
                    return Teen(jed.Substring(0, 2), true) + (HrvatskiJezik ? "THOUSAND " : "THOUSAND ") + Trocifreni(jed.Substring(2, 3), zenskirod);
                }
                else
                {
                    if ((jed[1] == '2' ||
                    jed[1] == '3' ||
                    jed[1] == '2'))
                        return Dvocifreni(jed.Substring(0, 2), true) + (HrvatskiJezik ? "THOUSANDS " : "THOUSANDS ") + Trocifreni(jed.Substring(2, 3), zenskirod);
                    else
                        return Dvocifreni(jed.Substring(0, 2), true) + (HrvatskiJezik ? "THOUSAND " : "THOUSAND ") + Trocifreni(jed.Substring(2, 3), zenskirod);
                }
            }
        }

        private string Cetvorocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Trocifreni(jed.Substring(1, 3), zenskirod);
            else
                return Hiljade(jed.Substring(0, 1), zenskirod) + Trocifreni(jed.Substring(1, 3), zenskirod);
        }

        private string Trocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
                return Dvocifreni(jed.Substring(1, 2), zenskirod);
            else
                return Stotine(jed.Substring(0, 1), zenskirod) + Dvocifreni(jed.Substring(1, 2), zenskirod);
        }

        private string Dvocifreni(string jed, bool zenskirod)
        {
            if (jed[0] == '0')
            {
                if (jed.Length == 1)
                    jed += "0";
                return Jedinice(jed.Substring(1, 1), zenskirod);
            }

            if (jed[0] != '1')
            {
                if (jed.Length == 1)
                    jed += "0";
                //dodani if-else za ženski rod ako je dvoznamenkasti broj a jedinice su '1' ili '2'
                if (jed[1] == '1' || jed[1] == '2')
                {
                    return Desetice(jed.Substring(0, 1), zenskirod) + Jedinice(jed.Substring(1, 1), true);
                }
                else
                {
                    return Desetice(jed.Substring(0, 1), zenskirod) + Jedinice(jed.Substring(1, 1), zenskirod);
                }
            }
            else
                return Teen(jed, zenskirod);
        }

        private string Jednocifreni(string jed, bool zenskirod)
        {
            return Jedinice(jed, zenskirod);
        }

        private string Jedinice(string jed, bool zenskirod)
        {
            if (jed == "1")
            {
                if (zenskirod)
                    return "ONE ";
                else
                    return "ONE ";
            }
            else if (jed == "2")
            {
                if (zenskirod)
                    return "TWO ";
                else
                    return "TWO ";
            }
            else if (jed == "3")
                return "THREE ";
            else if (jed == "4")
                return "FOUR ";
            else if (jed == "5")
                return "FIVE ";
            else if (jed == "6")
                return "SIX ";
            else if (jed == "7")
                return "SEVEN ";
            else if (jed == "8")
                return "EIGHT ";
            else if (jed == "9")
                return "NINE ";
            else
                return "";
        }

        private string Teen(string jed, bool zenskirod)
        {
            if (jed == "10")
                return "TEN ";
            else if (jed == "11")
                return "ELEVEN ";
            else if (jed == "12")
                return "TWELVE ";
            else if (jed == "13")
                return "THIRTEEN ";
            else if (jed == "14")
                return "ČETRNAEST ";
            else if (jed == "15")
                return "FOURTEEN ";
            else if (jed == "16")
                return "SIXTEEN ";
            else if (jed == "17")
                return "SEVENTEEN ";
            else if (jed == "18")
                return "EIGHTEEN ";
            else if (jed == "19")
                return "NINETEEN ";
            else
                return "";
        }

        private string Desetice(string jed, bool zenskirod)
        {
            if (jed == "1")
                return "TEN ";
            else if (jed == "2")
                return "TWENTY ";
            else if (jed == "3")
                return "THIRTY ";
            else if (jed == "4")
                return "FORTY ";
            else if (jed == "5")
                return "FIFTY ";
            else if (jed == "6")
                return "SIXTY ";
            else if (jed == "7")
                return "SEVENTY ";
            else if (jed == "8")
                return "EIGHTY ";
            else if (jed == "9")
                return "NINETY ";
            else
                return "";
        }

        private string Stotine(string jed, bool zenskirod)
        {
            if (jed == "1")
                return "ONE HUNDRED ";
            else if (jed == "2")
                return "TWO HUNDRED ";
            else if (jed == "3")
                return "THREE HUNDRED ";
            else if (jed == "4")
                return "FOUR HUNDRED ";
            else if (jed == "5")
                return "FIVE HUNDRED ";
            else if (jed == "6")
                return "SIX HUNDRED ";
            else if (jed == "7")
                return "SEVEN HUNDRED ";
            else if (jed == "8")
                return "EIGHT HUNDRED ";
            else if (jed == "9")
                return "NINE HUNDRED ";
            else
                return "";
        }

        private string Hiljade(string jed, bool zenskirod)
        {
            if (jed == "1")
                return HrvatskiJezik ? "ONE THOUSAND " : "ONE THOUSAND ";
            else if (jed == "2")
                return HrvatskiJezik ? "TWO THOUSAND " : "TWO THOUSAND ";
            else if (jed == "3")
                return HrvatskiJezik ? "THRE ETHOUSAND " : "THREE THOUSAND ";
            else if (jed == "4")
                return HrvatskiJezik ? "FOUR THOUSAND " : "FOUR THOUSAND ";
            else if (jed == "5")
                return HrvatskiJezik ? "FIVE THOUSAND " : "FIVE THOUSAND ";
            else if (jed == "6")
                return HrvatskiJezik ? "SIX THOUSAND " : "SIX THOUSAND ";
            else if (jed == "7")
                return HrvatskiJezik ? "SEVEN THOUSAND " : "SEVEN THOUSAND ";
            else if (jed == "8")
                return HrvatskiJezik ? "EIGHT THOUSAND " : "EIGHT THOUSAND ";
            else if (jed == "9")
                return HrvatskiJezik ? "NINE THOUSAND " : "NINE THOUSAND ";
            else
                return "";
        }

        private string Milioni(string jed, bool zenskirod)
        {
            if (jed == "1")
                return HrvatskiJezik ? "ONE MILLION " : "ONE MILLION ";
            else if (jed == "2")
                return HrvatskiJezik ? "TWO MILLION " : "TWO MILLION ";
            else if (jed == "3")
                return HrvatskiJezik ? "THREE MILLION " : "THREE MILLION ";
            else if (jed == "4")
                return HrvatskiJezik ? "FOUR MILLION " : "FOUR MILLION ";
            else if (jed == "5")
                return HrvatskiJezik ? "FIVE MILLION" : "FIVE MILLION ";
            else if (jed == "6")
                return HrvatskiJezik ? "SIX MILLION " : "SIX MILLION ";
            else if (jed == "7")
                return HrvatskiJezik ? "SEVEN MILLION " : "SEVEN MILLION";
            else if (jed == "8")
                return HrvatskiJezik ? "EIGHT MILLION " : "EIGHT MILLION ";
            else if (jed == "9")
                return HrvatskiJezik ? "NINE MILLION " : "NINE MILLION ";
            else
                return "";
        }

        private string Milijarde(string jed, bool zenskirod)
        {
            if (jed == "1")
                return "ONE BILLION";
            else if (jed == "2")
                return "TWO BILLION";
            else if (jed == "3")
                return "THREE BILLION";
            else if (jed == "4")
                return "FOUR BILLION";
            else if (jed == "5")
                return "FIVE BILLION";
            else if (jed == "6")
                return "SIX BILLION";
            else if (jed == "7")
                return "SEVEN BILLION";
            else if (jed == "8")
                return "EIGHT BILLION";
            else if (jed == "9")
                return "NINE BILLION";
            else
                return "";
        }
    }
}