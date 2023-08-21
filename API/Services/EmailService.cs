using System.Net;
using System.Net.Mail;
using API.Models.ViewModelSP;

namespace API.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;

        public EmailService()
        {
            _smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("AuroraGTAirline@gmail.com", "olubzwsqwelmwwhr")
            };
        }

        //public async Task SendEmailAsync(string to, string subject, string body)
        public async Task SendEmailAsync(string subject, string body)
        {
            using var mailMessage = new MailMessage("AuroraGTAirline@gmail.com", "AuroraGTAirline@gmail.com", subject, body)
            {
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mailMessage);
        }
        public async Task SendEmailAsync(string subject, string body,string ToEmail)
        {
            using var mailMessage = new MailMessage("AuroraGTAirline@gmail.com", ToEmail, subject, body)
            {
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
