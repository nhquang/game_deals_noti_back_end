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
        public string thumb { get; set; }
    }
    public class SpecificGame
    {
        public Info info { get; set; }
        public Deal[] deals { get; set; }
    }
    public class Deal
    {
        public int storeID { get; set; }
        public string store { get; set; }
        public string storeURL { get; set; }
        public string dealID { get; set; }
        public double price { get; set; }
    }
    public class Info
    {
        public string title { get; set; }
    }
}
