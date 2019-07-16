using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace PCPOS
{
    internal class SendMail
    {
        public void Send_E_Mail()
        {
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient("localhost", 587);

            try
            {
                m.From = new MailAddress("drazen0001@gmail.com", "Display name");
                m.To.Add(new MailAddress("drazen0001@gmail.com", "Display name To"));
                //m.CC.Add(new MailAddress("CC@yahoo.com", "Display name CC"));
                //similarly BCC
                m.Subject = "Test1";
                m.IsBodyHtml = true;
                m.Body = " This is a Test Mail";

                FileStream fs = new FileStream("D:\\test.pdf",
                                   FileMode.Open, FileAccess.Read);
                Attachment a = new Attachment(fs, "test.pdf",
                                   MediaTypeNames.Application.Octet);
                m.Attachments.Add(a);

                string str = "<html><body><h1>Picture</h1><br/><img src=\"cid:image1\"></body></html>";
                AlternateView av = AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
                LinkedResource lr = new LinkedResource("D:\\vip.jpg", MediaTypeNames.Image.Jpeg);
                lr.ContentId = "image1";
                av.LinkedResources.Add(lr);
                m.AlternateViews.Add(av);

                sc.Host = "smtp.gmail.com";
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.Credentials = new System.Net.NetworkCredential("drazen0001@gmail.com", "");
                sc.EnableSsl = true;
                sc.Send(m);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}