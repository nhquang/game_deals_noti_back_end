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
        private readonly IOptions<DbConnectionConfigModel> _options;

        public DBContext(IOptions<DbConnectionConfigModel> options)
        {
            _options = options;
        }
        public async Task<bool> AddNotificationAsync(Notification notification)
        {
            var sqlString = _options.Value.DbConnectionString.Replace("{your_password}", Encryption.decryption(_options.Value.Password));
            try
            {
                bool status = false;
                using (var database = new SqlConnection(sqlString))
                {
                    await database.OpenAsync();
                    var cmd = new SqlCommand("add_noti", database);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("email", notification.email));
                    cmd.Parameters.Add(new SqlParameter("game_id", notification.game_id));
                    cmd.Parameters.Add(new SqlParameter("price", notification.price));
                    cmd.Parameters.Add(new SqlParameter("name", notification.name));
                    status = (await cmd.ExecuteNonQueryAsync()) > 0 ? true : false;
                    database.Close();
                }
                return status;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

    }
}
