using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;

namespace ReportingBackendApp.DesignerCustomImplementations;

public class MyConnectionProviderFactory(IConfiguration configuration) : IConnectionProviderFactory
{
    public IConnectionProviderService Create()
    {
        return new MyConnectionProviderService(configuration);
    }
}