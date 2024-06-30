using DevExpress.AspNetCore.Reporting.ReportDesigner.Native.Services;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevExpress.AspNetCore.Reporting.QueryBuilder.Native.Services;
using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.DataAccess.Sql;

namespace ReportingBackendApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomReportDesignerController(
        IReportDesignerMvcControllerService controllerService,
        IReportDesignerModelBuilder reportDesignerModelBuilder)
        : ReportDesignerController(controllerService)
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> GetDesignerModel([FromForm] string reportName)
        {
            // ...
            reportName = string.IsNullOrEmpty(reportName) ? "Report1" : reportName;
            //var designerModel = await reportDesignerModelBuilder
            //    .Report(reportName)
            //    .BuildModelAsync();

            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("ReportsConnectionString");
            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("Products")
                .SelectAllColumnsFromTable()
                .Build("Products");

            ds.Queries.Add(query);
            ds.RebuildResultSchema();

            var designerModel = await reportDesignerModelBuilder
                .DataSources(x => { x.Add("ReportsConnectionString", ds); })
                .BuildModelAsync();


            //var designerModel = await reportDesignerModelBuilder.BuildModelAsync();


            return DesignerModel(designerModel);
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomQueryBuilderController(IQueryBuilderMvcControllerService controllerService)
        : QueryBuilderController(controllerService);

    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomWebDocumentViewerController(IWebDocumentViewerMvcControllerService controllerService)
        : WebDocumentViewerController(controllerService);
}