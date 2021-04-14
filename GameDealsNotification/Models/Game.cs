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
        //public double cheapest { get; set; }
    }
    public class SpecificGame
    {
        public Info info { get; set; }
        public Deal[] deals { get; set; }
    }
    public class Deal
    {
        public int storeID { get; set; }
        public string dealID { get; set; }
        private double price_;
        public double price { get { return price_; } set { price_ = value * 1.3334; } }
    }
    public class Info
    {
        public string title { get; set; }
    }
}
