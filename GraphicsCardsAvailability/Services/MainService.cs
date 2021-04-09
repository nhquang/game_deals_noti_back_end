using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphicsCardsAvailability.Services.Interfaces;
using GraphicsCardsAvailability.Configurations;
using Microsoft.Extensions.Options;

namespace GraphicsCardsAvailability.Services
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
            throw new NotImplementedException();
        }
    }
}
