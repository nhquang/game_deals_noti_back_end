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

namespace GameDealsNotification.Controllers
{
    [Route("notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IDBContext _dBContext;
        private readonly IHttpRequest _httpRequest;
        private readonly IOptions<Settings> _settings;

        public NotificationsController(IDBContext dBContext, IHttpRequest httpRequest, IOptions<Settings> settings)
        {
            _dBContext = dBContext;
            _httpRequest = httpRequest;
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        [Route("GetGames")]
        public async Task<ActionResult> Get()
        {
            try
            {
                if (string.IsNullOrEmpty(Request.Query["title"]) || string.IsNullOrWhiteSpace(Request.Query["title"])) throw new Exception("title is required!!!");
                var queries = new Dictionary<string, string>();
                queries.Add("title", Request.Query["title"]);
                var response = await _httpRequest.GetRequestAsync(_settings.Value.GetGamesURL, queries);
                return Ok(new { status = true, games = response });
            }
            catch(Exception ex)
            {
                return Ok(new { status = false, message = ex.Message });
            }
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        [Route("AddNotification")]
        public async Task<ActionResult> Post(Notification notification)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception("Invalid inputs!!!");

                if (!(await _dBContext.AddNotificationAsync(notification))) throw new Exception("Failed to create price alert!!!");
                return Ok(new { status = true, message = "Price alert created successfully!!!" });
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
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
