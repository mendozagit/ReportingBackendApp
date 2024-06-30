using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Native;
using DevExpress.DataAccess.Web;
using DevExpress.Utils;

namespace ReportingBackendApp.Models
{
    public class ReportDesignerConnectionProvider
        : IDataSourceWizardConnectionStringsProvider
    {
        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = AppConfigHelper.GetConnections().Keys.ToDictionary(x => x, x => x);


            // Customize the loaded connections list. 
            connections.Add("ReportsConnectionString", "Application reports data source");
            return connections;
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            // Return custom connection parameters for the custom connection(s). 

            var connectionParameters = GetMsSqlConnectionParameters(name);

           // return connectionParameters;


           return AppConfigHelper.LoadConnectionParameters(name);
        }

        private MsSqlConnectionParameters GetMsSqlConnectionParameters(string name)
        {
            var configuration = ConfigurationProvider.GetConfiguration();
            var connectionString = configuration.GetConnectionString(name);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Report Connection string '{name}' not found.");
            }


            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Report Connection string '{name}' not found.");
            }

            var parameters = ParseConnectionString(connectionString);
            return new MsSqlConnectionParameters(
                parameters["Server"],
                parameters["Database"],
                parameters.TryGetValue("User Id", out var parameter) ? parameter : null,
                parameters.TryGetValue("Password", out var parameter1) ? parameter1 : null,
                parameters.ContainsKey("Integrated Security") && parameters["Integrated Security"]
                    .Equals("SSPI", StringComparison.OrdinalIgnoreCase)
                    ? MsSqlAuthorizationType.Windows
                    : MsSqlAuthorizationType.SqlServer,
                parameters.ContainsKey("Encrypt") &&
                parameters["Encrypt"].Equals("True", StringComparison.OrdinalIgnoreCase)
                    ? DefaultBoolean.True
                    : DefaultBoolean.False,
                parameters.ContainsKey("TrustServerCertificate") && parameters["TrustServerCertificate"]
                    .Equals("True", StringComparison.OrdinalIgnoreCase)
                    ? DefaultBoolean.True
                    : DefaultBoolean.False
            );
        }

        private Dictionary<string, string> ParseConnectionString(string connectionString)
        {
            return connectionString.Split(';')
                .Where(part => part.Contains('='))
                .Select(part => part.Split('='))
                .ToDictionary(
                    keyValue => keyValue[0].Trim(),
                    keyValue => keyValue[1].Trim());
        }
    }
}

public static class ConfigurationProvider
{
    private static IConfiguration _configuration;

    public static void Initialize(IConfiguration configuration)
    {
        ConfigurationProvider._configuration = configuration;
    }

    public static IConfiguration GetConfiguration()
    {
        if (_configuration == null)
        {
            throw new InvalidOperationException("Configuration has not been initialized.");
        }

        return _configuration;
    }
}