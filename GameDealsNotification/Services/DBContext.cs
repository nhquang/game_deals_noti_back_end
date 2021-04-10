using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using GameDealsNotification.Configurations;
using GameDealsNotification.Models;
using GameDealsNotification.Services.Interfaces;
using GameDealsNotification.Utilities;
using Microsoft.Extensions.Options;

namespace GameDealsNotification.Services
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
