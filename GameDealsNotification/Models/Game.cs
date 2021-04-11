using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Models
{
    public class Game
    {
        public long gameID { get; set; }
        public string external { get; set; }
        public double cheapest { get; set; }
    }
}
