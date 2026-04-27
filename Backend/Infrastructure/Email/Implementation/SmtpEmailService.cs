using LuminiSchool.Infrastructure.Email.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System;

namespace LuminiSchool.Infrastructure.Email.Implementation
{
    public class SmtpEmailService : IEmailService
    {
        private readonly IConfiguration _cfg;
        private readonly ILogger<SmtpEmailService> _logger;

        public SmtpEmailService(IConfiguration cfg, ILogger<SmtpEmailService> logger)
        {
            _cfg    = cfg;
            _logger = logger;
        }

        public async Task SendTemporaryPasswordAsync(string toEmail, string fullName, string temporaryPassword)
        {
            var html = $@"
                <div style='font-family:sans-serif;max-width:480px;margin:auto'>
                  <h2 style='color:#1a56db'>Bienvenido/a a Lumini School</h2>
                  <p>Hola <strong>{fullName}</strong>,</p>
                  <p>Tu cuenta ha sido creada. Usa la siguiente contraseña temporal para iniciar sesión:</p>
                  <div style='background:#f3f4f6;border-radius:8px;padding:16px;font-size:22px;
                              font-weight:bold;letter-spacing:2px;text-align:center'>
                    {temporaryPassword}
                  </div>
                  <p style='color:#ef4444;margin-top:16px'>
                    ⚠️ Deberás cambiarla obligatoriamente en tu primer inicio de sesión.
                  </p>
                  <hr/>
                  <p style='font-size:12px;color:#6b7280'>Este correo fue generado automáticamente. No respondas a este mensaje.</p>
                </div>";

            await SendEmailAsync(toEmail, "Tu cuenta en Lumini School - Contraseña temporal", html);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                var host     = _cfg["Email:Host"]!;
                var port     = int.Parse(_cfg["Email:Port"] ?? "587");
                var username = _cfg["Email:Username"]!;
                var password = _cfg["Email:Password"]!;
                var from     = _cfg["Email:From"]!;

                using var client = new SmtpClient(host, port)
                {
                    Credentials  = new NetworkCredential(username, password),
                    EnableSsl    = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var mail = new MailMessage
                {
                    From       = new MailAddress(from, "Lumini School"),
                    Subject    = subject,
                    Body       = htmlBody,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                await client.SendMailAsync(mail);
                _logger.LogInformation("Email enviado a {Email}", toEmail);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error enviando email a {Email}", toEmail);
                throw;
            }
        }
    }
}
