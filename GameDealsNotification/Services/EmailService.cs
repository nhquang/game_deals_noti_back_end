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

        async Task SendMailAsync(MailMessage mail)
        {
            using (SmtpClient smtp = new SmtpClient(_options.Value.Emailkeys.Host, _options.Value.Emailkeys.Port))
            {
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_options.Value.Emailkeys.Username, Encryption.decryption(_options.Value.Emailkeys.Password), _options.Value.Emailkeys.Host);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(mail);
            }
        } 

        public async Task<bool> SendNotiAsync(Notification notification, SpecificGame game)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
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
                    await SendMailAsync(mail);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendConfirmationEmailAsync(Notification notification)
        {
            try
            {
                string cad = notification.currency == Currency.CAD ? "C" : string.Empty;
                using (MailMessage mail = new MailMessage())
                {
                    mail.Subject = $"{notification.game.ToUpper()} PRICE ALERT CREATED SUCCESSFULLY!!!";
                    mail.Body = $"<!DOCTYPE html>"
                              + $"<html>"
                              + $"<body>"
                              + $"<div>"
                              + $"<h3>Hi {notification.name},</h3>"
                              + $"<h3>A price alert for {notification.game} has been created successfully in our system. We will email you once the price drops below {cad}${notification.price}.</h3>"
                              + $"</div>"
                              + $"</body>"
                              + $"</html>";
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(_options.Value.Emailkeys.Username);
                    mail.To.Add(new MailAddress(notification.email));
                    await SendMailAsync(mail);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
