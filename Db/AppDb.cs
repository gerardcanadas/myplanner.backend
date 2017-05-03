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
                Console.WriteLine("ConnectionString: {0}", connectionString);
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