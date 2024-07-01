using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.Utils;

namespace ReportingBackendApp.DesignerCustomImplementations;

public class MyConnectionProviderService(IConfiguration configuration) : IConnectionProviderService
{
    // "ReportsConnectionString": "Server=127.0.0.1,1433\\SQLEXPRESS;Database=ReportsDb;User Id=sa;Password=12345678;TrustServerCertificate=True"
    public SqlDataConnection LoadConnection(string connectionName)
    {
        // Load connection parameters from the configuration file instead of hardcoding them
        return new SqlDataConnection(connectionName, new MsSqlConnectionParameters
        {
            DatabaseName = "ReportsDb",
            ServerName = ".\\SQLEXPRESS",
            AuthorizationType = MsSqlAuthorizationType.Windows,
            Encrypt = DefaultBoolean.True,
            TrustServerCertificate = DefaultBoolean.True
        });
    }
}