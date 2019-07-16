using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class frmAutomatskaUsklada : Form
    {
        public frmAutomatskaUsklada()
        {
            InitializeComponent();
        }

        //DataTable dtPostavke;
        private DataSet dsAutomatskaUsklada;

        private DateTime datum_do;
        private int id_skladiste;

        private void frmAutomatskaUsklada_Load(object sender, EventArgs e)
        {
            try
            {
                if (Util.Korisno.GodinaKojaSeKoristiUbazi < DateTime.Now.Year)
                {
                    dtpDatumDo.Value = Convert.ToDateTime(Util.Korisno.GodinaKojaSeKoristiUbazi + "-12-31 23:59:59");
                }

                //dtPostavke = classSQL.select_settings("select * from postavke", "postavke").Tables[0];
                this.dgwUskladaItems.BorderStyle = BorderStyle.None;

                string sql = @"select *
from skladiste where upper(aktivnost) = 'DA';";
                DataSet dsSkladista = classSQL.select(sql, "skladiste");
                if (dsSkladista != null && dsSkladista.Tables.Count > 0 && dsSkladista.Tables[0] != null && dsSkladista.Tables[0].Rows.Count > 0)
                {
                    this.cmbSkladiste.DataSource = dsSkladista.Tables[0];
                    this.cmbSkladiste.DisplayMember = "skladiste";
                    this.cmbSkladiste.ValueMember = "id_skladiste";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUcitaj_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                progressBar1.MarqueeAnimationSpeed = 30;
                datum_do = dtpDatumDo.Value;
                id_skladiste = Convert.ToInt32(cmbSkladiste.SelectedValue);
                bgwAutomatskaUskladaUcitaj.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwUskladaItems.Rows.Count == 0)
                {
                    MessageBox.Show("Podaci nisu učitani.");
                    return;
                }

                DateTime datum_od = Convert.ToDateTime(classSQL.select("select coalesce( max(datum), '" + Util.Korisno.GodinaKojaSeKoristiUbazi + "-01-01 00:00:00') as datum from automatska_usklada where id_poslovnica = '" + Class.Postavke.id_default_ducan + "' and id_naplatni_uredaj = '" + Class.Postavke.id_maloprodaja_naplatni_uredaj + "' and id_skladiste = '" + id_skladiste + "';", "automatska_usklada").Tables[0].Rows[0]["datum"]);

                string sql = @"insert into automatska_usklada (broj, datum, id_zaposlenik, id_poslovnica, id_naplatni_uredaj, id_skladiste)
values (
(select coalesce(max(broj), 0) zbroj 1 as broj from automatska_usklada where id_poslovnica = '" + Class.Postavke.id_default_ducan + @"' and id_naplatni_uredaj = '" + Class.Postavke.id_maloprodaja_naplatni_uredaj + @"'),
'" + datum_do.ToString("yyyy-MM-dd HH:mm:ss") + @"',
'" + Properties.Settings.Default.id_zaposlenik + @"',
'" + Class.Postavke.id_default_ducan + @"',
'" + Class.Postavke.id_maloprodaja_naplatni_uredaj + @"',
'" + id_skladiste + @"'
);

select coalesce(max(broj), 0) as broj from automatska_usklada where id_poslovnica = '" + Class.Postavke.id_default_ducan + @"' and id_naplatni_uredaj = '" + Class.Postavke.id_maloprodaja_naplatni_uredaj + @"';";

                DataTable dtAutomatskaUsklada = classSQL.select(sql, "automatska_usklada").Tables[0];

                sql = @"
drop table if exists tempUlazIzlaz;
create temporary table tempUlazIzlaz as
select ulaz_izlaz_robe_financijski.skladiste, ulaz_izlaz_robe_financijski.ui, ulaz_izlaz_robe_financijski.sifra,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina), 0) as kolicinaUkp,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina * nbc), 0) as nbcUkp,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.vpc), 0) as vpcUkp,
coalesce(sum(kolicina * ((ulaz_izlaz_robe_financijski.vpc * (1 zbroj ulaz_izlaz_robe_financijski.porez / 100)) -
	((ulaz_izlaz_robe_financijski.vpc * (1 zbroj ulaz_izlaz_robe_financijski.porez / 100)) * (ulaz_izlaz_robe_financijski.rabat / 100)))), 0) as mpcUkp

