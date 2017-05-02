using System.IO;
using Microsoft.Extensions.Configuration;

namespace devices.Db
{
    public static class AppConfig
    {
        public static IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile("config.json")
        .Build();
    }
}