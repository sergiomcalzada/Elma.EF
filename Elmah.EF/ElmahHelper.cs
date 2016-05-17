using System.Collections;
using System.Configuration;

namespace Elmah.EF
{
    public static class ElmahHelper
    {
        public const int MaxAppLength = 60;
        public const int MaxHostName = 50;

        public static T Find<T>(this IDictionary dic, object key, T defaultValue)
        {
            if (!dic.Contains(key)) return defaultValue;
            return (T)dic[key];
        }

        public static string GetConnectionString()
        {

            var errorLogSection = ConfigurationManager.GetSection("elmah/errorLog") as IDictionary;
            if (errorLogSection == null)
            {
                throw new ConfigurationErrorsException("The elmah/errorLog section is missing from the application's configuration file.");
            }

            return GetConnectionString(errorLogSection);
        }

        public static string GetConnectionString(IDictionary config)
        {

            if (!config.Contains("connectionStringName"))
            {
                throw new ConfigurationErrorsException(
                    "The elmah/errorLog section in the application's configuration file is missing the connectionStringName attribute.");
            }

            var connectionString = config["connectionStringName"].ToString();
            if (connectionString.Length == 0)
                throw new ApplicationException("Connection string is missing for the SQL error log.");
            
            return connectionString;
        }

        public static string GetAppName(IDictionary config)
        {

            var appName = config.Find("applicationName", string.Empty);

            if (appName.Length > MaxAppLength)
            {
                throw new ApplicationException($"Application name is too long. Maximum length allowed is {MaxAppLength.ToString("N0")} characters.");
            }

            return appName;
        }
    }
}