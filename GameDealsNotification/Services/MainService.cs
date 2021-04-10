using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using GameDealsNotification.Services.Interfaces;
using GameDealsNotification.Configurations;
using Microsoft.Extensions.Options;
using GameDealsNotification.Utilities;

namespace GameDealsNotification.Services
{
    public class MainService : IMainService
    {
        private readonly IOptions<DbConnectionConfigModel> _options;

        public MainService(IOptions<DbConnectionConfigModel> options)
        {
            _options = options;
        }

        public void ScanningItemsAndSendingNoti()
        {
            var a = Thread.CurrentThread.ManagedThreadId;
        }
    }
}
