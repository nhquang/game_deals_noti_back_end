using GameDealsNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Services.Interfaces
{
    public interface IDBContext
    {
        Task<bool> AddNotificationAsync(Notification notification);
        Task<List<Notification>> GetAllNotificationsAsync();
        Task<bool> DeleteNotificationAsync(Notification notification);
        Task<List<Notification>> GetNotificationsByEmailAsync(string email);
    }
}
