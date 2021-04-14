using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDealsNotification.Configurations
{
    public class Settings
    {
        public string GetGamesURL { get; set; }
        public Emailkeys Emailkeys { get; set; }
    }
    public class Emailkeys
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
