using GraphicsCardsAvailability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsCardsAvailability.Services.Interfaces
{
    public interface IDBContext
    {
        bool AddNotification(Notification notification);
    }
}
