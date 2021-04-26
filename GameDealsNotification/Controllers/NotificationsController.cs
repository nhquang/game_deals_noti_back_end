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
        public async Task<ActionResult> Get()
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
