using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using GameDealsNotification.Services.Interfaces;
using GameDealsNotification.Configurations;
using Microsoft.Extensions.Options;
using GameDealsNotification.Utilities;
using Newtonsoft.Json;
using GameDealsNotification.Models;

namespace GameDealsNotification.Services
{
    public class MainService : IMainService
    {
        private readonly IDBContext _dBContext;
        private readonly IHttpRequest _httpRequest;
        private readonly IEmailService _emailService;
        
        public MainService(IDBContext dBContext, IHttpRequest httpRequest, IEmailService emailService)
        {
            _dBContext = dBContext;
            _httpRequest = httpRequest;
            _emailService = emailService;
        }

        public async Task ScanningItemsAndSendingNotiAsync()
        {
            try
            {
                var noties = await _dBContext.GetAllNotificationsAsync();
                var query = new Dictionary<string, string>();
                query.Add("id", string.Empty);
                foreach(var item in noties)
                {
                    query["id"] = item.game_id.ToString();
                    var responseString = await _httpRequest.GetRequestAsync("https://www.cheapshark.com/api/1.0/games", query);
                    var temp = JsonConvert.DeserializeObject<SpecificGame>(responseString);
                    if(temp.deals[0].price <= item.price)
                    {
                        if(await _emailService.SendEmailAsync(item, temp))
                        {

                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
