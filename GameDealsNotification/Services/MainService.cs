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
        private readonly IOptions<Settings> _options;
        
        public MainService(IDBContext dBContext, IHttpRequest httpRequest, IEmailService emailService ,IOptions<Settings> options)
        {
            _dBContext = dBContext;
            _httpRequest = httpRequest;
            _emailService = emailService;
            _options = options;
        }

        public async Task ScanningDealsAndSendingNotiAsync()
        {
            try
            {
                var noties = await _dBContext.GetAllNotificationsAsync();
                var query = new Dictionary<string, string>();
                query.Add("id", string.Empty);
                foreach(var noti in noties)
                {
                    query["id"] = noti.game_id.ToString();
                    var responseString = await _httpRequest.GetRequestAsync("https://www.cheapshark.com/api/1.0/games", query);
                    var temp = JsonConvert.DeserializeObject<SpecificGame>(responseString);
                    if (noti.currency == Currency.CAD) temp.deals[0].price = Math.Round(temp.deals[0].price * 1.3335, 2) ;
                    if (temp.deals[0].price <= noti.price || temp.deals[0].price < noti.price + 10)
                    {
                        switch (temp.deals[0].storeID)
                        {
                            case (int)Store.Steam:
                                temp.deals[0].store = Store.Steam.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Steam;
                                break;
                            case (int)Store.GamersGate:
                                temp.deals[0].store = Store.GamersGate.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.GamersGate;
                                break;
                            case (int)Store.GreenManGaming:
                                temp.deals[0].store = Store.GreenManGaming.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.GreenManGaming;
                                break;
                            case (int)Store.Direct2Drive:
                                temp.deals[0].store = Store.Direct2Drive.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Direct2Drive;
                                break;
                            case (int)Store.GOG:
                                temp.deals[0].store = Store.GOG.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.GOG;
                                break;
                            case (int)Store.Origin:
                                temp.deals[0].store = Store.Origin.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Origin;
                                break;
                            case (int)Store.HumbleStore:
                                temp.deals[0].store = Store.HumbleStore.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.HumbleStore;
                                break;
                            case (int)Store.Uplay:
                                temp.deals[0].store = Store.Uplay.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Uplay;
                                break;
                            case (int)Store.Fanatical:
                                temp.deals[0].store = Store.Fanatical.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Fanatical;
                                break;
                            case (int)Store.WinGameStore:
                                temp.deals[0].store = Store.WinGameStore.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.WinGameStore;
                                break;
                            case (int)Store.GameBillet:
                                temp.deals[0].store = Store.GameBillet.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.GameBillet;
                                break;
                            case (int)Store.EpicGames:
                                temp.deals[0].store = Store.EpicGames.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.EpicGames;
                                break;
                            case (int)Store.Gamesplanet:
                                temp.deals[0].store = Store.Gamesplanet.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.GamesPlanet;
                                break;
                            case (int)Store.Gamesload:
                                temp.deals[0].store = Store.Gamesload.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Gamesload;
                                break;
                            case (int)Store.TwoGame:
                                temp.deals[0].store = Store.TwoGame.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.TwoGame;
                                break;
                            case (int)Store.IndieGala:
                                temp.deals[0].store = Store.IndieGala.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.IndieGala;
                                break;
                            case (int)Store.Blizzard:
                                temp.deals[0].store = Store.Blizzard.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.Blizzard;
                                break;
                            case (int)Store.AllYouPlay:
                                temp.deals[0].store = Store.AllYouPlay.ToString();
                                temp.deals[0].storeURL = _options.Value.Stores.AllYouPlay;
                                break;
                        }
                        if (await _emailService.SendNotiAsync(noti, temp))
                            await _dBContext.DeleteNotificationAsync(noti);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