from ulaz_izlaz_robe_financijski
left join roba on ulaz_izlaz_robe_financijski.sifra = roba.sifra
where skladiste = '" + id_skladiste + @"' and doc not in ('radni_nalog_skida_normative_prema_uslugi')
and roba.oduzmi = 'DA'
and ulaz_izlaz_robe_financijski.datum between '" + datum_od.ToString("yyyy-MM-dd HH:mm:ss") + @"' and '" + datum_do.ToString("yyyy-MM-dd HH:mm:ss") + @"'
group by ulaz_izlaz_robe_financijski.skladiste, ulaz_izlaz_robe_financijski.sifra, ulaz_izlaz_robe_financijski.ui
order by ulaz_izlaz_robe_financijski.skladiste asc,
ulaz_izlaz_robe_financijski.sifra asc,
ulaz_izlaz_robe_financijski.ui desc;

drop table if exists tempPrerada;
create temporary table tempPrerada as
select tU.skladiste, tU.sifra,
coalesce(tU.kolicinaUkp, 0) as KolicinaUlaz,

coalesce(tU.nbcUkp, 0) as NbcUlaz,
coalesce(tU.vpcUkp, 0) as VpcUlaz,
coalesce(tU.mpcUkp, 0) as MpcUlaz,

coalesce(tI.kolicinaUkp, 0) as KolicinaIzlaz,

coalesce(tI.nbcUkp, 0) as NbcIzlaz,
coalesce(tI.vpcUkp, 0) as VpcIzlaz,
coalesce(tI.mpcUkp, 0) as MpcIzlaz

from tempUlazIzlaz tU
left join tempUlazIzlaz as tI on tU.sifra = tI.sifra and tI.ui = 'izlaz'
where tU.ui = 'ulaz'

union

select tI.skladiste, tI.sifra,
coalesce(tU.kolicinaUkp, 0) as KolicinaUlaz,

coalesce(tU.nbcUkp, 0) as NbcUlaz,
coalesce(tU.vpcUkp, 0) as VpcUlaz,
coalesce(tU.mpcUkp, 0) as MpcUlaz,

coalesce(tI.kolicinaUkp, 0) as KolicinaIzlaz,

coalesce(tI.nbcUkp, 0) as NbcIzlaz,
coalesce(tI.vpcUkp, 0) as VpcIzlaz,
coalesce(tI.mpcUkp, 0) as MpcIzlaz

from tempUlazIzlaz tU
right join tempUlazIzlaz as tI on tU.sifra = tI.sifra and tU.ui = 'ulaz'
where tI.ui = 'izlaz';

-- drop table if exists tempIzracun;
-- create temporary table tempIzracun as

