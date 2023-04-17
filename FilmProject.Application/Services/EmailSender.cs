using FilmProject.Application.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FilmProject.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IWebHostEnvironment _env;
        public EmailSender(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "FilmProject45@gmail.com";
            var pw = "ProfITFilm45!";
            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            // Şablon dosyasını aç ve içeriği oku
            var templateFilePath = Path.Combine(_env.ContentRootPath, "Views/EmailTemplates", "VerificationMail.html");
            using (var reader = new StreamReader(templateFilePath))
            {
                var body = await reader.ReadToEndAsync();

                // Doğrulama bağlantısını şablon içindeki yere yerleştir
                body = body.Replace("{verificationLink}", htmlMessage);

                // E-posta gönderimi
                await client.SendMailAsync(
                    new MailMessage(
                            from: mail,
                            to: email,
                            subject,
                            body
                        )
                    {
                        IsBodyHtml = true
                    }
                );
            }
        }
    }
}
