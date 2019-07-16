using System;
using System.Globalization;
using System.Windows.Forms;

namespace PCPOS.Util
{
    public static class FormatBroja
    {
        public static void Provjeri()
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            //NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            // Displays a value with the default separator (".").

            //Console.WriteLine(myInt.ToString("N", nfi));

            //// Displays the same value with a blank as the separator.
            //nfi.NumberDecimalSeparator = " ";
            //Console.WriteLine(myInt.ToString("N", nfi));
            //var seps = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
            //    .Select(ci => ci.NumberFormat.NumberDecimalSeparator)
            //    .Distinct()
            //    .ToList();

            //CultureInfo.CurrentUICulture.NumberFormat.NumberGroupSeparator.ToString()

            //if (CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString() == ",")
            //{
            //    MessageBox.Show("Separator je ',' zarez");
            //}
            //else if (CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString() == ".")
            //{
            //    MessageBox.Show("Separator je '.' točka");
            //}
            //else
            //{
            //    MessageBox.Show("Separator je '" + CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString() + "' neki drugi");
            //}

            if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString() == ",")
            {
                //MessageBox.Show("Separator je ',' zarez");
            }
            else if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString() == ".")
            {
                MessageBox.Show("Separator za decimalne brojeve je točka '.'. U regionalnim postavkama u control panelu " +
                    "postavite zarez za decimalni separator i točku za grupirajući separator jer bi moglo doći " +
                    "do konflikta prilikom konverzije." + Environment.NewLine + " Ako postoje nekakve nejasnoće, " +
                    "obratite se službi za korisnike.", "Upozorenje!");
            }
            else
            {
                //MessageBox.Show("Separator je '" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString() + "' neki drugi");
            }
        }
    }
}