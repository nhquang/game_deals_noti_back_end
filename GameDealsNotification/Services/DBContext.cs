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
                    cmd.Parameters.Add(new SqlParameter("currency", (int)notification.currency));
                    status = (await cmd.ExecuteNonQueryAsync()) > 0 ? true : false;
                    cmd.Dispose();
                    database.Close();
                }
                return status;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteNotificationAsync(Notification notification)
        {
            var sqlString = _options.Value.DbConnectionString.Replace("{your_password}", Encryption.decryption(_options.Value.Password));
            try
            {
                bool status = false;
                using (var database = new SqlConnection(sqlString))
                {
                    await database.OpenAsync();
                    var cmd = new SqlCommand("delete_noti", database);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("email", notification.email));
                    cmd.Parameters.Add(new SqlParameter("game_id", notification.game_id));
                    cmd.Parameters.Add(new SqlParameter("price", notification.price));
                    status = (await cmd.ExecuteNonQueryAsync()) > 0 ? true : false;
                    cmd.Dispose();
                    database.Close();
                }
                return status;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            var sqlString = _options.Value.DbConnectionString.Replace("{your_password}", Encryption.decryption(_options.Value.Password));
            var rslt = new List<Notification>();
            using (var database = new SqlConnection(sqlString))
            {
                await database.OpenAsync();
                var cmd = new SqlCommand("get_all_notifications", database);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var temp = new Notification()
                    {
                        game_id = (int)reader.GetInt64(0),
                        email = reader.GetString(1),
                        price = (double)reader.GetDecimal(2),
                        name = reader.GetString(3),
                        currency = (Currency)reader.GetInt32(4)
                    };
                    rslt.Add(temp);
                }
                reader.Close();
                cmd.Dispose();
                database.Close();
            }
            return rslt;
        }

        
    }
}
