using System;
using System.Text;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmPovijestPromjena : Form
    {
        public frmPovijestPromjena()
        {
            InitializeComponent();
        }

        private void PovijestPromjena_Load(object sender, EventArgs e)
        {
            string razmakSZarezom = "   - ";
            string razmak = "     ";
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("U sljedećoj verziji će biti:");
            sb.AppendLine(razmakSZarezom + "kontni plan");
            sb.AppendLine(razmakSZarezom + "omogućena promjena skladišta kod uređivanja primki, izdatnica, ");
            sb.AppendLine(razmak + "povratnica...");
            sb.AppendLine(razmakSZarezom + "smjene - ispis na A4");

            sb.AppendLine("");

            sb.AppendLine("v1.45:");
            sb.AppendLine(razmakSZarezom + "FISKALIZACIJA FAKTURA");
            sb.AppendLine(razmakSZarezom + "izmjena forme i izvještaja za odjavu komisione robe (dodan mpc");
            sb.AppendLine(razmak + "i porez u izvještaju)");
            sb.AppendLine(razmakSZarezom + "dodan modul za servisni radni nalog");
            sb.AppendLine(razmakSZarezom + "dodana opcija za ubacivanje servisnog radnog naloga u fakturu");
            sb.AppendLine(razmakSZarezom + "dodana forma s poviješću promjena");
            sb.AppendLine(razmakSZarezom + "riješen bug kod postavljanja inventure");
            sb.AppendLine(razmakSZarezom + "omogućeno ažuriranje nefiskaliziranih avansa");
            sb.AppendLine(razmakSZarezom + "proširenje izvještaja za kalkulaciju");
            sb.AppendLine(razmakSZarezom + "računanje po valuti u kalkulaciji");
            sb.AppendLine(razmakSZarezom + "dodane provjere kod unosa kalkulacije");
            sb.AppendLine(razmakSZarezom + "implementirane smjene zaposlenika");
            sb.AppendLine(razmakSZarezom + "riješen bug kod načina plaćanja kod avansa");
            sb.AppendLine(razmakSZarezom + "riješen bug kod odabira slike za logo");
            sb.AppendLine(razmakSZarezom + "omogućen prikaz (na ekranu) ponuda, računa i faktura prije ");
            sb.AppendLine(razmak + "spremanja (tj. prije fiskalizacije)");
            sb.AppendLine(razmakSZarezom + "omogućeno ažuriranje ponuda s tečajevima");
            sb.AppendLine(razmakSZarezom + "omogućeno ažuriranje faktura s tečajevima");
            sb.AppendLine(razmakSZarezom + "riješen bug kod izvještaja po skladištima-ne množi s količinom");
            sb.AppendLine(razmakSZarezom + "omogućeno storniranje faktura");
            sb.AppendLine(razmakSZarezom + "dodan swift kod banke kod podataka o tvrtki");
            sb.AppendLine(razmakSZarezom + "riješen bug kod storniranja avansa iz prethodne godine");
            sb.AppendLine(razmakSZarezom + "omogućeno dodavanje kaucija");

            rtbPromjene.Text = sb.ToString();
        }
    }
}