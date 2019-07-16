using System;
using System.IO;
using System.Management;
using System.Windows.Forms;

namespace PCPOS
{
    public partial class DiskSpace : Form
    {
        public DiskSpace()
        {
            InitializeComponent();
        }

        private void DiskSpace_Load(object sender, EventArgs e)
        {
            ReadSetting();
            LoadChars();
            SendEmail();
        }

        private string email;
        private string drive1;
        private string drive2;
        private double size;
        private double size1;
        private double freeSpace;
        private double freeSpace1;
        private double usedspace;
        private double usedspace1;

        public void LoadChars()
        {
            try
            {
                //drive1 and drive2 are the drive letter from the disk
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive1.Substring(0, 2) + "\"");
                disk.Get();
                ManagementObject disk1 = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive2.Substring(0, 2) + "\"");
                disk1.Get();
                //Get the size
                size = double.Parse(disk["Size"].ToString());
                //convert the size from bytes to GB
                size = Math.Round(size / 1000000000, 2);
                //Get the freeSpace
                freeSpace = double.Parse(disk["FreeSpace"].ToString());
                //convert the size from bytes to GB
                freeSpace = Math.Round(freeSpace / 1000000000, 2);
                //Get the usedspace
                usedspace = (size - freeSpace);

                //this.chart1.Palette = ChartColorPalette.SeaGreen;

                //// Set title.
                //this.chart1.Titles.Clear();
                //this.chart1.Titles.Add("Disk Information");
                //Series series = new Series();
                //// Add series.
                //chart1.Series.Clear();
                //chart1.Legends[0].Alignment = StringAlignment.Center;
                //chart1.Palette = ChartColorPalette.Bright;
                //chart1.BackColor = Color.AliceBlue;
                ////Chart setting
                //chart1.ChartAreas[0].BackColor = Color.Transparent;
                //Series series1 = new Series
                //{
                //    Name = "series1",
                //    IsVisibleInLegend = true,
                //    Color = System.Drawing.Color.Green,
                //    ChartType = SeriesChartType.Pie
                //};
                //series1.Font = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold);

                //chart1.BorderlineColor = Color.Aquamarine;
                //chart1.Series.Add(series1);
                //series1.Points.Add(freeSpace);
                //series1.Points.Add(usedspace);
                //var p1 = series1.Points[0];
                //p1.AxisLabel = freeSpace.ToString();
                //p1.LegendText = "Free Space";
                //var p2 = series1.Points[1];
                //p2.AxisLabel = usedspace.ToString();
                //p2.LegendText = "Used Space";
                //chart1.Invalidate();

                ////-------------------------------------------------------------------------
                //size1 = double.Parse(disk1["Size"].ToString());
                //size1 = Math.Round(size1 / 1000000000, 2);
                //freeSpace1 = double.Parse(disk1["FreeSpace"].ToString());
                //freeSpace1 = Math.Round(freeSpace1 / 1000000000, 2);
                //usedspace1 = (size1 - freeSpace1);

                //this.chart2.Palette = ChartColorPalette.SeaGreen;

                //// Set title.
                //this.chart2.Titles.Clear();
                //this.chart2.Titles.Add("Disk Information");

                //// Add series.
                //chart2.Series.Clear();

                //chart2.Palette = ChartColorPalette.Bright;
                //chart2.BackColor = Color.AliceBlue;
                //chart2.Legends[0].Alignment = StringAlignment.Center;
                //chart2.ChartAreas[0].BackColor = Color.Transparent;
                //Series series2 = new Series
                //{
                //    Name = "series2",
                //    IsVisibleInLegend = true,
                //    Color = System.Drawing.Color.Green,
                //    ChartType = SeriesChartType.Pie
                //};
                //series2.Font = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold);

                //chart2.BorderlineColor = Color.Aquamarine;
                //chart2.Series.Add(series2);
                //series2.Points.Add(freeSpace1);
                //series2.Points.Add(usedspace1);
                //var p3 = series2.Points[0];
                //p3.AxisLabel = freeSpace1.ToString();
                //p3.LegendText = "Free Space";
                //var p4 = series2.Points[1];
                //p4.AxisLabel = usedspace1.ToString();
                //p4.LegendText = "Used Space";
                //chart2.Invalidate();

                //label2.Text = size.ToString();
                //label5.Text = size1.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Attention!!", "One of your disk have been removed from your PC /nLoading Default disks...", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                File.Delete("setting.txt");
                StreamWriter sw = new StreamWriter(@"setting.txt", true);
                string lines = email + "-" + @"C:\" + "-" + @"C:\";

                // Write the string to a file.

                sw.WriteLine(lines);

                sw.Close();
            }
        }

        public void ReadSetting()
        {
            StreamReader tr = new StreamReader("setting.txt");
            char[] separator = { '-' };
            string[] lines = tr.ReadLine().Split(separator);
            tr.Close();
            email = lines[0];
            drive1 = lines[1];
            drive2 = lines[2];
            //labeldrive1.Text = drive1;
            //labeldrive2.Text = drive2;
        }

        public void SendEmail()
        {
            //if ((freeSpace * 100 / size) < 15)
            //{
            //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //    message.To.Add(email);
            //    message.Subject = "Alert!!! Disk Almost full";
            //    message.From = new System.Net.Mail.MailAddress("youremail@gmail.com");
            //    message.Body = "Your hard disk with Letter: " + drive1 + " is almost full, please replace the disk or cleanup";
            //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            //    {
            //        Credentials = new NetworkCredential("youremail@gmail.com", "marcopolo860425"),
            //        EnableSsl = true
            //    };

            //    client.Send(message);
            //}
            //if ((freeSpace1 * 100 / size1) < 15)
            //{
            //    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //    message.To.Add(email);
            //    message.Subject = "Alert!!! Disk Almost full";
            //    message.From = new System.Net.Mail.MailAddress("youremail@gmail.com");
            //    message.Body = "Your hard disk with Letter: " + drive2 + " is almost full, please replace the disk or cleanup";
            //    SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            //    {
            //        Credentials = new NetworkCredential("youremail@gmail.com", "password"),
            //        EnableSsl = true
            //    };

            //    client.Send(message);
            //}
        }
    }
}