namespace PCPOS.Util
{
    internal class classMail
    {
        public static void send_email(string em, string passw, string em_to, string tem, string sadrz)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            System.Net.NetworkCredential cred = new System.Net.NetworkCredential(em, passw);
            mail.To.Add(em_to);
            mail.Subject = tem;
            mail.From = new System.Net.Mail.MailAddress(em);
            mail.IsBodyHtml = true;
            mail.Body = sadrz;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            smtp.Send(mail);
        }
    }
}