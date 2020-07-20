using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebSqlGit.Configuration
{
    public class ConfigSettings
    {
        private readonly IConfiguration _serviceConfiguration;

        #region Congigs

        public DbContextConfiguration DbContextConfiguration => _serviceConfiguration.GetSection("DbConnection").Get<DbContextConfiguration>();

        #endregion

        public ConfigSettings()
        {
            _serviceConfiguration = GetServiceConfiguration();
        }

        private IConfiguration GetServiceConfiguration()
        {
            string env = Environment.GetEnvironmentVariable ("ASPNETCORE_RUNTIME_ENVIRONMENT");
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false); // Базовая конфигурация

            string additioonalCofigFile = $"appsettings.{env}.json";
            if (File.Exists(additioonalCofigFile))
            {
                configurationBuilder.AddJsonFile(additioonalCofigFile, false); // Переопределения для окружения
            }

            return configurationBuilder.Build();
        }
    }
}