insert into automatska_usklada_stavke (broj, id_poslovnica, id_naplatni_uredaj, id_skladiste, sifra, kolicina_ulaz, nbc_ulaz, vpc_ulaz, mpc_ulaz, nbc_prosjecna, vpc_prosjecna, mpc_prosjecna, kolicina_izlaz, nbc_izlaz_izracun, vpc_izlaz_izracun, mpc_izlaz_izracun, nbc_izlaz, vpc_izlaz, mpc_izlaz)
(
    select
    '" + dtAutomatskaUsklada.Rows[0]["broj"] + @"',
    '" + Class.Postavke.id_default_ducan + @"',
    '" + Class.Postavke.id_maloprodaja_naplatni_uredaj + @"',
    skladiste, sifra, kolicinaUlaz,
    round(nbcUlaz, 4) as nbcUlaz,
    round(vpcUlaz, 4) as vpcUlaz,
    round(mpcUlaz, 4) as mpcUlaz,
    round((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end), 4) as nbcProsjecno,
    round((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end), 4) as vpcProsjecno,
    round((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end), 4) as mpcProsjecno,
    round(kolicinaIzlaz, 4) as kolicinaIzlaz,
    round((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as nbcIzlazIzracun,
    round((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as vpcIzlazIzracun,
    round((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as mpcIzlazIzracun,
    round(nbcIzlaz, 4) as nbcIzlaz, round(vpcIzlaz, 4) as vpcIzlaz, round(mpcIzlaz, 4) as mpcIzlaz
    from tempPrerada
    -- where kolicinaUlaz <> 0 or kolicinaIzlaz <> 0
    -- round(((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - nbcIzlaz, 4) <> 0
    -- or round(((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - vpcIzlaz, 4) <> 0
    -- or round(((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - mpcIzlaz, 4) <> 0
);";

                classSQL.select(sql, "automatska_usklada");
                MessageBox.Show("Spremljeno");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
        }

        private void cmbSkladiste_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var combo = sender as ComboBox;
                DataTable dtSkladiste = (DataTable)combo.DataSource;
                if (dtSkladiste != null)
                {
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.ControlLight), e.Bounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SystemColors.Menu), e.Bounds);
                    }

                    e.Graphics.DrawString(dtSkladiste.Rows[e.Index]["skladiste"].ToString(),
                                                  e.Font,
                                                  new SolidBrush(Color.Black),
                                                  new Point(e.Bounds.X, e.Bounds.Y));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwAutomatskaUskladaUcitaj_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DateTime datum_od = Convert.ToDateTime(classSQL.select("select coalesce( max(datum), '" + Util.Korisno.GodinaKojaSeKoristiUbazi + "-01-01 00:00:00') as datum from automatska_usklada where id_poslovnica = '" + Class.Postavke.id_default_ducan + "' and id_naplatni_uredaj = '" + Class.Postavke.id_maloprodaja_naplatni_uredaj + "' and id_skladiste = '" + id_skladiste + "';", "automatska_usklada").Tables[0].Rows[0]["datum"]);

                string sql = @"
drop table if exists tempUlazIzlaz;
create temporary table tempUlazIzlaz as
select ulaz_izlaz_robe_financijski.skladiste, ulaz_izlaz_robe_financijski.ui, ulaz_izlaz_robe_financijski.sifra,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina), 0) as kolicinaUkp,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina * nbc), 0) as nbcUkp,
coalesce(sum(ulaz_izlaz_robe_financijski.kolicina * ulaz_izlaz_robe_financijski.vpc), 0) as vpcUkp,
coalesce(sum(kolicina * ((ulaz_izlaz_robe_financijski.vpc * (1 zbroj ulaz_izlaz_robe_financijski.porez / 100)) -
	((ulaz_izlaz_robe_financijski.vpc * (1 zbroj ulaz_izlaz_robe_financijski.porez / 100)) * (ulaz_izlaz_robe_financijski.rabat / 100)))), 0) as mpcUkp

from ulaz_izlaz_robe_financijski
left join roba on ulaz_izlaz_robe_financijski.sifra = roba.sifra
where skladiste = '" + id_skladiste + @"' and doc not in ('radni_nalog_skida_normative_prema_uslugi')
and roba.oduzmi = 'DA'
and ulaz_izlaz_robe_financijski.datum between '" + datum_od.ToString("yyyy-MM-dd HH:mm:ss") + @"' and '" + datum_do.ToString("yyyy-MM-dd HH:mm:ss") + @"'
group by ulaz_izlaz_robe_financijski.skladiste, ulaz_izlaz_robe_financijski.sifra, ulaz_izlaz_robe_financijski.ui
order by ulaz_izlaz_robe_financijski.skladiste asc,
ulaz_izlaz_robe_financijski.sifra asc,
ulaz_izlaz_robe_financijski.ui desc;

drop table if exists tempPrerada;
create temporary table tempPrerada as
select tU.skladiste, tU.sifra,
coalesce(tU.kolicinaUkp, 0) as KolicinaUlaz,

coalesce(tU.nbcUkp, 0) as NbcUlaz,
coalesce(tU.vpcUkp, 0) as VpcUlaz,
coalesce(tU.mpcUkp, 0) as MpcUlaz,

coalesce(tI.kolicinaUkp, 0) as KolicinaIzlaz,

coalesce(tI.nbcUkp, 0) as NbcIzlaz,
coalesce(tI.vpcUkp, 0) as VpcIzlaz,
coalesce(tI.mpcUkp, 0) as MpcIzlaz

from tempUlazIzlaz tU
left join tempUlazIzlaz as tI on tU.sifra = tI.sifra and tI.ui = 'izlaz'
where tU.ui = 'ulaz'

union

select tI.skladiste, tI.sifra,
coalesce(tU.kolicinaUkp, 0) as KolicinaUlaz,

coalesce(tU.nbcUkp, 0) as NbcUlaz,
coalesce(tU.vpcUkp, 0) as VpcUlaz,
coalesce(tU.mpcUkp, 0) as MpcUlaz,

coalesce(tI.kolicinaUkp, 0) as KolicinaIzlaz,

coalesce(tI.nbcUkp, 0) as NbcIzlaz,
coalesce(tI.vpcUkp, 0) as VpcIzlaz,
coalesce(tI.mpcUkp, 0) as MpcIzlaz

from tempUlazIzlaz tU
right join tempUlazIzlaz as tI on tU.sifra = tI.sifra and tU.ui = 'ulaz'
where tI.ui = 'izlaz';

-- drop table if exists tempIzracun;
-- create temporary table tempIzracun as
select skladiste, sifra, kolicinaUlaz,
round(nbcUlaz, 4) as nbcUlaz,
round(vpcUlaz, 4) as vpcUlaz,
round(mpcUlaz, 4) as mpcUlaz,
round((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end), 4) as nbcProsjecno,
round((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end), 4) as vpcProsjecno,
round((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end), 4) as mpcProsjecno,
round(kolicinaIzlaz, 4) as kolicinaIzlaz,
round((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as nbcIzlazIzracun,
round((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as vpcIzlazIzracun,
round((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end) * kolicinaIzlaz, 4) as mpcIzlazIzracun,
round(nbcIzlaz, 4) as nbcIzlaz, round(vpcIzlaz, 4) as vpcIzlaz, round(mpcIzlaz, 4) as mpcIzlaz,
round(((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - nbcIzlaz, 4) as nbcRazlika,
round(((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - vpcIzlaz, 4) as vpcRazlika,
round(((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - mpcIzlaz, 4) as mpcRazlika
from tempPrerada
-- where kolicinaUlaz <> 0 or kolicinaIzlaz <> 0
-- round(((case when kolicinaUlaz = 0 then 0 else nbcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - nbcIzlaz, 4) <> 0
-- or round(((case when kolicinaUlaz = 0 then 0 else VpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - vpcIzlaz, 4) <> 0
-- or round(((case when kolicinaUlaz = 0 then 0 else MpcUlaz / kolicinaUlaz end) * kolicinaIzlaz) - mpcIzlaz, 4) <> 0
;";

                dsAutomatskaUsklada = classSQL.select(sql, "automatska_usklada");

                if (dsAutomatskaUsklada != null && dsAutomatskaUsklada.Tables.Count > 0 && dsAutomatskaUsklada.Tables[0] != null)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // Execute the following code on the GUI thread.
                        this.dgwUskladaItems.DataSource = dsAutomatskaUsklada.Tables[0];
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        // Execute the following code on the GUI thread.
                        this.dgwUskladaItems.DataSource = dsAutomatskaUsklada;
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwAutomatskaUskladaUcitaj_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.MarqueeAnimationSpeed = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bgwAutomatskaUskladaSpremi_DoWork(object sender, DoWorkEventArgs e)
        {
        }
    }
}