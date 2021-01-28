using System.Configuration;

namespace ClearBank.DeveloperTest.Services.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string DataStoreType => Get("DataStoreType");

        private static string Get(string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException($"Configuration item \"{name}\" is missing.");
            }

            return value;
        }
    }
}
