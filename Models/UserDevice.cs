using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using devices.Db;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace devices.Models
{
    public class User
    {
        public int Id { get; set;}
        public string Username { get; set;}
    }

    public class Device
    {
        public int Id { get; set;}
        public string DeviceId { get; set;}
        public bool Active { get; set;}
    }

    public class UserDevice
    {
        public User User { get; set;}
        public List<Device> Devices { get; set;}
    }
    
}