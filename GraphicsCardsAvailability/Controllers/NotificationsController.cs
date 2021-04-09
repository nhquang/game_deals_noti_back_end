using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraphicsCardsAvailability.Models;
using GraphicsCardsAvailability.Services.Interfaces;

namespace GraphicsCardsAvailability.Controllers
{
    [Route("notifications/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private IDBContext _dBContext;

        public NotificationsController(IDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post(Notification notification)
        {
            _dBContext.AddNotification(notification);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
