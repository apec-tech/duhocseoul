using Microsoft.Extensions.Configuration;

namespace DuHocSeoulWebsite.Utils
{
    public class AppSettings
    {
        private static AppSettings _appSettings;
        private readonly IConfigurationRoot _configuration;

        public static AppSettings Instance => _appSettings ?? (_appSettings = new AppSettings());

        public AppSettings()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        public string GetByKey(string key)
        {
            try
            {
                return _configuration[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        public void Reload()
        {
            _configuration.Reload();
        }
    }
}
