using System.Net;
using System.Net.Mail;

namespace Dream.API.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = "DarvyXe@gmail.com";
        private string _mailFrom = "log@DarvyXe.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail From {_mailFrom} To {_mailTo} , "
                              + $"With {nameof(LocalMailService)} , ");

            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }

        public static void Email(string subject, string htmlString, string to)
        {
            try
            {
                string _mailFrom = "log@Toplearn.com";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_mailFrom);
                message.To.Add(new MailAddress(to));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }
    }
}
