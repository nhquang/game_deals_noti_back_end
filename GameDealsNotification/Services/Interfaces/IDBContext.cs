using GameDealsNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Services.Interfaces
{
    public interface IDBContext
    {
        bool AddNotification(Notification notification);
    }
}
