using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace WebManagement.Common
{
    public class EmailSender
    {
        public void Send(string subject, string body, string destinyEmail)
        {
            var fromEmail = "dalhio.hana@gmail.com";
            var fromPassword = "ABCD";
            var fromHost = "smtp.gmail.com";
            var fromPort = 587;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(fromEmail);
            mail.To.Add(new MailAddress(destinyEmail));
            mail.Subject = subject;
            mail.Body = body;

            using (SmtpClient smtp = new SmtpClient(fromHost, fromPort))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);

            }


        }
    }
}
