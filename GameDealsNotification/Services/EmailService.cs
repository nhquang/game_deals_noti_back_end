using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using GameDealsNotification.Configurations;
using GameDealsNotification.Models;
using GameDealsNotification.Services.Interfaces;
using GameDealsNotification.Utilities;
using Microsoft.Extensions.Options;

namespace GameDealsNotification.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<Settings> _options;

        public EmailService(IOptions<Settings> options)
        {
            _options = options;
        }

        public async Task<bool> SendEmailAsync(Notification notification, SpecificGame game)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(_options.Value.Emailkeys.Host, _options.Value.Emailkeys.Port))
                {
                    MailMessage mail = new MailMessage(new MailAddress(_options.Value.Emailkeys.Username), new MailAddress(notification.email));
                    mail.Subject = $"{game.info.title} PRICE ALERT";
                    mail.Body = $"{game.info.title} IS SELLING FOR {game.deals[0].price}!!!!";
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_options.Value.Emailkeys.Username, _options.Value.Emailkeys.Password);
                    var a = Encryption.encryption(_options.Value.Emailkeys.Password);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    await smtp.SendMailAsync(mail);
                    mail.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
