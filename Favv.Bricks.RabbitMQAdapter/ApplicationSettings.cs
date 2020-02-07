using Microsoft.Extensions.Configuration;
using System.IO;

namespace Favv.Bricks.RabbitMQAdapter
{
    public static class ApplicationSettings
    {
        public static IConfiguration GetSettings()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Directory where the json files are located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}
