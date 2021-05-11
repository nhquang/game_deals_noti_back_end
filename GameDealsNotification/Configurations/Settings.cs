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
        public Stores Stores { get; set; }
    }
    public class Emailkeys
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Stores
    {
        public string Steam { get; set; }
        public string GamersGate { get; set; }
        public string GreenManGaming { get; set; }
        public string Direct2Drive { get; set; }
        public string GOG { get; set; }
        public string Origin { get; set; }
        public string HumbleStore { get; set; }
        public string Uplay { get; set; }
        public string Fanatical { get; set; }
        public string WinGameStore { get; set; }
        public string GameBillet { get; set; }
        public string EpicGames { get; set; }
        public string GamesPlanet { get; set; }
        public string Gamesload { get; set; }
        public string TwoGame { get; set; }
        public string IndieGala { get; set; }
        public string Blizzard { get; set; }
        public string AllYouPlay { get; set; }
        public string Voidu { get; set; }
    }

}
