using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;

namespace ReportingBackendApp.DesignerCustomImplementations
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
}