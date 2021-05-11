using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameDealsNotification.Models;
using GameDealsNotification.Services.Interfaces;
using System.Threading;
using Microsoft.Extensions.Options;
using GameDealsNotification.Configurations;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace GameDealsNotification.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IDBContext _dBContext;
        private readonly IHttpRequest _httpRequest;
        private readonly IOptions<Settings> _settings;
        private readonly IEmailService _emailService;

        public NotificationsController(IDBContext dBContext, IHttpRequest httpRequest, IEmailService emailService, IOptions<Settings> settings)
        {
            _dBContext = dBContext;
            _httpRequest = httpRequest;
            _settings = settings;
            _emailService = emailService;
        }

        // GET api/values
        [HttpGet]
        [Route("GetGames")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> GetGames()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Query["title"]) || string.IsNullOrWhiteSpace(Request.Query["title"])) return BadRequest();
                var queries = new Dictionary<string, string>();
                queries.Add("title", Request.Query["title"].ToString().Replace(" ",""));
                var response = await _httpRequest.GetRequestAsync(_settings.Value.GetGamesURL, queries);
                var rslt = JsonConvert.DeserializeObject<Game[]>(response);
                return Ok(new { status = true, games = rslt });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("GetNotifications")]
        public async Task<ActionResult> GetNotifications()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Query["email"]) || string.IsNullOrWhiteSpace(Request.Query["email"])) return BadRequest();
                var notifications = await _dBContext.GetNotificationsByEmailAsync(Request.Query["email"]);
                return Ok(new { status = true, notifications = notifications.ToArray() });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }

        }
        [HttpGet]
        [Route("GetDeals")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> GetDeals()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Query["id"]) || string.IsNullOrWhiteSpace(Request.Query["id"])) return BadRequest();
                var queries = new Dictionary<string, string>();
                queries.Add("id", Request.Query["id"]);
                var response = await _httpRequest.GetRequestAsync(_settings.Value.GetGamesURL, queries);
                var rs = JsonConvert.DeserializeObject<SpecificGame>(response);
                for(int i = 0; i < rs.deals.Length; i++)
                {
                    switch (rs.deals[i].storeID)
                    {
                        case (int)Store.Steam:
                            rs.deals[i].store = Store.Steam.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Steam;
                            break;
                        case (int)Store.GamersGate:
                            rs.deals[i].store = Store.GamersGate.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.GamersGate;
                            break;
                        case (int)Store.GreenManGaming:
                            rs.deals[i].store = Store.GreenManGaming.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.GreenManGaming;
                            break;
                        case (int)Store.Direct2Drive:
                            rs.deals[i].store = Store.Direct2Drive.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Direct2Drive;
                            break;
                        case (int)Store.GOG:
                            rs.deals[i].store = Store.GOG.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.GOG;
                            break;
                        case (int)Store.Origin:
                            rs.deals[i].store = Store.Origin.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Origin;
                            break;
                        case (int)Store.HumbleStore:
                            rs.deals[i].store = Store.HumbleStore.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.HumbleStore;
                            break;
                        case (int)Store.Uplay:
                            rs.deals[i].store = Store.Uplay.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Uplay;
                            break;
                        case (int)Store.Fanatical:
                            rs.deals[i].store = Store.Fanatical.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Fanatical;
                            break;
                        case (int)Store.WinGameStore:
                            rs.deals[i].store = Store.WinGameStore.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.WinGameStore;
                            break;
                        case (int)Store.GameBillet:
                            rs.deals[i].store = Store.GameBillet.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.GameBillet;
                            break;
                        case (int)Store.EpicGames:
                            rs.deals[i].store = Store.EpicGames.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.EpicGames;
                            break;
                        case (int)Store.Gamesplanet:
                            rs.deals[i].store = Store.Gamesplanet.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.GamesPlanet;
                            break;
                        case (int)Store.Gamesload:
                            rs.deals[i].store = Store.Gamesload.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Gamesload;
                            break;
                        case (int)Store.TwoGame:
                            rs.deals[i].store = Store.TwoGame.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.TwoGame;
                            break;
                        case (int)Store.IndieGala:
                            rs.deals[i].store = Store.IndieGala.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.IndieGala;
                            break;
                        case (int)Store.Blizzard:
                            rs.deals[i].store = Store.Blizzard.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Blizzard;
                            break;
                        case (int)Store.AllYouPlay:
                            rs.deals[i].store = Store.AllYouPlay.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.AllYouPlay;
                            break;
                        case (int)Store.Voidu:
                            rs.deals[i].store = Store.Voidu.ToString();
                            rs.deals[i].storeURL = _settings.Value.Stores.Voidu;
                            break;
                    }
                }

                return Ok(new { status = true, deals = rs.deals });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        // POST api/values
        [HttpPost]
        [Route("AddNotification")]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult> Post([FromBody]Notification notification)
        {
            try
            {
                if (!(await _dBContext.AddNotificationAsync(notification))) throw new Exception("Failed to create price alert!!!");
                if(!(await _emailService.SendConfirmationEmailAsync(notification))) throw new Exception("Price alert created successfully, but we failed to send you a confirmation email!!!"); ;
                return Ok(new { status = true, message = "Price alert created successfully. A confirmation email has been sent!!!" });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/values/5
        [HttpDelete]
        [Route("DeleteNotification")]
        public async Task<ActionResult> Delete([FromBody] Notification notification)
        {
            try
            {
                if (!(await _dBContext.DeleteNotificationAsync(notification))) throw new Exception("Failed to remove price alert!!!");
                return Ok(new { status = true, message = "Price alert removed successfully!!!" });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }
    }
}
