using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsCardsAvailability.Utilities
{
    public static class Encryption
    {
        public static string encryption(string pwd)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(pwd);
            return Convert.ToBase64String(bytes);
        }
        public static string decryption(string encrypted)
        {
            byte[] bytes = Convert.FromBase64String(encrypted);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
