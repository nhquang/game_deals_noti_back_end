using GameDealsNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Notification notification, SpecificGame deal);
    }
}
