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

        public async Task<bool> SendNotiAsync(Notification notification, SpecificGame game)
        {
            try
            {
                using (SmtpClient smtp = new SmtpClient(_options.Value.Emailkeys.Host, _options.Value.Emailkeys.Port))
                {
                    MailMessage mail = new MailMessage();
                    mail.Subject = $"{game.info.title} PRICE ALERT";
                    mail.Body = $"<!DOCTYPE html>"
                              + $"<html>"
                              + $"<body>"
                              + $"<h1>Price Alert!!!!</h1>"
                              + $"<div>"
                              + $"<h3>Hi {notification.name},</h3>"
                              + $"<h3>{game.info.title} is selling for <span style = 'color:red'>${game.deals[0].price}</span> on <a href = '{game.deals[0].storeURL}' style=''>{game.deals[0].store}</a></h3>"
                              + $"</div>"
                              + $"</body>"
                              + $"</html>";
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(_options.Value.Emailkeys.Username);
                    mail.To.Add(new MailAddress(notification.email));
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_options.Value.Emailkeys.Username,Encryption.decryption(_options.Value.Emailkeys.Password), _options.Value.Emailkeys.Host);
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
