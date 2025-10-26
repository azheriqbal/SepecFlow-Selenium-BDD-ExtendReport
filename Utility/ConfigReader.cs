using System.Configuration;

namespace SeleniumBDDFramework.Utilities
{
    public static class ConfigReader
    {
        public static string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    }
}
