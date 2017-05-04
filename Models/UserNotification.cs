using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using devices.Db;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace devices.Models
{

    public class Notification
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Message { get; set;}
        public string Cron { get; set;}
        public NotificationType notificationType { get; set;} 
        public bool Active { get; set;}
    }
    public class NotificationType
    {
        public int Id { get; set;}
        public string Name { get; set;}
        public string Description { get; set;}
    }

    public class UserNotification
    {
        public User User { get; set;}
        public List<Notification> Notifications { get; set;}
    }
}