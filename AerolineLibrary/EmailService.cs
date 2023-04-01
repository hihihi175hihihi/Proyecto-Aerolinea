using System.Net.Mail;
using System.Net;

namespace AerolineLibrary
{
    public static class EmailService
    {
        public static async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            var response = true;
            var _smtpClient = new SmtpClient
            {
                Host = "smpt.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("AuroraGTAirline@gmail.com", "olubzwsqwelmwwhr")
            };
            using var mailMessage = new MailMessage("AuroraGTAirline@gmail.com", to, subject, body)
            {
                IsBodyHtml = true
            };
            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }
    }
}
