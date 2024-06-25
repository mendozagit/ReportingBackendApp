using DevExpress.XtraReports.UI;
using Microsoft.EntityFrameworkCore;
using ReportingBackendApp.Reports;
using System;
using System.Security.Policy;

namespace ReportingBackendApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SqlDataConnectionDescription> SqlDataConnections { get; set; }
        public DbSet<ReportItem> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().Property(x => x.Price).HasPrecision(18, 6);
            // Seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Chai", Price = 18.00m },
                new Product { Id = 2, Name = "Chang", Price = 19.00m },
                new Product { Id = 3, Name = "Aniseed Syrup", Price = 10.00m },
                new Product { Id = 4, Name = "Chef Anton's Cajun Seasoning", Price = 22.00m },
                new Product { Id = 5, Name = "Chef Anton's Gumbo Mix", Price = 21.35m },
                new Product { Id = 6, Name = "Grandma's Boysenberry Spread", Price = 25.00m },
                new Product { Id = 7, Name = "Uncle Bob's Organic Dried Pears", Price = 30.00m },
                new Product { Id = 8, Name = "Northwoods Cranberry Sauce", Price = 40.00m },
                new Product { Id = 9, Name = "Mishi Kobe Niku", Price = 97.00m },
                new Product { Id = 10, Name = "Ikura", Price = 31.00m }
            );

            // Seed reports
            modelBuilder.Entity<ReportItem>().HasData(
                new ReportItem
                {
                    Id = 1,
                    Name = "Report1",
                    DisplayName = "Products catalog demo report",
                    LayoutData = GetReportBytes("Report1")
                }
            );
        }

        public DbSet<Product> Products { get; set; }


        byte[] GetReportBytes(string reportName)
        {
            if (!ReportsFactory.Reports.ContainsKey(reportName))
                throw new DevExpress.XtraReports.Web.ClientControls.FaultException(
                    $"Could not find report '{reportName}'.");

            using var ms = new MemoryStream();
            using var report = ReportsFactory.Reports[reportName]();
            report.SaveLayoutToXml(ms);
            return ms.ToArray();
        }
    }
}