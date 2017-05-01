using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using devices.Models;
using MySql.Data.MySqlClient;

namespace devices.Db.Repositories
{
    public class UserDeviceRespository
    {
        //private log4net.ILog log = log4net.LogManager.GetLogger(Assembly.GetEntryAssembly().GetName().Name, "log");
        private AppDb Db = null;

        public UserDeviceRespository(AppDb appDb) 
        {
            this.Db = appDb;
        }

        public async Task InsertUserAsync(User user)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO 'Users' ('Username') VALUES (@username);";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = user.Username
            });
            await cmd.ExecuteNonQueryAsync();
            user.Id = (int) cmd.LastInsertedId;
        }

        public async Task InsertUserDeviceAsync(UserDevice uDev)
        {
            try {
                int userId = -1;
                int deviceId = -1;
                var cmd = Db.Connection.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO Users (Username) VALUES (@username);";
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@username",
                    DbType = DbType.String,
                    Value = uDev.User.Username
                });
                await cmd.ExecuteNonQueryAsync();
                userId = (int) cmd.LastInsertedId;

                cmd = null;
                cmd = Db.Connection.CreateCommand() as MySqlCommand;
                foreach (Device dev in uDev.Devices) {
                    cmd.CommandText = @"INSERT INTO Devices (deviceId, active) VALUES (@deviceId, 0);";
                    cmd.Parameters.Add(new MySqlParameter
                    {
                        ParameterName = "@deviceId",
                        DbType = DbType.String,
                        Value = uDev.Devices.FirstOrDefault().DeviceId
                    });
                
                    await cmd.ExecuteNonQueryAsync();
                    deviceId = (int) cmd.LastInsertedId;

                    if (userId > 0 && deviceId > 0)
                    {
                        cmd = null;
                        cmd = Db.Connection.CreateCommand() as MySqlCommand;
                        cmd.CommandText = @"INSERT INTO UsersDevices (userId, deviceId) VALUES (@userId, @deviceId)";
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@userId",
                            DbType = DbType.Int32,
                            Value = userId
                        });
                        cmd.Parameters.Add(new MySqlParameter
                        {
                            ParameterName = "@deviceId",
                            DbType = DbType.Int32,
                            Value = deviceId
                        });
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex) 
            {
                //log.Error("Error creating UserDevice entity", ex);
                throw ex;
            }
        }

        public async Task<UserDevice> FindUserDevicesAsync(string Username)
        {
            var cmd = Db.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT 'user.id', 'user.username','device.id', 'device.deviceId', 'device.active' FROM UserDevices udev 
                                LEFT JOIN Users user ON udev.userId = user.id
                                LEFT JOIN Devices device ON udev.deviceId = device.Id
                                WHERE user.username = @username";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@username",
                DbType = DbType.String,
                Value = Username,
            });
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<UserDevice> ReadAllAsync(DbDataReader reader)
        {
            UserDevice res = null;
            User user = null;
            List<Device> devicesList = new List<Device>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    if (user == null) 
                    {
                        user = new User()
                        {
                            Id = await reader.GetFieldValueAsync<int>(0),
                            Username = await reader.GetFieldValueAsync<string>(1)
                        };
                        devicesList.Add(new Device()
                        {
                            Id = await reader.GetFieldValueAsync<int>(2),
                            DeviceId = await reader.GetFieldValueAsync<string>(3),
                            Active = await reader.GetFieldValueAsync<bool>(4),
                        });
                    }
                }
            }

            res = new UserDevice()
            {
                User = user,
                Devices = devicesList
            };

            return res;
        }

    }
}