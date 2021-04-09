using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GraphicsCardsAvailability.Configurations;
using GraphicsCardsAvailability.Models;
using GraphicsCardsAvailability.Services.Interfaces;
using GraphicsCardsAvailability.Utilities;
using Microsoft.Extensions.Options;

namespace GraphicsCardsAvailability.Services
{
    public class DBContext : IDBContext
    {
        private IOptions<DbConnectionConfigModel> _options;

        public DBContext(IOptions<DbConnectionConfigModel> options)
        {
            _options = options;
        }
        public bool AddNotification(Notification notification)
        {
            var sqlString = _options.Value.DbConnectionString.Replace("{your_password}", Encryption.decryption(_options.Value.Password));
            try
            {
                bool status = false;
                using (var database = new SqlConnection(sqlString))
                {

                }
                return status;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
