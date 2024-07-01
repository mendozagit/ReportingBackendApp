using DevExpress.XtraReports.UI;
using ReportingBackendApp.Reports;

namespace ReportingBackendApp.DesignerCustomImplementations;

public static class MyReportsFactory
{
    public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>()
    {
        ["Report1"] = () => new Report1()
    };
}