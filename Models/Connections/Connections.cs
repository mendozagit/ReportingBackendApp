using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.Utils;

namespace ReportingBackendApp.Models.Connections
{
    public class MyDataSourceWizardConnectionStringsProvider(
        IConfiguration configuration,
        IConnectionProviderFactory connectionProviderFactory)
        : IDataSourceWizardConnectionStringsProvider
    {
        public Dictionary<string, string> GetConnectionDescriptions()
        {
            //Load list of connections here
            return new Dictionary<string, string>
            {
                { "ReportsConnectionString", "Application reports connection" },
            };
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            //Move implementation logic to the IConnectionProviderService.LoadConnection method
            var connectionProviderService = connectionProviderFactory.Create();
            var connectionParameters = connectionProviderService.LoadConnection(name).ConnectionParameters;
            return connectionParameters;
        }
    }

    public class MyConnectionProviderFactory(IConfiguration configuration) : IConnectionProviderFactory
    {
        public IConnectionProviderService Create()
        {
            return new MyConnectionProviderService(configuration);
        }
    }

    public class MyConnectionProviderService(IConfiguration configuration) : IConnectionProviderService
    {
        // "ReportsConnectionString": "Server=127.0.0.1,1433\\SQLEXPRESS;Database=ReportsDb;User Id=sa;Password=12345678;TrustServerCertificate=True"
        public SqlDataConnection LoadConnection(string connectionName)
        {
            return new SqlDataConnection(connectionName, new MsSqlConnectionParameters
            {
                DatabaseName = "ReportsDb",
                Password = "12345678",
                ServerName = "127.0.0.1,1433\\SQLEXPRESS",
                UserName = "sa",
                AuthorizationType = MsSqlAuthorizationType.SqlServer,
                Encrypt = DefaultBoolean.True,
                TrustServerCertificate = DefaultBoolean.True
            });
        }
    }
}