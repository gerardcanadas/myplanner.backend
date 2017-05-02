using System;
using MySql.Data.MySqlClient;

namespace devices.Db
{
    public class AppDb : IDisposable
    {

        public MySqlConnection Connection;

        public AppDb()
        {
            try {
                string connectionString = AppConfig.Config["Data:ConnectionString"];
                Connection = new MySqlConnection(connectionString);
                Connection.Open();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}