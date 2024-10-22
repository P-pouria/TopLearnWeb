using System;
using MailKit.Net.Smtp;
using MimeKit;

namespace TopLearn.Core.Senders
{
    public class SendEmail
    {
        public static void Send(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Your App Name", "no-reply@yourdomain.com"));

            message.To.Add(new MailboxAddress("", to));

            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body 
            };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("your-smtp-server.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("no-reply@yourdomain.com", "yourpassword");

                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}
