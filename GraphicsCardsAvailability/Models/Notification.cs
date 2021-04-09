using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphicsCardsAvailability.Models
{
    public class Notification
    {
        public int card_id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public int price { get; set; }
    }
}
